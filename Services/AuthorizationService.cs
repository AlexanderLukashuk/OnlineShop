using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool Authentication(User user, List<User> users)
        {
            foreach (var el in users)
            {
                if ((el.Email == user.Email || el.Phone == user.Phone) && el.Password == user.Password)
                {
                    return false;
                }
            }

            Registration(user, users);
            //users.Add(user);
            return true;
        }

        private void Registration(User user, List<User> users)
        {
            users.Add(user);
        }

        public bool Authorization(User user, List<User> users)
        {
            if (!validUser(user))
            {
                return false;
            }

            foreach (var el in users)
            {
                if ((el.Email == user.Email || el.Phone == user.Phone) && el.Password == user.Password)
                {
                    return true;
                }
            }

            return false;
        }

        private bool validUser(User user)
        {
            return !((user.Email.Length == 0 || user.Phone.Length == 0) || user.Password.Length == 0);
        }
    }
}