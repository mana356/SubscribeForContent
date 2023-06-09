using SFC_DataEntities.Entities;

namespace SubscribeForContentAPI.Services.Contracts
{
    public interface IAuthService
    {
        Task<UserProfile> GetLoggedInUser();
        string GetFirebaseUserId();
    }
}
