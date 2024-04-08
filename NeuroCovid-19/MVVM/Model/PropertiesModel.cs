using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class PropertiesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUsed { get; set; }
        public double Coef { get; set; }
    }
}
