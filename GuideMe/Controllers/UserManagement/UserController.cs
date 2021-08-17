using GuideMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GuideMe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuideMe.Models.Account;

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
            var user = _userService.Get(userId);

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
