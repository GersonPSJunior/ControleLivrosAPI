using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleLivros.Models
{
    public class User
    {
        public long Id { get; private set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Book> Books { get; private set; }

        public User() { }

        public User(long id, string name, string login, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Login = login;
            this.Password = password;
        }
    }
}