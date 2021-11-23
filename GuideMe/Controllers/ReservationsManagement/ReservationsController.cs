﻿using GuideMe.Models.Experiences;
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
    //[Authorize(AuthenticationSchemes = "Mobile")]
    [Route(Util.Route)]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _reservationsService;
        private readonly GuideExperienceService _experienceService;
        public ReservationsController(ReservationsService reservationsService, GuideExperienceService experienceService)
        {
            _reservationsService = reservationsService;
            _experienceService = experienceService;
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

        [HttpGet("getGuideReservations/{guideId}")]
        public async Task<IActionResult> GetGuideReservations(string guideId)
        {
            try
            {
                var guideReservations = _reservationsService.GetGuideReservations(guideId);
                return Ok(guideReservations);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
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

        [HttpPut("rateReservation")]
        public async Task<IActionResult> RateReservation([FromBody] ExperienceReservation experienceReservation)
        {
            if (experienceReservation == null)
                return BadRequest();

            try
            {
                experienceReservation.ExperienceRating.UserId = experienceReservation.TouristUserId;
                experienceReservation.ExperienceRating.UserName = $"{experienceReservation.TouristFirstName} {experienceReservation.TouristLastName}";
                await _reservationsService.UpdateReservation(experienceReservation);
                await _experienceService.AddReviewToExperience(experienceReservation.GuideExperienceId, experienceReservation.ExperienceRating);
                return Ok();
            }
            catch (Exception e) { return BadRequest(e.Message); }
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
