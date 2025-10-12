using NeuroCovid19.Enumerations;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using NeuroCovid19.Providers;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Forms;
using System.Windows.Media;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;

namespace NeuroCovid19.Extensions
{
    public static class ReportExtensions
    {
        public static void GetClasterReport()
        {
            var data = App.ContextOfData.SelectedMethod;
            var clasterisationProvider = new ClasterisationProvider();
            var dataColumns = clasterisationProvider.СolomnsData();

            var fileName = $"Отчет по методу {App.ContextOfData.SelectedClasterisationString}";

            var clasters = App.ContextOfData.SelectedMethod == Method.Kohanen ? App.ContextOfData.KohanenOptions.ClastersInfo :
                           App.ContextOfData.SelectedMethod == Method.DBScan ? App.ContextOfData.DBScanOptions.ClastersInfo :
                           App.ContextOfData.ClassificationClasses;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel Workbook|*.xls*", ValidateNames = true, FileName = fileName })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExcelPackage excelFile = new ExcelPackage();
                    excelFile.Workbook.Properties.Author = "NeuroNet";
                    excelFile.Workbook.Properties.Title = fileName;
                    excelFile.Workbook.Properties.Created = DateTime.Now;

                    GenerateFirstPage(excelFile, clasters);
                    GenerateAvarageDataInfo(excelFile, clasters);
                    GenerateAiPage(excelFile, clasters);

                    for (int clastIndex = 0; clastIndex < clasters.Count(); clastIndex++)
                    {
                        var claster = clasters[clastIndex];
                        var worksheet = excelFile.Workbook.Worksheets.Add(claster.Name);
                        for (int i = 0; i < dataColumns.Count; i++)
                            worksheet.Cells[1, i + 1].Value = dataColumns[i];
                        int k = 2;

                        foreach (DataCOVIDEars item in claster.Items)
                        {
                            var info = item.GetAllData();
                            for (int i = 0; i < info.Count(); i++)
                            {
                                if (double.TryParse(info[i], out double value))
                                {
                                    worksheet.Cells[k, i + 1].Value = value;
                                    if (clasterisationProvider.colomnsToCheck.Any(x => x.Contains(i)))
                                    {
                                        worksheet.Cells[k, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[k, i + 1].Style.Fill.BackgroundColor.SetColor(GetColorForCells(clasterisationProvider, info, i, item.TimeObservation, item.TimeGestagration));
                                    }
                                }
                                else
                                {
                                    worksheet.Cells[k, i + 1].Value = info[i];
                                }
                            }
                            k++;
                        }

                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    }

                    excelFile.SaveAs(new FileInfo(saveFileDialog.FileName + ".xlsx"));
                    MessageBox.Show("Данные были успешно сохранены в файл\n" + saveFileDialog.FileName);
                }
            }
        }

        private static void GenerateFirstPage(ExcelPackage excelFile, List<ClasterInfo> clasters)
        {
            var worksheet = excelFile.Workbook.Worksheets.Add($"Информация по отчету");

            worksheet.Cells[2, 2, 2, 3].Merge = true;
            worksheet.Cells[2, 2].Value = $"Отчет на {DateTime.Now.ToString("HH:mm - dd.MM.yyyy")}";
            worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheet.Cells[3, 2, 3, 3].Merge = true;
            worksheet.Cells[3, 2].Value = $"Выбранный метод - {App.ContextOfData.SelectedClasterisationString}";
            worksheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[3, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheet.Cells[4, 2].Value = "Количество кластеров/классов";
            worksheet.Cells[4, 3].Value = clasters.Count;

            var currentRowIndex = 5;
            if (App.ContextOfData.SelectedMethod != Method.Classification)
            {
                worksheet.Cells[currentRowIndex, 2].Value = "Количество используемых признаков для кластеризации";
                var numProps = App.ContextOfData.SelectedMethod == Method.Kohanen ? App.ContextOfData.KohanenOptions.Properties.Count(x => x.IsUsed)
                    : App.ContextOfData.DBScanOptions.Properties.Count(x => x.IsUsed);
                worksheet.Cells[currentRowIndex, 3].Value = numProps;
                currentRowIndex++;
            }

            worksheet.Cells[currentRowIndex, 2].Value = "Количество используемых данных";
            var numDataInClasters = clasters.Sum(x => x.Items.Length);
            worksheet.Cells[currentRowIndex, 3].Value = numDataInClasters;
            currentRowIndex++;

            worksheet.Cells[currentRowIndex, 2].Value = "Количество данных, не попавших в метод";
            worksheet.Cells[currentRowIndex, 3].Value = (App.ContextOfData.Childrens_Info.Count() - numDataInClasters);
            currentRowIndex++;

            switch (App.ContextOfData.SelectedMethod)
            {
                case Method.Kohanen:
                    {
                        worksheet.Cells[currentRowIndex, 2].Value = "Скорость сходимости (p)";
                        worksheet.Cells[currentRowIndex, 3].Value = App.ContextOfData.KohanenOptions.V;
                        currentRowIndex++;
                        worksheet.Cells[currentRowIndex, 2].Value = "Граница разрыва (p)";
                        worksheet.Cells[currentRowIndex, 3].Value = App.ContextOfData.KohanenOptions.Rk;
                        currentRowIndex++;
                        worksheet.Cells[currentRowIndex, 2].Value = "Количество шагов (p)";
                        worksheet.Cells[currentRowIndex, 3].Value = App.ContextOfData.KohanenOptions.Steps;
                        currentRowIndex++;
                        break;
                    }
                case Method.DBScan:
                    {
                        worksheet.Cells[currentRowIndex, 2].Value = "Радиус сходимости (p)";
                        worksheet.Cells[currentRowIndex, 3].Value = App.ContextOfData.DBScanOptions.Eps;
                        currentRowIndex++;
                        worksheet.Cells[currentRowIndex, 2].Value = "Плотность точки (p)";
                        worksheet.Cells[currentRowIndex, 3].Value = App.ContextOfData.DBScanOptions.MinPts;
                        currentRowIndex++;
                        break;
                    }
                case Method.Classification:
                    {
                        worksheet.Cells[currentRowIndex, 2].Value = "Класс 1";
                        worksheet.Cells[currentRowIndex, 3].Value = "Отоакустическая эмиссия средне или высокочастотная, пороги ASSR 25 дБ и ниже, срок гестации до 33 недель." + Environment.NewLine +
                            "Необходимо еще 2 раза пройти исследования слуховой функций до 6 мес и в 1 год жизни";
                        worksheet.Cells[currentRowIndex, 3].Style.WrapText = true;
                        currentRowIndex++;

                        worksheet.Cells[currentRowIndex, 2].Value = "Класс 2";
                        worksheet.Cells[currentRowIndex, 3].Value = "Отоакустическая эмиссия средне или высокочастотная, пороги ASSR 25 дБ и ниже, срок гестации больше 33 недель." + Environment.NewLine +
                            "Обследование необходимо пройти однократно в период до 9 мес";
                        worksheet.Cells[currentRowIndex, 3].Style.WrapText = true;
                        currentRowIndex++;

                        worksheet.Cells[currentRowIndex, 2].Value = "Класс 3";
                        worksheet.Cells[currentRowIndex, 3].Value = "Отоакустическая эмиссия низкоамплитудная, пороги ASSR 25 дБ и ниже." + Environment.NewLine +
                            "Необходимо провести исследование слуховой функции в период от 3 до 6, и 6 до 12 месяцев.\nЕсли слуховая функция улучшается или становиться прежней, то необходимо проверить слух через 1 год";
                        worksheet.Cells[currentRowIndex, 3].Style.WrapText = true;
                        currentRowIndex++;

                        worksheet.Cells[currentRowIndex, 2].Value = "Класс 4";
                        worksheet.Cells[currentRowIndex, 3].Value = "Отоакустическая эмиссия средне или высокочастотная, пороги ASSR выше 25 дБ, гестация до 36 недель." + Environment.NewLine +
                            "Необходимо еще 2 раз пройти исследования слуховой функций до 9 мес." + Environment.NewLine +
                            "При отсутствии улучшения или ухудшении показателей ASSR (пороги становятся еще выше), дети направляются в Сурдологический центр.";
                        worksheet.Cells[currentRowIndex, 3].Style.WrapText = true;
                        currentRowIndex++;

                        worksheet.Cells[currentRowIndex, 2].Value = "Класс 5";
                        worksheet.Cells[currentRowIndex, 3].Value = "Отоакустическая эмиссия средне или высокочастотная, пороги ASSR выше 25 дБ, гесация от 37 недель." + Environment.NewLine +
                            "Необходимо еще 1 раз пройти исследования слуховой функций 6 мес и при отсутствии улучшения или ухудшении показателей ASSR (пороги становятся еще выше), он направляется в Сурдологический центр.";
                        worksheet.Cells[currentRowIndex, 3].Style.WrapText = true;
                        currentRowIndex++;

                        worksheet.Cells[currentRowIndex, 2].Value = "Класс 6";
                        worksheet.Cells[currentRowIndex, 3].Value = "Отоакустическая эмиссия низкоамплитудная, пороги ASSR выше 25 дБ и ниже, срок гестации до 36 недель." + Environment.NewLine +
                            "Необходимо еще 2 раз пройти исследования слуховой функций до 9 мес.\nПри отсутствии улучшения или ухудшении показателей ASSR (пороги становятся еще выше), дети направляются в Сурдологический центр.";
                        worksheet.Cells[currentRowIndex, 3].Style.WrapText = true;
                        currentRowIndex++;
                        break;
                    }
                default:
                    break;
            }

            if (App.ContextOfData.SelectedMethod != Method.Classification)
            {

                (new ClasterisationProvider()).CalculateRandIndex(clasters, out string distributedInfo, out double randIndex);
                worksheet.Cells[currentRowIndex, 2].Value = "Метрики";
                worksheet.Cells[currentRowIndex, 3].Value = distributedInfo;
                currentRowIndex++;

                worksheet.Cells[currentRowIndex, 2].Value = "Индекс Rand";
                worksheet.Cells[currentRowIndex, 3].Value = Math.Round(randIndex, 2);
                currentRowIndex++;

                worksheet.Cells[currentRowIndex, 2, currentRowIndex, 3].Merge = true;
                worksheet.Cells[currentRowIndex, 2].Value = "* (p) обозначает что поле отвечает за параметры кластеризации";
                worksheet.Cells[currentRowIndex, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[currentRowIndex, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            if (App.ContextOfData.SelectedMethod == Method.Classification)
            {
                worksheet.Column(3).Width = 200;
            }

            var range = worksheet.Cells[2, 2, currentRowIndex, 3];
            range.Style.Border.BorderAround(ExcelBorderStyle.Thick, Color.Black);

            for (int clastIndex = 0; clastIndex < clasters.Count; clastIndex++)
            {
                worksheet.Cells[3 + clastIndex, 5].Value = clasters[clastIndex].Name;
                worksheet.Cells[3 + clastIndex, 6].Value = clasters[clastIndex].Items.Length;
            }
            var chart = worksheet.Drawings.AddChart("PieChart", eChartType.Pie);
            chart.SetPosition(2, 0, 4, 0);
            chart.SetSize(600, 400);

            var serie = chart.Series.Add(
                worksheet.Cells[3, 6, clasters.Count + 2, 6],
                worksheet.Cells[3, 5, clasters.Count + 2, 5]
                );
            serie.Header = "Количество";
            chart.Title.Text = "Количественная диаграмма";
        }

        private static void GenerateAvarageDataInfo(ExcelPackage excelFile, List<ClasterInfo> clasters)
        {
            var worksheet = excelFile.Workbook.Worksheets.Add($"Сводная информация по кластерам/классам");

            worksheet.Cells[2, 2, 2, clasters.Count + 2].Merge = true;
            worksheet.Cells[2, 2].Value = $"Средние значения по кластерам/классам";
            worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            var avarageData = new List<double[]>();
            for (int clastIndex = 0; clastIndex < clasters.Count; clastIndex++)
            {
                avarageData.Add(new AvgCovidEars(clasters[clastIndex].Name, clasters[clastIndex].Items, clastIndex).GetData());
                worksheet.Cells[3, clastIndex + 3].Value = clasters[clastIndex].Name;
            }

            var clasterisationProps = App.ContextOfData.SelectedMethod == Method.Kohanen ?  App.ContextOfData.KohanenOptions.Properties : App.ContextOfData.DBScanOptions.Properties;
            for (int propId = 0; propId < clasterisationProps.Count; propId++)
            {
                var dataToInsert = new List<object>()
                {
                    clasterisationProps[propId].Name,
                };
                dataToInsert.AddRange(avarageData.Select(x => (object)x[propId]));

                var excelRow = new object[1][];
                excelRow[0] = dataToInsert.ToArray();

                worksheet.Cells[4 + propId, 2, 4 + propId, avarageData.Count + 2].LoadFromArrays(excelRow);
                worksheet.Cells[4 + propId, 3, 4 + propId, avarageData.Count + 2].Style.Numberformat.Format = "0.00";

                if (clasterisationProps[propId].IsUsed && App.ContextOfData.SelectedMethod != Method.Classification)
                {
                    var rowRange = worksheet.Cells[4 + propId, 2, 4 + propId, avarageData.Count + 2];
                    rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rowRange.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    rowRange.Style.Font.Bold = true;
                }
            }

            var currentCellNum = 3 + clasterisationProps.Count;
            if (App.ContextOfData.SelectedMethod != Method.Classification)
            {
                currentCellNum++;
                worksheet.Cells[currentCellNum, 2, currentCellNum, avarageData.Count + 2].Merge = true;
                worksheet.Cells[currentCellNum, 2].Value = "* желтым цветом выделены признаки по которым произведена кластеризация";
                worksheet.Cells[currentCellNum, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[currentCellNum, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            var range = worksheet.Cells[2, 2, currentCellNum, avarageData.Count + 2];
            range.Style.Border.BorderAround(ExcelBorderStyle.Thick, Color.Black);

            var mapperForDiagrams = new ClasterisationProvider().MainMapperForPropIds;
            int chartNumber = 0;
            foreach (var diagram in mapperForDiagrams)
            {
                var chart = worksheet.Drawings.AddChart(diagram.Key, eChartType.ColumnStacked);

                int row = 3 + (chartNumber / 2) * (300 / 15);
                int col = clasters.Count + 4 + (chartNumber % 2) * 8;

                chart.SetPosition(row, 0, col, 0);
                chart.SetSize(500, 300);
                chart.Title.Text = diagram.Key;

                var series = chart.Series.Add(
                    worksheet.Cells[4 + diagram.Value, 3, 4 + diagram.Value, avarageData.Count + 2],
                    worksheet.Cells[3, 3, 3, avarageData.Count + 2]
                    );

                chart.Legend.Remove();
                chartNumber++;
            }
        }

        private static void GenerateAiPage(ExcelPackage excelFile, List<ClasterInfo> clasters)
        {
            var worksheet = excelFile.Workbook.Worksheets.Add($"Характеристика ИИ по кластерам/классам");

            int colNumber = 2;
            foreach (var claster in clasters)
            {
                worksheet.Cells[2, colNumber].Value = claster.Name;
                worksheet.Cells[2, colNumber].Style.Font.Bold = true;
                worksheet.Cells[3, colNumber].Value = claster.DeepseekAnalysis;
                worksheet.Cells[3, colNumber].Style.WrapText = true;
                worksheet.Cells[3, colNumber].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Column(colNumber).Width = 90;
                colNumber++;
            }
        }

        private static Color GetColorForCells(ClasterisationProvider clExt, string[] data, int col, double age, double gest)
        {
            if (!Double.TryParse(data[col], out double value) || Double.IsNaN(value))
            {
                return Color.Transparent;
            }

            double[] range = clExt.SelectedPropNormal(col, age, gest);

            if (clExt.colomnsToCheck[0].Contains(col) || clExt.colomnsToCheck[1].Contains(col))
            {
                bool isForOtoacustic = clExt.colomnsToCheck[0].Contains(col);

                if (Math.Abs(value - range[0]) <= 1.5 && isForOtoacustic)
                    return ThreeValuesToColorConverter.SuchSmallerDrawingColor;
                else if (value < range[0] && isForOtoacustic)
                    return ThreeValuesToColorConverter.MoreSmallerDrawingColor;
                if (Math.Abs(value - range[1]) <= 1.5 && isForOtoacustic)
                    return ThreeValuesToColorConverter.SuchBiggerDrawingColor;
                else if (value > range[1] && isForOtoacustic)
                    return ThreeValuesToColorConverter.MoreBiggerDrawingColor;
                else if (value >= range[0] && value <= range[1])
                    return Color.FromArgb(255, 90, 255, 87);
                else
                    return ThreeValuesToColorConverter.MoreBiggerDrawingColor;
            }

            if (clExt.colomnsToCheck[2].Contains(col) || clExt.colomnsToCheck[3].Contains(col))
            {
                var indexOfProp = clExt.colomnsToCheck[2].Contains(col) ? clExt.colomnsToCheck[2].IndexOf(col) : clExt.colomnsToCheck[3].IndexOf(col);

                if (indexOfProp == 0)
                {
                    return value > 0 ? Color.FromArgb(255, 90, 255, 87) : ThreeValuesToColorConverter.MoreBiggerDrawingColor;
                }

                if (indexOfProp == 1)
                {
                    if (Double.TryParse(data[col - 1], out double prevValue) && prevValue > 0)
                    {
                        return Color.Transparent;
                    }

                    return value > 0 ? Color.FromArgb(255, 90, 255, 87) : ThreeValuesToColorConverter.MoreBiggerDrawingColor;
                }

                if (Double.TryParse(data[col - 2], out double firstValue) && firstValue > 0 ||
                    Double.TryParse(data[col - 1], out double secondValue) && secondValue > 0)
                {
                    return Color.Transparent;
                }

                return value > 0 ? Color.FromArgb(255, 90, 255, 87) : ThreeValuesToColorConverter.MoreBiggerDrawingColor;
            }

            return Color.Transparent;
        }
    }
}
