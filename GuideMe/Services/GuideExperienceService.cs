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

        public List<GuideExperience> GetByUserId(string userId)
        {
            List<GuideExperience> expList = new List<GuideExperience>();
            User user = _userService.Get(userId);
            foreach(var ex in user.Wishlist)
            {
                var exp = Get(ex);
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
