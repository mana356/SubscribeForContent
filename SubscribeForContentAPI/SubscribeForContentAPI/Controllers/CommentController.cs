using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SFC_DTO.Comment;
using SFC_DTO.Post;
using SFC_Utility;
using SubscribeForContentAPI.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace SubscribeForContentAPI.Controllers
{
    [ApiController]
    [Route("api/v1/Comments")]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorage _blobStorage;
        private readonly IAuthService _authService;

        public CommentController(IMapper mapper, IUnitOfWork unitOfWork, IBlobStorage blobStorage, IAuthService authService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _blobStorage = blobStorage;
            _authService = authService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] CommentSearchFilter queryFilter = null)
        {
            var comments = await _unitOfWork.CommentRepository.GetCommentsAsync(queryFilter);
            var commentResults = _mapper.Map<List<CommentDTO>>(comments);

            if (queryFilter.PostId.HasValue)
            {
                commentResults = commentResults.Where(x => x.ParentCommentId == null).ToList();
            }
            
            return Ok(commentResults);
        }

        [Authorize]
        [HttpGet("{id}", Name = "CommentLink")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var commentEntity = await _unitOfWork.CommentRepository.GetFirstOrDefaultAsync(c => c.Id == id, "LikedByUsers,ChildComments,User");

            if (commentEntity == null)
            {
                return NotFound();
            }
            

            var dataToReturn = _mapper.Map<CommentDTO>(commentEntity);

            return Ok(dataToReturn);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentCreationDTO commentCreationDTO)
        {
            var commentEntity = _mapper.Map<Comment>(commentCreationDTO);

            var user = await _authService.GetLoggedInUser();
            commentEntity.UserId = user.Id;

            _unitOfWork.CommentRepository.Add(commentEntity);
            await _unitOfWork.SaveAsync();

            var dataToReturn = _mapper.Map<CommentDTO>(commentEntity);

            return CreatedAtRoute(routeName: "CommentLink", routeValues: new { id = commentEntity.Id }, value: dataToReturn);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody][Required] string body)
        {
            var commentEntity = await _unitOfWork.CommentRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            if (commentEntity == null)
            {
                return NotFound();
            }
            var userEntity = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.FirebaseUserId == _authService.GetFirebaseUserId());
            if (userEntity == null)
            {
                return NotFound();
            }
            if (commentEntity.User.Id != userEntity.Id)
            {
                return Unauthorized();
            }
            if (string.IsNullOrEmpty(body))
            {
                return BadRequest("Comment body cannot be empty");
            }

            commentEntity.Body = body;
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var commentEntity = await _unitOfWork.CommentRepository.GetFirstOrDefaultAsync(x => x.Id == id, "ChildComments");
            if (commentEntity == null)
            {
                return NotFound();
            }

            //maintain comment chain in case of deletion
            if (commentEntity.ChildComments != null && commentEntity.ChildComments.Any())
            {
                commentEntity.UserId = Constants.DeletedUserId;
                commentEntity.Body = "Deleted";
            }
            else {
                _unitOfWork.CommentRepository.Remove(id);
            }
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [Authorize]
        [HttpPut("like/{id}")]
        public async Task<IActionResult> UpdateCommentLike(int id, [FromBody] bool like)
        {
            var commentEntity = await _unitOfWork.CommentRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            if (commentEntity == null)
            {
                return NotFound();
            }
            var userEntity = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.FirebaseUserId == _authService.GetFirebaseUserId());
            if (userEntity == null)
            {
                return NotFound();
            }
            if (like && !commentEntity.LikedByUsers.Any(u => u.Id == userEntity.Id))
            {
                commentEntity.LikedByUsers.Add(userEntity);
            }
            else if (!like && commentEntity.LikedByUsers.Any(u => u.Id == userEntity.Id)) 
            { 
                commentEntity.LikedByUsers.Remove(userEntity);
            }
            return NoContent();
        }
    }
}
