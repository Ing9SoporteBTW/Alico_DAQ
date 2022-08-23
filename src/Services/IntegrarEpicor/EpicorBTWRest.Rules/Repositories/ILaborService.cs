using EpicorBTWRest.DataAccess.DataContext;
using SharedService.Models;
using SharedService.Models.Epicor.BOLabor;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Repositories
{
    public interface ILaborService
    {
        Task<PetitionResponse> GetNewLaborDtlNoHdr (Labor labor);

        Task<PetitionResponse> CreateTimeDirectInInEpicor(IntermediaContext _context, EpicorCredentials epicorCredentials);
    }
}
