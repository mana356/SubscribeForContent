using SFC_DataAccess.Data;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;

namespace SFC_DataAccess.Repository
{

    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        readonly SFCDBContext _context;

        public UserProfileRepository(SFCDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
