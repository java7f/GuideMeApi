using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    public class GuideExperienceViewDataService
    {
        private readonly IMongoRepository<GuideExperienceViewData> _guideExperienceViewDataRepository;
        private readonly IMongoRepository<GuideExperience> _guideExperienceRepository;

        public GuideExperienceViewDataService(IMongoRepository<GuideExperienceViewData> guideExperienceViewDataRepository,
            IMongoRepository<GuideExperience> guideExperienceRepository)
        {
            _guideExperienceViewDataRepository = guideExperienceViewDataRepository;
            _guideExperienceRepository = guideExperienceRepository;
        }

        public GuideExperienceViewData Get(string experienceId)
        {
            return _guideExperienceViewDataRepository.FindById(experienceId);
        }

        public GuideExperienceViewData GetByGuideFirebaseId(string firebaseId)
        {
            return _guideExperienceViewDataRepository.FindOne(ex => ex.GuideFirebaseId == firebaseId);
        }

        public IEnumerable<GuideExperienceViewData> GetExperiences(string location, string currentTouristId)
        {
            return _guideExperienceViewDataRepository.FindAll()
                .Where(experience => (experience.GuideAddress != null) && experience.GuideFirebaseId != currentTouristId
                && (experience.GuideAddress.City.ToLower().Contains(location) || 
                experience.GuideAddress.Country.ToLower().Contains(location)));
        }
        
        public IEnumerable<GuideExperienceViewData> GetExperiences()
        {
            return _guideExperienceViewDataRepository.FindAll();
        }

        public async Task Insert(GuideExperienceViewData experience)
        {
            await _guideExperienceViewDataRepository.InsertOneAsync(experience);
        }

        public async Task Update(GuideExperienceViewData experience)
        {
            await _guideExperienceViewDataRepository.ReplaceOneAsync(experience);
        }

        public async Task UpdateRating(string experienceId, float newRating)
        {
            var experience = Get(experienceId);
            experience.GuideRating = newRating;
            await Update(experience);
        }

        public async Task UpdateExperiencePhoto(string fileUrl, string firebaseUserId)
        {
            var guideExpViewData = GetByGuideFirebaseId(firebaseUserId);
            var guideExp = _guideExperienceRepository.FindOne(exp => exp.GuideFirebaseId == firebaseUserId);

            if (guideExpViewData != null)
            {
                guideExpViewData.GuidePhotoUrl = fileUrl;
                await Update(guideExpViewData);
            }

            if (guideExp != null)
            {
                guideExp.GuidePhotoUrl = fileUrl;
                await _guideExperienceRepository.ReplaceOneAsync(guideExp);
            }
        }
    }
}
