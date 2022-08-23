using System.ComponentModel.DataAnnotations;

namespace SharedService.Models.Epicor.BOLabor
{
    public class Labor
    {
        public Erp.Tablesets.LaborTableset ds { get; set; }

        public EpicorCredentials EpicorCredentials { get; set; }
    }
}
