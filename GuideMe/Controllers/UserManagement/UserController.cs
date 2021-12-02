using GuideMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GuideMe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuideMe.Models.Account;
using Microsoft.AspNetCore.Http;

namespace GuideMe.Controllers.UserManagement
{
    [ApiController]
    [Route(Util.Route)]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            var user = _userService.GetByFirebaseId(userId);

            if (user != null) return Ok(user);
            else return NotFound();
        }

        [HttpGet("getById/{userId}")]
        public IActionResult GetById(string userId)
        {
            var user = _userService.GetById(userId);
            if (user != null) return Ok(user);
            else return NotFound();
        }
        
        [HttpGet("getByEmail/{email}")]
        public IActionResult GetByEmail(string email)
        {
            var user = _userService.GetByEmail(email);

            if (user != null) return Ok(user);
            else return NotFound();
        }
        
        [HttpGet("getTouristInstanceId/{userId}")]
        public IActionResult GetTouristInstanceId(string userId)
        {
            var instanceId = _userService.GetTouristInstanceId(userId);

            if (instanceId != null) return Ok(instanceId);
            else return NotFound();
        }
        
        [HttpGet("getGuideInstanceId/{userId}")]
        public IActionResult GetGuideInstanceId(string userId)
        {
            var instanceId = _userService.GetTouristInstanceId(userId);

            if (instanceId != null) return Ok(instanceId);
            else return NotFound();
        }
        
        [HttpGet("saveGuideInstanceId/{instanceId}/{userId}")]
        public async Task<IActionResult> SaveGuideInstanceId(string instanceId, string userId)
        {
            try
            {
                 await _userService.SaveGuideInstanceId(instanceId, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("saveTouristInstanceId/{instanceId}/{userId}")]
        public async Task<IActionResult> SaveTouristInstanceId(string instanceId, string userId)
        {
            try
            {
                await _userService.SaveTouristInstanceId(instanceId, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetUsers().ToList();

            if (users != null) return Ok(users);
            else return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] User user)
        {
            try
            {
                await _userService.Insert(user);
                return Ok();
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("uploadFile/{email}")]
        public async Task<IActionResult> InsertProfilePicture(string email, [FromForm] IFormFile profile_photo)
        {
            try
            {
                var url = await _userService.InsertProfilePhoto(email, profile_photo);
                return Ok(url);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                await _userService.Update(user);
                return Ok();
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
