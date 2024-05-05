using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Extensions
{
    public static class BaseExtensions
    {
        public static string GetProperyNameById<T>(int id)
        {
            var propertyInfo = typeof(T).GetProperties()[id];
            return propertyInfo.Name;
        }

        public static double GetPropertyValueById<T>(this T obj, int id)
        {
            var propertyInfo = typeof(T).GetProperties()[id];
            return Convert.ToDouble(propertyInfo.GetValue(obj));
        }

        public static T[] GetArrayLine<T>(this T[,] array, int lineIndex)
        {
            T[] line = new T[array.GetLength(1)];
            for (int i = 0; i < array.GetLength(1); i++)
                line[i] = array[lineIndex, i];

            return line;
        }

        public static void InsertValueOfDouble<T>(this T obj, int id, double value)
        {
            var propertyInfo = typeof(T).GetProperties()[id];
            propertyInfo.SetValue(obj, value);
        }
    }
}
