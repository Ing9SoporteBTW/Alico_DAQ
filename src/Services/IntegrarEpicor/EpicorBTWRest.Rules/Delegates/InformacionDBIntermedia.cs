using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.DataAccess.Models.TimeReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EpicorBTWRest.Rules.Delegates
{
    public class InformacionDBIntermedia
    {
        public static async Task<bool> TraerTrabajos(IntermediaContext context,ILogger log)
        {
            List<Epicor_UD05> ud05 = new List<Epicor_UD05>();
            List<Epicor_Labor> labor = new List<Epicor_Labor>();

            // TRAER 
            //-- Para la tabla Labor
            var LaborSincronizado = await context.Epicor_Labors
                    .AsNoTracking()
                        .Where(x => x.Sincronizado==false).ToListAsync();

            //-- Para la tabla UD05
            var UD05Sincronizado = await context.Epicor_UD05s
                    .AsNoTracking()
                        .Where(x => x.Sincronizado==false).ToListAsync();


            foreach (var lab in LaborSincronizado)
            {
                labor.Add(lab);
            }
            foreach (var ud in UD05Sincronizado)
            {
                ud05.Add(ud);
            }

            return true;
        }
    }
}
