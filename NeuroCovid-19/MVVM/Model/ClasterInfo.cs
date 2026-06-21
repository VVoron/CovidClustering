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
                    "возраст на момент болезни ковидом",
                    "срок беременности, на котором мать переболела ковидом (в неделях)",
                    "срок гестации (в неделях)",
                    "возраст на момент обследования",
                        "ОАЭ правого уха (среднее)",
                        "ОАЭ правого уха (максимальное)",
                        "ОАЭ правого уха (количество не пройденных обследования)",
                        "ОАЭ левого уха (среднее)",
                        "ОАЭ левого уха (максимальное)",
                        "ОАЭ левого уха (количество не пройденных обследования)",
                        "ASSR правого уха (среднее)",
                        "ASSR левого уха (среднее)",
                ],
                Items.Select(x => x.DataForAI()).ToList()
            );
        }
    }
}
