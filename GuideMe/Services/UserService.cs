using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Account;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    public class UserService
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly FileManagerService _fileManagerService;
        private readonly GuideExperienceViewDataService _guideExperienceViewDataService;

        public UserService(IMongoRepository<User> userRepository, 
            FileManagerService fileManagerService,
            GuideExperienceViewDataService guideExperienceViewDataService)
        {
            _userRepository = userRepository;
            _fileManagerService = fileManagerService;
            _guideExperienceViewDataService = guideExperienceViewDataService;
        }

        public User GetByFirebaseId(string firebaseId)
        {
            User user = _userRepository.FindOne(user => user.FirebaseUserId == firebaseId);
            return user;
        }

        public User GetById(string userId)
        {
            User user = _userRepository.FindById(userId);
            return user;
        }
        
        public string GetTouristInstanceId(string userId)
        {
            string instanceID = _userRepository.FindOne(user => user.FirebaseUserId == userId).TouristInstanceId;
            return instanceID;
        }
        
        public string GetGuideInstanceId(string userId)
        {
            string instanceID = _userRepository.FindOne(user => user.FirebaseUserId == userId).GuideInstanceId;
            return instanceID;
        }
        
        public async Task SaveTouristInstanceId(string instanceId, string userId)
        {
            User user = _userRepository.FindOne(user => user.FirebaseUserId == userId);
            user.TouristInstanceId = instanceId;
            await Update(user);
        }

        public async Task SaveGuideInstanceId(string instanceId, string userId)
        {
            User user = _userRepository.FindOne(user => user.FirebaseUserId == userId);
            user.GuideInstanceId = instanceId;
            await Update(user);
        }
        
        [Obsolete]
        public User GetByEmail(string userEmail)
        {
            return _userRepository.FindOne(user => user.Email == userEmail);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.FindAll();
        }

        public async Task Insert(User user)
        {
            await _userRepository.InsertOneAsync(user);
        }

        public async Task Update(User user)
        {
            await _userRepository.ReplaceOneAsync(user);
        }

        public async Task<string> InsertProfilePhoto(string email, IFormFile profile_photo)
        {
            var file_url = await _fileManagerService.UploadProfilePhoto(profile_photo);
            var user = GetByEmail(email);

            user.ProfilePhotoUrl = file_url;
            await _guideExperienceViewDataService.UpdateExperiencePhoto(file_url, user.FirebaseUserId);

            await Update(user);
            return file_url;
        }
    }
}
