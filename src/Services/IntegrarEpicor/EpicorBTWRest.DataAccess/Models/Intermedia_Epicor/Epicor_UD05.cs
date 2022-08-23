using System.ComponentModel.DataAnnotations.Schema;

namespace EpicorBTWRest.DataAccess.Models.TimeReport
{
    [Table("EpicorUD05")]
    public class Epicor_UD05
    {
        public int epicorud05id { get; set; }
        public int id { get; set; }
        public string jobnum { get; set; }
        public string ResourceID { get; set; }
        public string EmployeeNum { get; set; }
        public DateTime Entrada { get; set; }// Tiempo de inicio
        public DateTime Final { get; set; } //Tiempo de terminación 
        public int Oper { get; set; }
        public string Codi_Ejecu { get; set; } // codigo de ejecución
        public string Codi_Paro { get; set; } // codigo de paro 
        public string QtlyFinal { get; set; }
        public int Ensamble { get; set; }
        public string GrupoRecurso { get; set; }
        public string Maquina { get; set; }
        public string TiempoPerdido { get; set; }
        public string TipoLabor { get; set; }

        public bool Sincronizado { get; set; }
        public int Posicion1 { get; set; }
        public int Consecutivo { get; set; }
        public bool OnlyCreated { get; set; }
        public bool OnlyUpdate { get; set; }
        public bool Closed { get; set; }
    }
}
