using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class DBScanPoint
    {
        public DataCOVIDEars data { get; set; }
        public double[] Coordinates;
        public int? Claster;
        public DBScanPoint(DataCOVIDEars item, double[] coordinates)
        {
            data = item;
            Coordinates = coordinates;
        }

        private double GetDistance(DBScanPoint point)
        {
            double distance = 0;

            for (int i = 0; i < this.Coordinates.Length; i++)
                distance += Math.Pow(point.Coordinates[i] - this.Coordinates[i], 2);

            return Math.Sqrt(distance);
        }

        public bool CheckForNeighbor(DBScanPoint point, double eps)
        {
            return Math.Round(GetDistance(point), 2) <= eps;
        }
    }
}
