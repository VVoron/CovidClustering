using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class ClasterInfo
    {
        public ClasterInfo()
        {

        }

        public string Name { get; set; }
        public DataCOVIDEars[] Items { get; set; }
        public string DeepseekAnalysis { get; set; }

        public Tuple<string[], List<string[]>> GetDataForAI()
        {
            return new Tuple<string[], List<string[]>>(
                [
                    "возраст на момент обследования",
                        "ОАЭ правого уха 1кГц",
                        "ОАЭ правого уха 2кГц",
                        "ОАЭ правого уха 4кГц",
                        "ОАЭ правого уха 6кГц",
                        "ОАЭ левого уха 1кГц",
                        "ОАЭ левого уха 2кГц",
                        "ОАЭ левого уха 4кГц",
                        "ОАЭ левого уха 6кГц",
                        "ASSR правого уха 500Гц",
                        "ASSR правого уха 1000Гц",
                        "ASSR правого уха 2000Гц",
                        "ASSR правого уха 4000Гц",
                        "ASSR левого уха 500Гц",
                        "ASSR левого уха 1000Гц",
                        "ASSR левого уха 2000Гц",
                        "ASSR левого уха 4000Гц",
                        "КСВП правого уха 20",
                        "КСВП правого уха 40",
                        "КСВП правого уха 60",
                        "КСВП левого уха 20",
                        "КСВП левого уха 40",
                        "КСВП левого уха 60"
                ],
                Items.Select(x => x.DataForAI).ToList()
            );
        }
    }
}
