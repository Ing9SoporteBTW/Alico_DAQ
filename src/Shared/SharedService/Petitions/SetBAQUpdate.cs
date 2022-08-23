using System.ComponentModel.DataAnnotations;

namespace SharedService.Petitions
{
    public class SetBAQUpdate
    {
        [Required]
        public string BAQName { get; set; }
        [Required]
        public string ClienteID { get; set; }
        [Required]
        public string CompanyID { get; set; }
        [Required]
        public object BAQData { get; set; }
    }
}
