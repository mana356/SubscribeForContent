using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SFC_DTO.FileContent;
using SFC_DTO.Post;
using SFC_Utility;
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
        private readonly IAuthService _authService;

        public PostController(IMapper mapper, IUnitOfWork unitOfWork, IBlobStorage blobStorage, IAuthService authService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _blobStorage = blobStorage;
            _authService = authService;
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
                        var recordToUpdate = postResults.FirstOrDefault(p => p.Id == post.Id);
                        var fileContent = recordToUpdate?.FileContents.FirstOrDefault(f => f.Id == row.Id);
                        if (fileContent != null)
                        { 
                            fileContent.Url = await UpdatePictureLink(row);
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
                    var recordToUpdate = dataToReturn.FileContents.FirstOrDefault(f => f.Id == row.Id);
                    recordToUpdate.Url = await UpdatePictureLink(row);
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
                        Type = Constants.PostContentContainer,
                        Extension = Path.GetExtension(f.FileName)
                    }
                    ,file = f
                }).ToList();
                
                foreach (var item in files)
                {
                    item.fileContent.ContainerName = Constants.PostContentContainer;
                    item.fileContent.BlobId = await _blobStorage.UploadFileAsync(item.fileContent.ContainerName, item.file.OpenReadStream(), item.fileContent.Extension);
                }

                _unitOfWork.FileContentRepository.AddRange(files.Select(f => f.fileContent).ToList());
                postEntity.FileContents = files.Select(f => f.fileContent).ToList();
            }
            var user = await _authService.GetLoggedInUser();
            postEntity.CreatorId = user.Id;

            _unitOfWork.PostRepository.Add(postEntity);
            await _unitOfWork.SaveAsync();

            var dataToReturn = _mapper.Map<PostDTO>(postEntity);

            return CreatedAtRoute(routeName: "PostLink", routeValues: new { id = postEntity.Id }, value: dataToReturn);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var postEntity = await _unitOfWork.PostRepository.GetFirstOrDefaultAsync(x => x.Id == id, "FileContents");
            if (postEntity == null)
            {
                return NotFound();
            }
            
            if (postEntity.FileContents != null && postEntity.FileContents.Any())
            {
                foreach (var file in postEntity.FileContents)
                {
                    await _blobStorage.DeleteBlob(file.ContainerName, file.BlobId);
                }
            }

            _unitOfWork.PostRepository.Remove(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [Authorize]
        [HttpPut("like/{id}")]
        public async Task<IActionResult> UpdatePostLike(int id, [FromBody] bool like)
        {
            var postEntity = await _unitOfWork.PostRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            if (postEntity == null)
            {
                return NotFound();
            }
            var userEntity = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.FirebaseUserId == _authService.GetFirebaseUserId());
            if (userEntity == null)
            {
                return NotFound();
            }
            if (like && !postEntity.LikedByUsers.Any(u => u.Id == userEntity.Id))
            {
                postEntity.LikedByUsers.Add(userEntity);
            }
            else if (!like && postEntity.LikedByUsers.Any(u => u.Id == userEntity.Id))
            {
                postEntity.LikedByUsers.Remove(userEntity);
            }
            return NoContent();
        }

        private async Task<string> UpdatePictureLink(FileContent fileContent)
        {
            return await _blobStorage.GetSasUrlAsync(fileContent.ContainerName, fileContent.BlobId);
        }
    }
}
