using GuideMe.Models.Experiences;
using GuideMe.Services;
using GuideMe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Controllers.ReservationsManagement
{
    [ApiController]
    [Authorize]
    [Route(Util.Route)]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _reservationsService;
        public ReservationsController(ReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationRequestsForTourist(string touristId)
        {
            if (string.IsNullOrEmpty(touristId))
                return BadRequest();

            var openRequests = _reservationsService.GetOpenReservationRequestsForTourist(touristId);


            return Ok(openRequests);
        }

        [HttpGet("getPastReservationsTourist/{email}")]
        public async Task<IActionResult> GetPastReservationsForTourist(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();

            var pastExperiences = _reservationsService.GetPastReservationsForTourist(email);


            return Ok(pastExperiences);
        } 
        
        [HttpPost("insertReservation")]
        public async Task<IActionResult> InsertReservation([FromBody] ExperienceReservation experienceReservation)
        {
            if (experienceReservation == null)
                return BadRequest();

            try
            {
                await _reservationsService.InsertReservation(experienceReservation);
                return Ok();
            }
            catch(Exception e) { return BadRequest(e.Message); }
        }

        #region Reservation Requests

        [HttpPost("insertReservationRequest")]
        public async Task<IActionResult> InsertReservationRequest([FromBody] ExperienceReservationRequest experienceReservationRequest)
        {
            if (experienceReservationRequest == null)
                return BadRequest();

            try
            {
                await _reservationsService.InsertReservationRequest(experienceReservationRequest);
                return Ok();
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        #endregion
    }
}
