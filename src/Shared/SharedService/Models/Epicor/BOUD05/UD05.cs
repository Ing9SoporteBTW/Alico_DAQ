using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Models.Epicor.BOUD05
{
    public class UD05
    {
        public Erp.Tablesets.LaborTableset ds { get; set; }
        public EpicorCredentials EpicorCredentials { get; set; }
    }

    public class GetNewUD05_input
    {
        public string UD05 { get; set; }
    }
}
