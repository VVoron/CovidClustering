using Microsoft.ML;
using Microsoft.ML.Data;
using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Providers
{
    public class ClassificationProvider
    {
        private readonly string _csvPath = "synthetic_oae_assr.csv";
        private readonly MLContext _mlContext;
        private readonly IDataView _dataView;
        private readonly bool _isPretrained = false;

        private TransformerChain<Microsoft.ML.Transforms.KeyToValueMappingTransformer> Model;

        public ClassificationProvider()
        {
            _mlContext = new MLContext(seed: 7);
            var loader = _mlContext.Data.CreateTextLoader(new TextLoader.Options
            {
                HasHeader = true,
                Separators = new[] { ',' },
                AllowQuoting = true,
                Columns = new[]
                {
                    new TextLoader.Column(nameof(HearingSample.ObservationMonths), DataKind.Single, 0),
                    new TextLoader.Column(nameof(HearingSample.GestWeeks), DataKind.Single, 1),

                    new TextLoader.Column(nameof(HearingSample.OAE_1k), DataKind.Single, 2),
                    new TextLoader.Column(nameof(HearingSample.OAE_2k), DataKind.Single, 3),
                    new TextLoader.Column(nameof(HearingSample.OAE_4k), DataKind.Single, 4),
                    new TextLoader.Column(nameof(HearingSample.OAE_6k), DataKind.Single, 5),

                    new TextLoader.Column(nameof(HearingSample.OAE_Mean), DataKind.Single, 6),
                    new TextLoader.Column(nameof(HearingSample.OAE_HF_Tilt), DataKind.Single, 7),

                    new TextLoader.Column(nameof(HearingSample.ASSR_Mean_dB), DataKind.Single, 8),
                    new TextLoader.Column(nameof(HearingSample.Class), DataKind.String, 9),
                }
            });

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "synthetic_oae_assr.csv");
            _dataView = loader.Load(filePath);
        }

        public string StartClassification()
        {
            var split = _mlContext.Data.TrainTestSplit(_dataView, testFraction: 0.2, seed: 7);
            var train = split.TrainSet;
            var test = split.TestSet;

            var features = new[]
            {
                nameof(HearingSample.ObservationMonths),
                nameof(HearingSample.GestWeeks),

                nameof(HearingSample.OAE_1k),
                nameof(HearingSample.OAE_2k),
                nameof(HearingSample.OAE_4k),
                nameof(HearingSample.OAE_6k),

                nameof(HearingSample.OAE_Mean),
                nameof(HearingSample.OAE_HF_Tilt),

                nameof(HearingSample.ASSR_Mean_dB),
            };

            var pipeline =
                _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(HearingSample.Class))
                .Append(_mlContext.Transforms.Concatenate("Features", features))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            Model = pipeline.Fit(train);

            var pred = Model.Transform(test);
            var metrics = _mlContext.MulticlassClassification.Evaluate(pred, labelColumnName: "Label", scoreColumnName: "Score");


            var resultMetrics = $"MicroAccuracy: {metrics.MicroAccuracy:F3}\n" +
                                $"MacroAccuracy: {metrics.MacroAccuracy:F3}\n" +
                                $"LogLoss: {metrics.LogLoss:F3}";

            return resultMetrics;
        }

        public bool IsModelPretrained()
        {
            return _isPretrained;
        }

        public void DoClassificationForData(List<DataCOVIDEars> items)
        {
            var classificationClasses = new List<List<DataCOVIDEars>>()
            {
                new List<DataCOVIDEars>(),
                new List<DataCOVIDEars>(),
                new List<DataCOVIDEars>(),
                new List<DataCOVIDEars>(),
                new List<DataCOVIDEars>(),
                new List<DataCOVIDEars>()
            };

            var engine = _mlContext.Model.CreatePredictionEngine<HearingSample, HearingPrediction>(Model);

            foreach (var item in items)
            {
                var sample = new HearingSample(item);
                var res = engine.Predict(sample);

                if (int.TryParse(res.PredictedLabel, out var classNum))
                {
                    try
                    {
                        classificationClasses[classNum - 1].Add(item);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(classNum);
                    }
                }
            }

            App.ContextOfData.ClassificationClasses = classificationClasses.Select(x => x.ToArray()).ToList();
        }
    }
}
