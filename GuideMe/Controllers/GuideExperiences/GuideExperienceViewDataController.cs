using GuideMe.Models.Experiences;
using GuideMe.Services;
using GuideMe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Controllers.GuideExperiences
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Mobile")]
    [Route(Util.Route)]
    public class GuideExperienceViewDataController : ControllerBase
    {
        private readonly GuideExperienceViewDataService _guideExperienceViewDataService;

        public GuideExperienceViewDataController(GuideExperienceViewDataService guideExperienceViewDataService)
        {
            _guideExperienceViewDataService = guideExperienceViewDataService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var experiences = _guideExperienceViewDataService.GetExperiences().ToList();

            if (experiences != null) return Ok(experiences);
            else return NotFound();
        }
        
        [HttpGet("getAll/{location}/{currentTouristId}")]
        public IActionResult GetAll(string location, string currentTouristId)
        {
            location = location.ToLower();
            var experiences = _guideExperienceViewDataService.GetExperiences(location, currentTouristId).ToList();

            if (experiences != null) return Ok(experiences);
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] GuideExperienceViewData experience)
        {
            try
            {
                await _guideExperienceViewDataService.Insert(experience);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GuideExperienceViewData experience)
        {
            try
            {
                await _guideExperienceViewDataService.Update(experience);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
