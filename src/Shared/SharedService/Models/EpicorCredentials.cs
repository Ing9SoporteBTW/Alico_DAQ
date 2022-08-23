using System.ComponentModel.DataAnnotations;

namespace SharedService.Models
{
    public class EpicorCredentials
    {
        [Required]
        //URL Epicor
        public string EpicorAPI { get; set; }
        //APIKey para conectarnos al API del ERP
        public string APIKey { get; set; }
        [Required]
        //Usuario del ERP
        public string Username { get; set; }
        [Required]
        //Contraseña del usuario del ERP
        public string Password { get; set; }

    }
}
