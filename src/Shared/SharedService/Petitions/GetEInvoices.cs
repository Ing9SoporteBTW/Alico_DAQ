using SharedService.Models;
using System.ComponentModel.DataAnnotations;

namespace SharedService.Petitions
{
    public class GetEInvoices
    {
        [Required]
        public string ClienteID { get; set; }
        [Required]
        public string CompanyID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string CustID { get; set; }
        public string EstadoTimbre { get; set; }
    }
}
