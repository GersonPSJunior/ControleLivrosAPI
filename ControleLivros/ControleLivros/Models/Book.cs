using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleLivros.Models
{
    public class Book
    {
        public long Id { get; private set; }
        public string Name { get; set; }
        public TypeStatus Status { get; private set; }
        public List<TypeGenre> Genre { get; private set; }

        public Book() { }
        public Book(long Id, string Name /*, List<TypeGenre> Genre*/)
        {
            this.Id = Id;
            this.Name = Name;
            Status = TypeStatus.Lista;
            //this.Genre = Genre;
        }

        public void AddGenre(TypeGenre genre)
        {
            this.Genre.Add(genre);
        }
    }
}