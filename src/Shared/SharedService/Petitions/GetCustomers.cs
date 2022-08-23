using SharedService.Models;
using System.ComponentModel.DataAnnotations;

namespace SharedService.Petitions
{
    public class GetCustomers
    {

        [Required]
        public string CompanyID { get; set; }
        [Required]
        public EpicorCredentials EpicorCredentials { get; set; }
    }
}
