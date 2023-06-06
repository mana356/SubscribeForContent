using Microsoft.EntityFrameworkCore;
using SFC_DataAccess.Data;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;

namespace SFC_DataAccess.Repository
{

    public class SubscriptionLevelRepository : Repository<CreatorSubscriptionLevel>, ISubscriptionLevelRepository
    {
        readonly SFCDBContext _context;

        public SubscriptionLevelRepository(SFCDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CreatorSubscriptionLevel>> GetSubscriptionLevelsOfCreator(string username)
        {
            var results = await _context.CreatorSubscriptionLevel
                .Include(x => x.Creator)
                .Where(c => c.Creator.UserName != null && c.Creator.UserName.ToLower() == username.ToLower())
                .ToListAsync();

            return results;
        }
    }
}
