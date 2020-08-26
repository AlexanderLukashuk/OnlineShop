using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public interface IAuthorizationService
    {
        public bool Authentication(User user, List<User> users);
        public bool Authorization(User user, List<User> users);
    }
}