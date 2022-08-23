using EpicorBTWRest.DataAccess.DataContext;
using SharedService.Models.Epicor.BAQ;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Repositories
{
    public interface IBAQService
    {
        Task<PetitionResponse> BAQConsult(BAQConsult baqConsult);
        Task<PetitionResponse> TestConsumo(IntermediaContext context,BAQConsult baqConsult);
        //Task<PetitionResponse> BAQUpdate(BAQUpdate baqUpdate);
    }
}
