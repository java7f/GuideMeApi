using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    public class TouristAlertService
    {
        private readonly IMongoRepository<TouristAlert> _touristAlertRepository;
        private readonly IMongoRepository<GuidingOffer> _guidingOfferRepository;
        private readonly ReservationsService _reservationsService;
        private readonly GuideExperienceService _guideExperienceService;
        private readonly UserService _userService;

        public TouristAlertService(IMongoRepository<TouristAlert> touristAlertRepository, 
            IMongoRepository<GuidingOffer> guidingOfferRepository,
            ReservationsService reservationsService,
            GuideExperienceService guideExperienceService, 
            UserService userService)
        {
            _touristAlertRepository = touristAlertRepository;
            _guidingOfferRepository = guidingOfferRepository;
            _reservationsService = reservationsService;
            _guideExperienceService = guideExperienceService;
            _userService = userService;
        }

        public async Task Insert(TouristAlert touristAlert)
        {
            await _touristAlertRepository.InsertOneAsync(touristAlert);
        }
        
        public async Task InsertGuideOffer(GuidingOffer guidingOffer)
        {
            var guideUser = _userService.GetByFirebaseId(guidingOffer.GuideId);
            var guideExperience = _guideExperienceService.GetExperienceByGuideId(guidingOffer.GuideId);

            guidingOffer.GuideExperienceId = guideExperience.Id;
            guidingOffer.GuideFirstName = guideUser.FirstName;
            guidingOffer.GuideLastName = guideUser.LastName;
            guidingOffer.GuidePhotoUrl = guideUser.ProfilePhotoUrl;

            await _guidingOfferRepository.InsertOneAsync(guidingOffer);
        }
        
        public async Task<List<GuidingOffer>> GetGuideOffersForTourist(string touristId)
        {
           return _guidingOfferRepository.FindAll().Where(offer => offer.TouristId == touristId && offer.ReservationStatus == ReservationStatus.PENDING).ToList();
        }
        
        public async Task<List<GuidingOffer>> GetGuideOffersForGuide(string guideId)
        {
           return _guidingOfferRepository.FindAll().Where(offer => offer.GuideId == guideId).ToList();
        }

        public async Task AcceptGuideOffer(string guideOfferId)
        {
            var offer = _guidingOfferRepository.FindById(guideOfferId);
            var guideExperience = _guideExperienceService.Get(offer.GuideExperienceId);
            offer.ReservationStatus = ReservationStatus.ACCEPTED;
            _guidingOfferRepository.ReplaceOne(offer);
            var newReservation = new ExperienceReservation
            {
                TouristUserId = offer.TouristId,
                GuideUserId = offer.GuideId,
                GuideExperienceId = offer.GuideExperienceId,
                FromDate = offer.FromDate,
                ToDate = offer.ToDate,
                ExperienceTags = guideExperience.ExperienceTags,
                Price = guideExperience.ExperiencePrice,
                Address = guideExperience.GuideAddress,
                TouristFirstName = offer.TouristFirstName,
                TouristLastName = offer.TouristLastName,
                GuideFirstName = offer.GuideFirstName,
                GuideLastName = offer.GuideLastName,
            };

            await _reservationsService.InsertReservation(newReservation);
        }
        
        public async Task RejectGuideOffer(string guideOfferId)
        {
            var offer = _guidingOfferRepository.FindById(guideOfferId);
            offer.ReservationStatus = ReservationStatus.REJECTED;
            _guidingOfferRepository.ReplaceOne(offer);
        }
        
        public async Task<List<TouristAlert>> GetOpenAlertsOfTourist(string touristId)
        {
           return _touristAlertRepository.FindAll().Where(alert => alert.TouristId == touristId).ToList();
        }

        public IEnumerable<TouristAlert> GetTouristAlerts(string location, string currentUserId)
        {
            return _touristAlertRepository.FindAll()
                .Where(alert => alert.TouristId != currentUserId && alert.TouristDestination.ToLower().Contains(location.ToLower()));
        }

        public async Task DeleteTouristAlert(string alertId)
        {
            await _touristAlertRepository.DeleteByIdAsync(alertId);
        }
    }
}
