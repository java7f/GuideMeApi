﻿using GuideMe.Models.Experiences;
using GuideMe.Services;
using GuideMe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Controllers.GuideExperiences
{
    [ApiController]
    [Authorize]
    [Route(Util.Route)]
    public class GuideExperienceController : ControllerBase
    {
        private readonly GuideExperienceService _guideExperienceService;

        public GuideExperienceController(GuideExperienceService guideExperienceService)
        {
            _guideExperienceService = guideExperienceService;
        }

        [HttpGet("{experienceId}")]
        public IActionResult Get(string experienceId)
        {
            var experience = _guideExperienceService.Get(experienceId);

            if (experience != null) return Ok(experience);
            else return NotFound();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var experiences = _guideExperienceService.GetExperiences().ToList();

            if (experiences != null) return Ok(experiences);
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] GuideExperience experience)
        {
            try
            {
                await _guideExperienceService.Insert(experience);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GuideExperience experience)
        {
            try
            {
                await _guideExperienceService.Update(experience);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}