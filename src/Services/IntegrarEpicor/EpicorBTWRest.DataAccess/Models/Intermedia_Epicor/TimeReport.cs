using EpicorBTWRest.DataAccess.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicorBTWRest.DataAccess.Models.TimeReport
{
    public class TimeReport
    {
        public string TimeReport_CodigoEjecucion { get; set; }
        public string TimeReport_CodigoParo { get; set; }
        public string TimeReport_Empleado { get; set; }
        public int TimeReport_Ensamble { get; set; }
        public EstadosMaquina Calculated_Estado { get; set; }
        public DateTime TimeReport_FechaFinal { get; set; }
        public DateTime TimeReport_FechaInicial { get; set; }
        public string TimeReport_GrupoRecurso { get; set; }
        public int TimeReport_Id { get; set; }
        public string TimeReport_Maquina { get; set; }
        public int TimeReport_Operacion { get; set; }
        public string TimeReport_QtyFinal { get; set; }
        public string TimeReport_Recurso { get; set; }
        public bool TimeReport_Sincronizado { get; set; }
        public string TimeReport_TiempoPerdido { get; set; }
        public string TimeReport_TipoLabor { get; set; }
        public string TimeReport_Trabajo { get; set; }
    }
}
