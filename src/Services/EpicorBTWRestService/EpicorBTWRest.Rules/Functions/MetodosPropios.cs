using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicorBTWRest.Rules.Functions
{
    public static class MetodosPropios
    {
        public static void cargaVariablesLocales(DateTime FechaInical, out decimal DHoraInicial, out decimal DHoraFinal, out decimal TiempoHrs, out string HoraI, out string HoraF)
        {

            DHoraInicial = Convert.ToDecimal(TimeSpan.Parse(FechaInical.ToString("HH:mm")).TotalHours);
            DHoraFinal = Convert.ToDecimal(TimeSpan.Parse(FechaInical.ToString("HH:mm")).TotalHours);

            decimal hoursI = Math.Floor(DHoraInicial);
            decimal minutesI = (DHoraInicial - hoursI) * 60.0M;
            decimal MI = (decimal)Math.Round(minutesI);
            decimal hoursF = Math.Floor(DHoraFinal);
            decimal minutesF = (DHoraFinal - hoursF) * 60.0M;
            decimal MF = (decimal)Math.Round(minutesF);

            HoraI = String.Format("{0:00}:{1:00}", hoursI, MI);
            HoraF = String.Format("{0:00}:{1:00}", hoursF, MF);
            TiempoHrs = DHoraFinal - DHoraInicial;

        }

        //public static void CierreDia(DateTime FechaInicial, DateTime dtmTime1, DateTime dtmTime2)
        //{

        //    //Se debe de hacer uso de la función cargaVariablesLocales
        //}


    }
}
