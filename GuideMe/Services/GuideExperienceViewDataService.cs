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
        public GuideExperienceViewDataService(IMongoRepository<GuideExperienceViewData> guideExperienceViewDataRepository)
        {
            _guideExperienceViewDataRepository = guideExperienceViewDataRepository;
        }

        public GuideExperienceViewData Get(string experienceId)
        {
            return _guideExperienceViewDataRepository.FindById(experienceId);
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
    }
}
