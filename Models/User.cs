using System;

namespace OnlineShop.Models
{
    public class User : Entity
    {
        //public string Name { get; set; }
        //private string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //public User() { }

        /*public User(string login, string password)
        {
            Login = login;
            Password = password;
        }*/

        /*public string GetLogin()
        {
            return Login;
        }*/

        /*public string GetPassword()
        {
            return Password;
        }*/
    }
}