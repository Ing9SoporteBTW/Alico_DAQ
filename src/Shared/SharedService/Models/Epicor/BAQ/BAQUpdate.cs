using System.ComponentModel.DataAnnotations;

namespace SharedService.Models.Epicor.BAQ
{
    public class BAQUpdate
    {
        [Required]
        public string BAQName { get; set; }
        [Required]
        public string CompanyID { get; set; }
        [Required]
        public object BAQData { get; set; }
        [Required]
        public EpicorCredentials EpicorCredentials { get; set; }
    }
}
