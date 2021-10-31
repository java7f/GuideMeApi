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
        public UserService(IMongoRepository<User> userRepository, FileManagerService fileManagerService)
        {
            _userRepository = userRepository;
            _fileManagerService = fileManagerService;
        }

        public User Get(string userId)
        {
            User user = _userRepository.FindOne(user => user.FirebaseUserId == userId);
            return user;
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

        public async Task<String> InsertProfilePhoto(string email, IFormFile profile_photo)
        {
            var file_url = await _fileManagerService.UploadProfilePhoto(profile_photo);
            var user = GetByEmail(email);
            user.ProfilePhotoUrl = file_url;
            await Update(user);
            return file_url;
        }
    }
}
