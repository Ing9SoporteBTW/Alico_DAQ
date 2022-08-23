using System.ComponentModel.DataAnnotations.Schema;

namespace EpicorBTWRest.DataAccess.Models.TimeReport
{
    [Table("ControlPerdidos")]
    public class ControlPerdidos
    {
        public int ContolPerdidosid { get; set; }
        public int id{ get; set; }
        public string jobnum { get; set; }
        public string ResourceID { get; set; }
        public string EmployeeNum { get; set; }
        public DateTime Entrada { get; set; }// Tiempo de inicio
        public int Oper { get; set; }
        public string Codi_Ejecu { get; set; } // codigo de ejecución
        public string Codi_Paro { get; set; } // codigo de paro 
        public string QtlyFinal { get; set; }
        public int posicion { get; set; }
        public int total { get; set; }

    }
}
