using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Account;
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
            return _userRepository.FindById(userId);
        }
        
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
            try
            {
                if (user.ProfilePhoto != null)
                    user.ProfilePhotoUrl = await _fileManagerService.UploadProfilePhoto(user.ProfilePhoto);
                //if (string.IsNullOrEmpty(user.Id))
                //    user.Id = ObjectId.GenerateNewId().ToString();
            }
            catch (Exception e) { throw (e); }
            await _userRepository.InsertOneAsync(user);
        }

        public async Task Update(User user)
        {
            try
            {
                if (user.ProfilePhoto != null)
                    user.ProfilePhotoUrl = await _fileManagerService.UploadProfilePhoto(user.ProfilePhoto);
            } catch (Exception e) { throw (e); }

            await _userRepository.ReplaceOneAsync(user);
        }
    }
}
