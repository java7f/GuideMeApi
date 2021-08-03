using AutoMapper;
using GuideMe.Models.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Utils
{
    public class GuideMeProfile : Profile
    {
        public GuideMeProfile()
        {
            CreateMap<GuideExperience, GuideExperienceViewData>();
        }
    }
}
