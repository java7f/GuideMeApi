using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Account;
using GuideMe.Models.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    public class ReservationsService
    {
        private readonly UserService _userService;
        private readonly IMongoRepository<ExperienceReservation> _experienceReservationRepository;
        private readonly IMongoRepository<ExperienceReservationRequest> _experienceReservationRequestRepository;

        public ReservationsService(IMongoRepository<ExperienceReservation> experienceReservationRepository,
            IMongoRepository<ExperienceReservationRequest> experienceReservationRequestRepository,
            UserService userService)
        {
            _userService = userService;
            _experienceReservationRepository = experienceReservationRepository;
            _experienceReservationRequestRepository = experienceReservationRequestRepository;
        }

        #region Experience Reservations
        public ExperienceReservation GetReservation(string id)
        {
            return _experienceReservationRepository.FindById(id);
        }

        public List<ExperienceReservation> GetPastReservationsForTourist(string email)
        {
            var user = _userService.GetByEmail(email);
            return _experienceReservationRepository.AsQueryable()
                .Where(reservation => reservation.TouristUserId == user.Id && reservation.ToDate < DateTime.Now).ToList();
        }

        public List<ExperienceReservation> GetPastReservationsForGuide(string guideId)
        {
            return _experienceReservationRepository.AsQueryable()
                .Where(reservation => reservation.GuideUserId == guideId && reservation.ToDate < DateTime.Now).ToList();
        }

        public List<ExperienceReservation> GetGuideReservations(string guideId)
        {
            return _experienceReservationRepository.AsQueryable()
                .Where(reservation => reservation.GuideUserId == guideId && reservation.ToDate > DateTime.Now)
                .OrderBy(r => r.FromDate)
                .ToList();
        }
        
        public List<ExperienceReservation> GetTouristReservations(string touristId)
        {
            return _experienceReservationRepository.AsQueryable()
                .Where(reservation => reservation.TouristUserId == touristId && reservation.ToDate > DateTime.Now)
                .OrderBy(r => r.FromDate)
                .ToList();
        }

        public async Task InsertReservation(ExperienceReservation experienceReservation)
        {
            await _experienceReservationRepository.InsertOneAsync(experienceReservation);
        }

        public async Task UpdateReservation(ExperienceReservation experienceReservation)
        {
            await _experienceReservationRepository.ReplaceOneAsync(experienceReservation);
        }

        public async Task<bool> DeleteReservation(string id)
        {
            bool result = true;
            try
            {
                await _experienceReservationRepository.DeleteByIdAsync(id);
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Experience Reservation Requests
        public async Task<ExperienceReservationRequest> GetReservationRequest(string id)
        {
            return await _experienceReservationRequestRepository.FindByIdAsync(id);
        }

        public List<ExperienceReservationRequest> GetOpenReservationRequestsForTourist(string touristId)
        {
            return _experienceReservationRequestRepository.AsQueryable()
                .Where(reservation => reservation.TouristUserId == touristId).ToList();
        }

        public List<ExperienceReservationRequest> GetOpenReservationRequestsForGuide(string guideId)
        {
            return _experienceReservationRequestRepository.AsQueryable()
                .Where(reservation => reservation.GuideUserId == guideId && reservation.ReservationStatus == ReservationStatus.PENDING).ToList();
        }

        public async Task AcceptReservationRequest(string reservationRequestId)
        {
            var offer = _experienceReservationRequestRepository.FindById(reservationRequestId);
            offer.ReservationStatus = ReservationStatus.ACCEPTED;
            _experienceReservationRequestRepository.ReplaceOne(offer);
            var newReservation = new ExperienceReservation
            {
                TouristUserId = offer.TouristUserId,
                GuideUserId = offer.GuideUserId,
                GuideExperienceId = offer.GuideExperienceId,
                FromDate = offer.FromDate,
                ToDate = offer.ToDate,
                Price = offer.Price,
                Address = offer.Address,
                TouristFirstName = offer.TouristFirstName,
                TouristLastName = offer.TouristLastName,
                GuideFirstName = offer.GuideFirstName,
                GuideLastName = offer.GuideLastName,
            };

            await InsertReservation(newReservation);
        }

        public async Task RejectReservationRequest(string reservationRequestId)
        {
            var offer = _experienceReservationRequestRepository.FindById(reservationRequestId);
            offer.ReservationStatus = ReservationStatus.REJECTED;
            _experienceReservationRequestRepository.ReplaceOne(offer);
        }

        public async Task InsertReservationRequest(ExperienceReservationRequest experienceReservationRequest)
        {
            await _experienceReservationRequestRepository.InsertOneAsync(experienceReservationRequest);
        }

        public async Task UpdateReservationRequest(ExperienceReservationRequest experienceReservationRequest)
        {
            await _experienceReservationRequestRepository.ReplaceOneAsync(experienceReservationRequest);
        }
        
        public async Task DeleteReservationRequest(string experienceReservationRequestId)
        {
            await _experienceReservationRequestRepository.DeleteByIdAsync(experienceReservationRequestId);
        }
        #endregion
    }
}
