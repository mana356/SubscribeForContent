using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SFC_DTO.FileContent;
using SFC_DTO.Post;
using SubscribeForContentAPI.Services.Contracts;
using System.Security.Claims;

namespace SubscribeForContentAPI.Controllers
{
    [ApiController]
    [Route("api/v1/Posts")]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorage _blobStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostController(IMapper mapper, IUnitOfWork unitOfWork, IBlobStorage blobStorage, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _blobStorage = blobStorage;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] PostSearchFilter queryFilter = null)
        {
            var posts = await _unitOfWork.PostRepository.GetPostsAsync(queryFilter);
            var postResults = _mapper.Map<List<PostSummaryDTO>>(posts);
            foreach (var post in posts)
            {
                if (post.FileContents != null && post.FileContents.Any())
                {
                    foreach (var row in post.FileContents)
                    {
                        var url = await _blobStorage.GetSasUrlAsync(row.ContainerName, row.BlobId);
                        var recordToUpdate = postResults.FirstOrDefault(p => p.Id == post.Id);
                        var fileContent = recordToUpdate?.FileContents.FirstOrDefault(f => f.Id == row.Id);
                        if (fileContent != null)
                        { 
                            fileContent.Url = url; 
                        }
                    }
                }
            }
            return Ok(postResults);
        }

        [Authorize]
        [HttpGet("{id}", Name = "PostLink")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var postEntity = await _unitOfWork.PostRepository.GetFirstOrDefaultAsync(p => p.Id == id, "Creator,CreatorSubscriptionLevel,LikedByUsers,PostComments,FileContents");

            if (postEntity == null)
            {
                return NotFound();
            }
            

            var dataToReturn = _mapper.Map<PostDTO>(postEntity);
            if (postEntity.FileContents != null && postEntity.FileContents.Any())
            {
                foreach (var row in postEntity.FileContents)
                {
                    var url = await _blobStorage.GetSasUrlAsync(row.ContainerName, row.BlobId);
                    var recordToUpdate = dataToReturn.FileContents.FirstOrDefault(f => f.Id == row.Id);
                    recordToUpdate.Url = url;
                }            
            }

            return Ok(dataToReturn);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAPost([FromForm] PostCreationDTO postCreationDTO)
        {
            var postEntity = _mapper.Map<Post>(postCreationDTO);

            if (postCreationDTO.FileContents != null)
            {
                var files = postCreationDTO.FileContents.Select(f=> new { 
                
                    fileContent = new FileContent()
                    { 
                        Name = Path.GetFileNameWithoutExtension(f.FileName),
                        Type = "PostContent",
                        Extension = Path.GetExtension(f.FileName)
                    }
                    ,file = f
                }).ToList();
                
                foreach (var item in files)
                {
                    item.fileContent.ContainerName = "PostContent";
                    item.fileContent.BlobId = await _blobStorage.UploadFileAsync(item.fileContent.ContainerName, item.file.OpenReadStream(), item.fileContent.Extension);
                }

                _unitOfWork.FileContentRepository.AddRange(files.Select(f => f.fileContent).ToList());
                postEntity.FileContents = files.Select(f => f.fileContent).ToList();
            }
            var user = await GetLoggedInUser();
            postEntity.CreatorId = user.Id;

            _unitOfWork.PostRepository.Add(postEntity);
            await _unitOfWork.SaveAsync();

            var dataToReturn = _mapper.Map<PostDTO>(postEntity);

            return CreatedAtRoute(routeName: "PostLink", routeValues: new { id = postEntity.Id }, value: dataToReturn);
        }

        private async Task<UserProfile> GetLoggedInUser()
        {
            var firebaseUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userProfile = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.FirebaseUserId == firebaseUserId);
            return userProfile;
        }
    }
}
