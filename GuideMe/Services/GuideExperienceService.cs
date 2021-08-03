using AutoMapper;
using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    public class GuideExperienceService
    {
        private readonly IMongoRepository<GuideExperience> _guideExperienceRepository;
        private readonly GuideExperienceViewDataService _guideExperienceViewDataService;

        private readonly IMapper _mapper;
        public GuideExperienceService(IMongoRepository<GuideExperience> guideExperienceRepository, 
            IMapper mapper,
            GuideExperienceViewDataService guideExperienceViewDataService)
        {
            _guideExperienceRepository = guideExperienceRepository;
            _guideExperienceViewDataService = guideExperienceViewDataService;
            _mapper = mapper;
        }

        public GuideExperience Get(string experienceId)
        {
            return _guideExperienceRepository.FindById(experienceId);
        }

        public IEnumerable<GuideExperience> GetExperiences()
        {
            return _guideExperienceRepository.FindAll();
        }

        public async Task Insert(GuideExperience experience)
        {
            try
            {
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
                await _guideExperienceRepository.ReplaceOneAsync(experience);
                var experienceViewData = _mapper.Map<GuideExperienceViewData>(experience);
                await _guideExperienceViewDataService.Update(experienceViewData);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
