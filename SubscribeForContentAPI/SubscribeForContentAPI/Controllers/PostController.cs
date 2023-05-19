using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SFC_DTO.FileContent;
using SFC_DTO.Post;
using SubscribeForContentAPI.Services.Contracts;

namespace SubscribeForContentAPI.Controllers
{
    [ApiController]
    [Route("api/v1/Posts")]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorage _blobStorage;

        public PostController(IMapper mapper, IUnitOfWork unitOfWork, IBlobStorage blobStorage)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _blobStorage = blobStorage;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] PostSearchFilter queryFilter = null)
        {
            var posts = await _unitOfWork.PostRepository.GetPostsAsync(queryFilter);
            var postResults = _mapper.Map<List<PostSummaryDTO>>(posts);

            return Ok(postResults);
        }

        [HttpGet("{id}", Name = "GetPostById")]
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
            postEntity.CreatorId = 3;
            _unitOfWork.PostRepository.Add(postEntity);
            await _unitOfWork.SaveAsync();

            var dataToReturn = _mapper.Map<PostDTO>(postEntity);

            return CreatedAtRoute("GetPostById", dataToReturn);
        }
    }
}
