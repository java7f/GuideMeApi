using GuideMe.Models.Experiences;
using GuideMe.Services;
using GuideMe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GuideMe.Controllers.GuideExperiences
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Mobile")]
    [Route(Util.Route)]
    public class TouristAlertController : ControllerBase
    {
        private readonly TouristAlertService _touristAlertService;

        public TouristAlertController(TouristAlertService touristAlertService)
        {
            _touristAlertService = touristAlertService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] TouristAlert touristAlert)
        {
            try
            {
                await _touristAlertService.Insert(touristAlert);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GettAll()
        {
            var touristAlerts = _touristAlertService.GetTouristAlerts().ToList();
            if (touristAlerts != null) return Ok(touristAlerts);
            else return NotFound();
        }
    }
}
