using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    class UsersDomain : IUsersDomain
    {
        private readonly IUserRepository _userRepository;

        public UsersDomain (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Users Authenticate(string username, string password)
        {
            return _userRepository.Authenticate(username,password);
        }
    }
}
