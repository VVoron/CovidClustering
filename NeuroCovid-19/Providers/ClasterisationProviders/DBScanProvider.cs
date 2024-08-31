using NeuroCovid19.Extensions;
using NeuroCovid19.Interfaces;
using NeuroCovid19.MVVM.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;

namespace NeuroCovid19.Providers
{
    public class DBScanProvider : IClasterisation
    {
        private double _minPts;
        private double _eps;
        private List<DBScanPoint> _allPoints;

        public List<DataCOVIDEars[]> Clasters { get; set; }

        public DBScanProvider() { }

        public DBScanProvider(List<DataCOVIDEars> data, double[,] dataPoints)
        {
            _eps = App.ContextOfData.DBScanOptions.Eps;
            _minPts = App.ContextOfData.DBScanOptions.MinPts;

            _allPoints = new List<DBScanPoint>();
            Clasters = new List<DataCOVIDEars[]>();

            for (int i = 0; i < dataPoints.GetLength(0); i++)
            {
                _allPoints.Add(new DBScanPoint(data[i], dataPoints.GetArrayLine(i)));
            }

            int numClasters = 0;
            foreach (var p in _allPoints)
            {
                if (p.Claster != null)
                    continue;

                var neigh = GetNeighbors(p, _eps);
                if (neigh.Count < _minPts)
                {
                    p.Claster = 0;
                    continue;
                }

                numClasters++;
                p.Claster = numClasters;
                CheckAllNeighbors(neigh, numClasters);
            }

            // Первый кластер - шум
            for (int i = 0; i <= numClasters; i++)
            {
                Clasters.Add(_allPoints.Where(x => x.Claster == i)
                                       .Select(x => x.data).ToArray());
            }

            App.ContextOfData.DBScanOptions.ClastersInfo = Clasters;
        }

        private void CheckAllNeighbors(List<DBScanPoint> points, int clastNumber)
        {
            foreach (var point in points)
            {
                if (point.Claster == 0)
                    point.Claster = clastNumber;

                if (point.Claster != null)
                    continue;

                point.Claster = clastNumber;
                var newNeigh = GetNeighbors(point, _eps);
                if (newNeigh.Count >= _minPts)
                    CheckAllNeighbors(newNeigh, clastNumber);
            }
        }

        public List<DBScanPoint> GetNeighbors(DBScanPoint currentPoint, double eps)
        {
            return _allPoints.Where(x => currentPoint.CheckForNeighbor(x, eps)).ToList();
        }

        public bool CheckForError()
        {
            return App.ContextOfData.DBScanOptions.MinPts == null ||
                   App.ContextOfData.DBScanOptions.Eps == null ||
                   !App.ContextOfData.DBScanOptions.Properties.Any(x => x.IsUsed);
        }
    }
}
