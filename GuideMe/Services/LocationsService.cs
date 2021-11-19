using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Locations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    public class LocationsService
    {
        private readonly IMongoRepository<Location> _locationsRepository;
        private readonly IMongoRepository<Audioguide> _audioguidesRepository;
        private readonly UserService _userService;
        private readonly FileManagerService _fileManagerService;

        public LocationsService(IMongoRepository<Location> locationsRepository,
            IMongoRepository<Audioguide> audioguidesRepository,
            UserService userService,
            FileManagerService fileManagerService)
        {
            _locationsRepository = locationsRepository;
            _audioguidesRepository = audioguidesRepository;
            _userService = userService;
            _fileManagerService = fileManagerService;
        }

        #region Locations Management
        public Location Get(string locationId)
        {
            return _locationsRepository.FindById(locationId);
        }
        
        public IEnumerable<Location> GetLocations(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
                return _locationsRepository.FindAll();
            else
                return _locationsRepository.AsQueryable().Where(location => location.UserId == userId).ToList();
        }
        
        public async Task InsertLocation(Location location)
        {
            try
            {
                location.LocationPhotoFile = _fileManagerService.ConvertBase64ToFormFile(location.LocationPhotoFileBase64, location.Name, location.LocationPhotoFileName);
                var locationPhotoUrl = await _fileManagerService.UploadLocationPhoto(location.LocationPhotoFile);
                if (!string.IsNullOrEmpty(locationPhotoUrl))
                {
                    location.LocationPhotoUrl = locationPhotoUrl;
                    await _locationsRepository.InsertOneAsync(location);
                }
            }
            catch (Exception e) { throw; }
        }
        
        public async Task UpdateLocation(Location location)
        {
            await _locationsRepository.ReplaceOneAsync(location);
        }
        
        public async Task DeleteLocation(string locationId)
        {
            var locationAudioguides = _audioguidesRepository.AsQueryable().Where(audio => audio.LocationId == locationId);
            foreach(var audioguide in locationAudioguides)
            {
                await DeleteAudioguideByName(audioguide.Name, audioguide.Id);
            }
            await _locationsRepository.DeleteByIdAsync(locationId);
        }
        #endregion

        #region Audioguides Management
        public async Task UploadAudiofileForLocation(Audioguide audioguide)
        {
            try
            {
                audioguide.Audiofile = _fileManagerService.ConvertBase64ToFormFile(audioguide.AudiofileBase64, audioguide.Name, audioguide.AudiofileName);
                var audiofileUrl = await _fileManagerService.UploadAudiofile(audioguide.Audiofile);
                if(!string.IsNullOrEmpty(audiofileUrl))
                {
                    audioguide.AudioguideUrl = audiofileUrl;
                    await _audioguidesRepository.InsertOneAsync(audioguide);
                }
            }
            catch(Exception e) { throw; }
        }

        public IEnumerable<Audioguide> GetAudioguides(string locationId)
        {
            return _audioguidesRepository.AsQueryable().Where(audio => audio.LocationId == locationId);
        }

        public async Task<bool> DeleteAudioguide(string audioguideId)
        {
            var audioguide = _audioguidesRepository.FindById(audioguideId);
            return await DeleteAudioguideByName(audioguide.AudiofileName, audioguideId);
        }

        public async Task<bool> DeleteAudioguideByName(string audioguideName, string audioguideId)
        {
            try
            {
                if (!string.IsNullOrEmpty(audioguideName))
                {
                    var deletionResult = await _fileManagerService.DeleteAudiofile(audioguideName);
                    if (deletionResult) _audioguidesRepository.DeleteById(audioguideId);
                    return deletionResult;
                }
                else return false;
            }
            catch (Exception e) { throw; }
        }

        public async Task UpdateAudioguide(Audioguide audioguide)
        {
            await _audioguidesRepository.ReplaceOneAsync(audioguide);
        }

        public Audioguide GetAudioguide(string audioguideId)
        {
            return _audioguidesRepository.FindById(audioguideId);
        }
        
        public List<Audioguide> GetProximityAudioguides(List<string> beaconIds)
        {
            return _audioguidesRepository.FindAll().Where(audio => beaconIds.Contains(audio.MacAddress)).ToList();
        }

        #endregion
    }
}
