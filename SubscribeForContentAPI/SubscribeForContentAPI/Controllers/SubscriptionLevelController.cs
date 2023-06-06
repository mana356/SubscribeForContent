using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC_DataAccess.Repository.Contracts;
using SFC_DTO.CreatorSubscriptionLevel;

namespace SubscribeForContentAPI.Controllers
{
    [ApiController]
    [Route("api/v1/SubscriptionLevels")]
    public class SubscriptionLevelController : Controller
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public SubscriptionLevelController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetSubscriptionLevelsForCreator(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is invalid");
            }

            var creator = await _unitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(u => u.UserName == username);

            if (creator == null)
            {
                return NotFound();
            }

            var levels = await _unitOfWork.SubscriptionLevelRepository.GetSubscriptionLevelsOfCreator(username);

            var dataToReturn = _mapper.Map<List<CreatorSubscriptionLevelDTO>>(levels);

            return Ok(dataToReturn);
        }
    }
}
