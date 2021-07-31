using GuideMe.Interfaces.Mongo;
using GuideMe.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    public class UserService
    {
        private readonly IMongoRepository<User> _userRepository;
        public UserService(IMongoRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Get(string userId)
        {
            return _userRepository.FindById(userId);
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
    }
}
