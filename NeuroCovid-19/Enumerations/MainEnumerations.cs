using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Enumerations
{
    public enum Method
    {
        /// <summary>
        /// Метод Кохонена
        /// </summary>
        Kohanen = 0,
        /// <summary>
        /// Метод DBScan
        /// </summary>
        DBScan = 1,

        /// <summary>
        /// Классификация
        /// </summary>
        Classification = 2,
    }
}
