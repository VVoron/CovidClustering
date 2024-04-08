using Microsoft.ML;
using NeuroCovid19.MVVM.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System.Data;
using System.Windows;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace NeuroCovid19.Extensions
{
    public static class ClasterVisualisationExtension
    {
        public static List<PCAFitted> GetGraphPoints(List<DataCOVIDEars[]> data, List<PropertiesModel> props)
        {
            try
            {
                var result = new List<PCAFitted>();
                List<DataCOVIDEarsFloatType> allDate = new List<DataCOVIDEarsFloatType>();
                int clasterIndex = 0;
                foreach (var claster in data)
                {

                    allDate.AddRange(claster.Select(x => new DataCOVIDEarsFloatType(x, (float)clasterIndex, props)));
                    clasterIndex++;
                }
                if (props.Where(x => x.IsUsed).Count() > 1)
                {
                    var context = new MLContext();
                    var colomnNamesOf = props.Where(x => x.IsUsed).Select(x => BaseExtensions.GetProperyNameById<DataCOVIDEarsFloatType>(x.Id + 1)).ToArray();
                    if (colomnNamesOf.Length == 1)
                    {
                        colomnNamesOf = new string[] { colomnNamesOf[0], "_additionalProperty" };
                    }
                    var dataView = context.Data.LoadFromEnumerable(allDate);
                    var normalizePipeline = context.Transforms.NormalizeMinMax(colomnNamesOf.Select(x => new InputOutputColumnPair(x + "Norm", x)).ToArray());
                    var normalizedDataView = normalizePipeline.Fit(dataView).Transform(dataView);

                    var pipeline = context.Transforms.Concatenate("Features", colomnNamesOf.Select(x => x + "Norm").ToArray())
                                                     .Append(context.Transforms.ProjectToPrincipalComponents("PCAFeatures", "Features", rank: 2, seed: 5));
                    var model = pipeline.Fit(normalizedDataView);
                    var transformedData = model.Transform(normalizedDataView);
                    result = context.Data.CreateEnumerable<PCAFitted>(transformedData, reuseRowObject: false).ToList();

                    if (result.Any(x => Double.IsNaN(x.PCAFeatures[0])))
                        MessageBox.Show("Ошибка при попытке провести кластеризацию: 1 или несколько свойств из списка имеют идентичные значения для всей выборки (процент их наличия больше 50)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var prop = props.Find(x => x.IsUsed);
                    var max = allDate.Select(x => x.GetPropertyValueById(prop.Id + 1)).Max();
                    var min = allDate.Select(x => x.GetPropertyValueById(prop.Id + 1)).Min();

                    result = allDate.Select(x => new PCAFitted() { PCAFeatures = new float[] { (float)((x.GetPropertyValueById(prop.Id + 1) - min) / (max - min)), (float)((x.GetPropertyValueById(prop.Id + 1) - min) / (max - min)) }, _clastIndex = x._clastIndex }).ToList();
                }
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                MessageBox.Show($"Ошибка при понижении размерности данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<PCAFitted>();
            }
        }
    }
}
