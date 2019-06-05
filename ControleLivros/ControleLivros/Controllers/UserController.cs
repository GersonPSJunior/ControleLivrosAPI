using ControleLivros.Dao;
using ControleLivros.Models;
using ControleLivros.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControleLivros.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/Usuario
        public IEnumerable<User> Get()
        {
            using(IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<User> dao = new UserDao(connection);
                return dao.ListAll();
            }
        }

        // GET: api/Usuario/5
        public User Get(int id)
        {
            using(IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<User> dao = new UserDao(connection);
                return dao.FindId(id);
            }
        }

        // POST: api/Usuario
        public void Post([FromBody]User model)
        {
            using(IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<User> dao = new UserDao(connection);
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Login) ||
                    string.IsNullOrEmpty(model.Password)) new Exception("Não pode ser vazio!");
                dao.Insert(new User(model.Id, model.Name, model.Login, model.Password));
            }
        }

        // PUT: api/Usuario/5
        public void Put(long id, [FromBody]User model)
        {
            using (IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<User> dao = new UserDao(connection);
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Login) ||
                    string.IsNullOrEmpty(model.Password)) new Exception("Não pode ser vazio!");
                User user = dao.FindId(id);
                if (user != null) dao.Update(new User(user.Id, model.Name, model.Login, model.Password));
            }
        }

        // DELETE: api/Usuario/5
        public void Delete(long model)
        {
            using(IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<User> dao = new UserDao(connection);
                dao.Delete(model);
                //if (!dao.Delete(model)) new Exception("Usuario não encontrado!");
            }
        }
    }
}
