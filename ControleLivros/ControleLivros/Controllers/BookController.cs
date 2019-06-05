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
    public class BookController : ApiController
    {
        // GET: api/Livro
        public IEnumerable<Book> Get()
        {
            using (IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Book> dao = new BookDao(connection);
                return dao.ListAll();
            }
        }

        // GET: api/Livro/5
        public Book Get(int id)
        {
            using(IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Book> dao = new BookDao(connection);
                return dao.FindId(id);
            }
        }

        // POST: api/Livro
        public void Post([FromBody]Book model)
        {
            using(IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Book> dao = new BookDao(connection);
                if (string.IsNullOrEmpty(model.Name)) new Exception("Não pode ser vazio!");
                dao.Insert(new Book(model.Id, model.Name));
            }
        }

        // PUT: api/Livro/5
        public void Put(long id, [FromBody]Book model)
        {
            using (IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Book> dao = new BookDao(connection);
                if (string.IsNullOrEmpty(model.Name))
                    new Exception("Valor não pode ser vazio!");
                Book book = dao.FindId(id);
                if (book != null) dao.Update(new Book(book.Id, model.Name));
            }
        }

        // DELETE: api/Livro/5
        public void Delete(long model)
        {
            using(IConnection connection = new ConnectionFactory())
            {
                connection.Open();
                IDao<Book> dao = new BookDao(connection);
                dao.Delete(model);
                //if (!dao.Delete(model)) new Exception("Livro não encontrado!");
            }
        }
    }
}
