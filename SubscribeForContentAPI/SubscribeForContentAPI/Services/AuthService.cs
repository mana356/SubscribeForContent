using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SubscribeForContentAPI.Services.Contracts;
using System.Security.Claims;

namespace SubscribeForContentAPI.Services
{
    public class AuthService : IAuthService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) 
        { 
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfile> GetLoggedInUser()
        {
            var firebaseUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userProfile = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.FirebaseUserId == firebaseUserId);
            return userProfile;
        }

        public string GetFirebaseUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
