using EpicorBTWRest.DataAccess.DataContext;
using SharedService.Models;
using SharedService.Models.Epicor.BOUD05;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Repositories
{
    public interface IUD05Service
    {
        Task<PetitionResponse> GetaNewUD05(UD05 ud05);
        Task<PetitionResponse> CreateLostTimeInEpicor(IntermediaContext _context, EpicorCredentials epicorCredentials);
    }
}


