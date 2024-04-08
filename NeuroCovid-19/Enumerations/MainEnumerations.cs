using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Enumerations
{
    public enum Clasterisation
    {
        /// <summary>
        /// Метод Кохонена
        /// </summary>
        Kohanen = 0,
        /// <summary>
        /// Метод DBScan
        /// </summary>
        DBScan = 1,
    }
}
