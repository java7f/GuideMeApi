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

        [HttpGet("getAll/{location}/{currentUserId}")]
        public async Task<IActionResult> GetAll(string location, string currentUserId)
        {
            var touristAlerts = _touristAlertService.GetTouristAlerts(location, currentUserId).ToList();
            if (touristAlerts != null) return Ok(touristAlerts);
            else return NotFound();
        }
        
        [HttpGet("guideOffers/{touristId}")]
        public async Task<IActionResult> GetOpenGuideOffersForTourist(string touristId)
        {
            var touristAlerts = await _touristAlertService.GetGuideOffersForTourist(touristId);
            if (touristAlerts != null) return Ok(touristAlerts);
            else return NotFound();
        }
        
        [HttpGet("guideOffers/guide/{guideId}")]
        public async Task<IActionResult> GetGuideOffersForGuide(string guideId)
        {
            var touristAlerts = await _touristAlertService.GetGuideOffersForGuide(guideId);
            if (touristAlerts != null) return Ok(touristAlerts);
            else return NotFound();
        }
        
        [HttpGet("guideOffers/accept/{offerId}")]
        public async Task<IActionResult> AcceptGuideOffers(string offerId)
        {
            try
            {
                await _touristAlertService.AcceptGuideOffer(offerId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("guideOffers/reject/{offerId}")]
        public async Task<IActionResult> RejectGuideOffers(string offerId)
        {
            try
            {
                await _touristAlertService.RejectGuideOffer(offerId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("alerts/{touristId}")]
        public async Task<IActionResult> GetOpenAlertsOfTourist(string touristId)
        {
            var touristAlerts = await _touristAlertService.GetOpenAlertsOfTourist(touristId);
            if (touristAlerts != null) return Ok(touristAlerts);
            else return NotFound();
        }

        [HttpPost("guideOffers")]
        public async Task<IActionResult> InsertGuideOffer([FromBody] GuidingOffer guidingOffer)
        {
            try
            {
                await _touristAlertService.InsertGuideOffer(guidingOffer);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
