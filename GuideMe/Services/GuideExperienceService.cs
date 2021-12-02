using AutoMapper;
using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Account;
using GuideMe.Models.Experiences;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    public class GuideExperienceService
    {
        private readonly IMongoRepository<GuideExperience> _guideExperienceRepository;
        private readonly GuideExperienceViewDataService _guideExperienceViewDataService;
        private readonly UserService _userService;

        private readonly IMapper _mapper;
        public GuideExperienceService(IMongoRepository<GuideExperience> guideExperienceRepository, 
            IMapper mapper,
            GuideExperienceViewDataService guideExperienceViewDataService,
            UserService userService)
        {
            _guideExperienceRepository = guideExperienceRepository;
            _guideExperienceViewDataService = guideExperienceViewDataService;
            _mapper = mapper;
            _userService = userService;
        }

        public GuideExperience Get(string experienceId)
        {
            return _guideExperienceRepository.FindById(experienceId);
        }
        public GuideExperience GetExperienceByGuideId(string guideId)
        {
            return _guideExperienceRepository.FindOne(exp => exp.GuideFirebaseId == guideId);
        }

        public List<GuideExperience> GetByUserFirebaseId(string userId)
        {
            List<GuideExperience> expList = new List<GuideExperience>();
            User user = _userService.GetByFirebaseId(userId);
            foreach(var ex in user.Wishlist)
            {
                var exp = Get(ex);
                if(exp != null)
                    expList.Add(exp);
            }
            return expList;
        }

        public IEnumerable<GuideExperience> GetExperiences()
        {
            return _guideExperienceRepository.FindAll();
        }

        public async Task Insert(GuideExperience experience)
        {
            try
            {
                var guideUser = _userService.GetByFirebaseId(experience.GuideFirebaseId);
                experience.GuideFirstName = guideUser.FirstName;
                experience.GuideLastName = guideUser.LastName;
                experience.GuidePhotoUrl = guideUser.ProfilePhotoUrl;
                experience.GuideAddress = guideUser.Address;
                await _guideExperienceRepository.InsertOneAsync(experience);
                var experienceViewData = _mapper.Map<GuideExperienceViewData>(experience);
                await _guideExperienceViewDataService.Insert(experienceViewData);

            } catch(Exception e) {
                throw new Exception(e.Message);
            }
        }
        
        public async Task Update(GuideExperience experience)
        {
            try
            {
                var guideUser = _userService.GetByFirebaseId(experience.GuideFirebaseId);
                experience.GuideAddress = guideUser.Address;
                await _guideExperienceRepository.ReplaceOneAsync(experience);
                var experienceViewData = _mapper.Map<GuideExperienceViewData>(experience);
                await _guideExperienceViewDataService.Update(experienceViewData);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task AddReviewToExperience(string guideExperienceId, string touristUserId, Review review)
        {
            try
            {
                var experience = _guideExperienceRepository.FindById(guideExperienceId);
                var user = _userService.GetByFirebaseId(touristUserId);

                review.ProfilePhotoUrl = user.ProfilePhotoUrl;
                experience.GuideReviews.Add(review);

                await _guideExperienceRepository.ReplaceOneAsync(experience);
                float totalRating = 0.0f;
                foreach(var rev in experience.GuideReviews)
                {
                    totalRating += rev.RatingValue;
                }
                totalRating = totalRating / experience.GuideReviews.Count;
                await _guideExperienceViewDataService.UpdateRating(guideExperienceId, totalRating);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Adds new review to user's reviews list. 
        /// Rating calculation is done in Android Studio with the <c>calculateUserRating</c> method from the <c>ProfileViewModel</c>.
        /// </summary>
        /// <param name="userFirebaseId"></param>
        /// <param name="review"></param>
        public async Task AddReviewToTourist(string userFirebaseId, Review review)
        {
            try
            {
                var user = _userService.GetByFirebaseId(userFirebaseId);
                review.ProfilePhotoUrl = user.ProfilePhotoUrl;
                user.Reviews.Add(review);
                await _userService.Update(user);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
