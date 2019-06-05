using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleLivros.Models
{
    public class Author
    {
        public long Id { get; private set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        //public List<Book> Books { get; private set; }

        public Author() { }
        public Author(long id, string name, string lastname)
        {
            this.Id = id;
            this.Name = name;
            this.LastName = lastname;
        }
    }
}