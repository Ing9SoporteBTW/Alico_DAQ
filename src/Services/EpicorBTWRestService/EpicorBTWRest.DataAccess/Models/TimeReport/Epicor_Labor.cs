using System.ComponentModel.DataAnnotations.Schema;

namespace EpicorBTWRest.DataAccess.Models.TimeReport
{
    [Table("EpicorLabor")]
    public class Epicor_Labor
    {
        public int epicorlaborid { get; set; }
        public int id { get; set; }
        public string jobnum { get; set; }
        public string LaborType { get; set; }
        public decimal laborhrs { get; set; }
        public string ResourceID { get; set; }
        public string EmployeeNum { get; set; }
        public decimal ClockOutTime { get; set; }
        public decimal ClockinTime { get; set; }
        public string DspClockInTime { get; set; }
        public string DspClockOutTime { get; set; }
        public int OprSeq { get; set; }
        public DateTime ClockInDate { get; set; }
        public string LaborNote { get; set; }
        public bool Sincronizado { get; set; }
        public string CodigoEjecucion { get; set; }
        public string CodigoParo { get; set; }
        public int Ensamble { get; set; }
        public string Maquina { get; set; }
        public string Labor_QtyFinal { get; set; }
        public string TiempoPerdido { get; set; }
        public string GrupoRecurso { get; set; }

        public bool OnlyCreated { get; set; }
        public bool OnlyUpdate { get; set; }
        public bool Closed { get; set; }
        public int Posicion { get; set; }
        public int Consecutivo { get; set; }
    }
}
