using SFC_DataEntities.Entities;

namespace SFC_DataAccess.Repository.Contracts
{
    public interface ISubscriptionLevelRepository : IRepository<CreatorSubscriptionLevel>
    {
        Task<List<CreatorSubscriptionLevel>> GetSubscriptionLevelsOfCreator(string username);
    }
}
