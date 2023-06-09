using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SFC_DTO.FileContent;
using SFC_DTO.Post;
using SFC_DTO.UserProfile;
using SFC_Utility;
using SubscribeForContentAPI.Services.Contracts;
using System.Runtime.Intrinsics.Arm;
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
        private readonly IAuthService _authService;
        private readonly UsernameGenerator _usernameGenerator;

        public UserProfileController(IMapper mapper, IUnitOfWork unitOfWork, IBlobStorage blobStorage, IAuthService authService, UsernameGenerator usernameGenerator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _blobStorage = blobStorage;
            _authService = authService;
            _usernameGenerator = usernameGenerator;
        }

        
        [Authorize]
        [HttpGet("{username}", Name = "GetUserByUserName")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var userProfileEntity = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(p => p.UserName == username || p.FirebaseUserId == username, "SubscriptionLevels,Subscriptions,Subscribers,ProfilePicture,CoverPicture");

            if (userProfileEntity == null)
            {
                return NotFound();
            }
            var userId = _authService.GetFirebaseUserId();

            var dataToReturn = _mapper.Map<UserProfileDTO>(userProfileEntity);
            if (userProfileEntity.ProfilePicture != null && dataToReturn.ProfilePicture != null)
            {
                dataToReturn.ProfilePicture.Url = await UpdatePictureLink(userProfileEntity.ProfilePicture);
            }
            if (userProfileEntity.CoverPicture != null && dataToReturn.CoverPicture != null)
            {
                dataToReturn.CoverPicture.Url = await UpdatePictureLink(userProfileEntity.CoverPicture);
            }

            return Ok(dataToReturn);
        }

        [Authorize]
        [HttpPost("CreateUserIfDoesNotExist")]
        public async Task<IActionResult> CreateUser()
        {
            var firebaseUserId = _authService.GetFirebaseUserId();


            if (!string.IsNullOrEmpty(firebaseUserId))
            {
                var existingUser = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.FirebaseUserId == firebaseUserId);
                if (existingUser == null)
                {
                    var email = User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value ?? "";
                    var name = User?.Claims?.FirstOrDefault(claim => claim.Type == "name")?.Value ?? email.Split('@')[0];                    
                    var profilePicUrl = User?.Claims?.FirstOrDefault(claim => claim.Type == "picture")?.Value;

                    var uniqueUserName = string.Empty;

                    //get a unique username for the user
                    while (uniqueUserName == string.Empty)
                    {
                        var newUserName = _usernameGenerator.GenerateName(false);
                        var existingUserName = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.UserName == newUserName);
                        if (existingUserName == null)
                        {
                            uniqueUserName = newUserName;
                        }
                    }

                    var userProfileEntity = new UserProfile()
                    {
                        Name = name,
                        Email = email,
                        FirebaseUserId = firebaseUserId,
                        UserName = uniqueUserName
                    };

                    if (!string.IsNullOrEmpty(profilePicUrl))
                    {
                        var fileContent = new FileContent()
                        {
                            Name = $"Profile Picture - {name}",
                            Type = Constants.ProfilePicContainer,
                            ContainerName = Constants.ProfilePicContainer,
                            Extension = ".png"
                        };


                        var client = new HttpClient();
                        var response = await client.GetAsync(profilePicUrl);

                        var content = await response.Content.ReadAsStreamAsync();
                        fileContent.BlobId = await _blobStorage.UploadFileAsync(fileContent.ContainerName, content, fileContent.Extension);
                        _unitOfWork.FileContentRepository.Add(fileContent);
                        userProfileEntity.ProfilePicture = fileContent;
                    }

                    _unitOfWork.UserProfileRepository.Add(userProfileEntity);
                    await _unitOfWork.SaveAsync();

                    return NoContent();
                }
            }
            return NoContent();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromForm] UserProfileUpdateDTO userProfileUpdateDto)
        {
            var firebaseUserId = _authService.GetFirebaseUserId();
            if (string.IsNullOrEmpty(firebaseUserId))
            {
                return NotFound();
            }

            var existingUser = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.FirebaseUserId == firebaseUserId);
            if (existingUser == null)
            {
                return NotFound();
            }

            _mapper.Map(userProfileUpdateDto, existingUser);

            if (userProfileUpdateDto.ProfilePicture != null)
            {
                var profilePicFile = new {

                    fileContent = new FileContent()
                    {
                        Name = Path.GetFileNameWithoutExtension(userProfileUpdateDto.ProfilePicture.FileName),
                        Type = Constants.ProfilePicContainer,
                        Extension = Path.GetExtension(userProfileUpdateDto.ProfilePicture.FileName)
                    }
                    ,
                    file = userProfileUpdateDto.ProfilePicture
                };

                profilePicFile.fileContent.ContainerName = Constants.ProfilePicContainer;
                profilePicFile.fileContent.BlobId = await _blobStorage.UploadFileAsync(profilePicFile.fileContent.ContainerName, profilePicFile.file.OpenReadStream(), profilePicFile.fileContent.Extension);

                _unitOfWork.FileContentRepository.Add(profilePicFile.fileContent);
                if (existingUser.ProfilePicture != null)
                { 
                    await _blobStorage.DeleteBlob(existingUser.ProfilePicture.ContainerName, existingUser.ProfilePicture.BlobId);
                    _unitOfWork.FileContentRepository.Remove(existingUser.ProfilePicture.Id);
                }
                existingUser.ProfilePicture = profilePicFile.fileContent;
            }
            if (userProfileUpdateDto.CoverPicture != null)
            {
                var coverPicFile = new
                {

                    fileContent = new FileContent()
                    {
                        Name = Path.GetFileNameWithoutExtension(userProfileUpdateDto.CoverPicture.FileName),
                        Type = Constants.ProfileCoverPicContainer,
                        Extension = Path.GetExtension(userProfileUpdateDto.CoverPicture.FileName)
                    }
                    ,
                    file = userProfileUpdateDto.ProfilePicture
                };

                coverPicFile.fileContent.ContainerName = Constants.ProfileCoverPicContainer;
                coverPicFile.fileContent.BlobId = await _blobStorage.UploadFileAsync(coverPicFile.fileContent.ContainerName, coverPicFile.file.OpenReadStream(), coverPicFile.fileContent.Extension);

                _unitOfWork.FileContentRepository.Add(coverPicFile.fileContent);
                if (existingUser.CoverPicture != null)
                {
                    await _blobStorage.DeleteBlob(existingUser.CoverPicture.ContainerName, existingUser.CoverPicture.BlobId);
                    _unitOfWork.FileContentRepository.Remove(existingUser.CoverPicture.Id);
                }
                existingUser.CoverPicture = coverPicFile.fileContent;
            }

            await _unitOfWork.SaveAsync();
            var dataToReturn = _mapper.Map<UserProfileDTO>(existingUser);
            if (existingUser.ProfilePicture != null && dataToReturn.ProfilePicture != null)
            {
                dataToReturn.ProfilePicture.Url = await UpdatePictureLink(existingUser.ProfilePicture);
            }
            if (existingUser.CoverPicture != null && dataToReturn.CoverPicture != null)
            {
                dataToReturn.CoverPicture.Url = await UpdatePictureLink(existingUser.CoverPicture);
            }

            return Ok(dataToReturn);
        }

        private async Task<string> UpdatePictureLink(FileContent fileContent)
        {
            return await _blobStorage.GetSasUrlAsync(fileContent.ContainerName, fileContent.BlobId);
        }
    }
}
