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

        public TouristAlertService(IMongoRepository<TouristAlert> touristAlertRepository)
        {
            _touristAlertRepository = touristAlertRepository;
        }

        public async Task Insert(TouristAlert touristAlert)
        {
            await _touristAlertRepository.InsertOneAsync(touristAlert);
        }

        public IEnumerable<TouristAlert> GetTouristAlerts()
        {
            return _touristAlertRepository.FindAll();
        }
    }
}
