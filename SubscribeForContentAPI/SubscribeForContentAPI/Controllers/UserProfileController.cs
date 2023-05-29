using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SFC_DTO.FileContent;
using SFC_DTO.Post;
using SFC_DTO.UserProfile;
using SubscribeForContentAPI.Services.Contracts;
using System.Security.Claims;

namespace SubscribeForContentAPI.Controllers
{
    [ApiController]
    [Route("api/v1/Users")]
    public class UserProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorage _blobStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProfileController(IMapper mapper, IUnitOfWork unitOfWork, IBlobStorage blobStorage, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _blobStorage = blobStorage;
            _httpContextAccessor = httpContextAccessor;
        }

        
        [Authorize]
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var userProfileEntity = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(p => p.FirebaseUserId == id, "SubscriptionLevels,Subscriptions,Subscribers,ProfilePicture,CoverPicture");

            if (userProfileEntity == null)
            {
                return NotFound();
            }
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var dataToReturn = _mapper.Map<UserProfileDTO>(userProfileEntity);
            if (userProfileEntity.ProfilePicture != null && dataToReturn.ProfilePicture != null)
            {
                var url = await _blobStorage.GetSasUrlAsync(userProfileEntity.ProfilePicture.ContainerName, userProfileEntity.ProfilePicture.BlobId);
                dataToReturn.ProfilePicture.Url = url;
            }
            if (userProfileEntity.CoverPicture != null && dataToReturn.CoverPicture != null)
            {
                var url = await _blobStorage.GetSasUrlAsync(userProfileEntity.CoverPicture.ContainerName, userProfileEntity.CoverPicture.BlobId);
                dataToReturn.CoverPicture.Url = url;
            }

            return Ok(dataToReturn);
        }

        [Authorize]
        [HttpPost("CreateUserIfDoesNotExist")]
        public async Task<IActionResult> CreateUser(UserProfileCreationDTO userProfileCreationDTO)
        {
            var firebaseUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(firebaseUserId))
            {
                var existingUser = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.FirebaseUserId == firebaseUserId);
                if (existingUser == null)
                {
                    var userProfileEntity = _mapper.Map<UserProfile>(userProfileCreationDTO);
                    userProfileEntity.FirebaseUserId = firebaseUserId;

                    _unitOfWork.UserProfileRepository.Add(userProfileEntity);
                    await _unitOfWork.SaveAsync();

                    return NoContent();
                }
            }
            return NoContent();
        }
    }
}
