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
    public class AuthorController : ApiController
    {
        // GET: api/Autor
        public IEnumerable<Author> Get()
        {
            using(IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Author> author = new AuthorDao(connection);
                return author.ListAll();
            }
        }

        // GET: api/Autor/5
        public Author Get(int id)
        {
            using (IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Author> author = new AuthorDao(connection);
                return author.FindId(id);
            }
        }

        // POST: api/Autor
        public void Post([FromBody]Author model)
        {
            using (IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Author> author = new AuthorDao(connection);
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.LastName))
                    new Exception("Valor não pode ser vazio!");

                author.Insert(new Author(model.Id, model.Name, model.LastName));
            }
        }
        // PUT: api/Autor/5
        public void Put(long id, [FromBody]Author model)
        {
            using (IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Author> dao = new AuthorDao(connection);
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.LastName))
                    new Exception("Valor não pode ser vazio!");
                Author author = dao.FindId(id);
                if(author!=null) dao.Update(new Author(author.Id, model.Name, model.LastName));
            }
        }

        // DELETE: api/Autor/5
        public void Delete(long model)
        {
            using (IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Author> author = new AuthorDao(connection);
                author.Delete(model);
            }
        }
    }
}
