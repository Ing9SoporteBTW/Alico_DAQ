using System.ComponentModel.DataAnnotations;

namespace SharedService.Petitions
{
    public class GetBAQConsult
    {
        [Required]
        public string BAQName { get; set; }
        [Required]
        public string ClienteID { get; set; }
        [Required]
        public string CompanyID { get; set; }
        public string parameters { get; set; } = string.Empty;
        public string select { get; set; } = string.Empty;
        public string filter { get; set; } = string.Empty;
        public string orderby { get; set; } = string.Empty;
        public string top { get; set; } = string.Empty;
        public string skip { get; set; } = string.Empty;
        public string inlinecount { get; set; } = string.Empty;
    }
}
