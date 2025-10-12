using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Interfaces
{
    public interface IClasterisation
    {
        public List<ClasterInfo> Clasters { get; set; }
    }
}
