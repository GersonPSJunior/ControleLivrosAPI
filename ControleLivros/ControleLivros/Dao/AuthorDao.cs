using ControleLivros.Models;
using ControleLivros.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleLivros.Dao
{
    public class AuthorDao : IDao<Author>, IDisposable
    {
        private IConnection connection;
        public AuthorDao(IConnection ConnectionFactory)
        {
            this.connection = ConnectionFactory;
        }

        public void Insert(Author model)
        {
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO GersonAuthor([name], [lastname]) VALUES (@Name, @Lastname)";

                command.Parameters.Add("@Name", SqlDbType.Text).Value = model.Name;
                command.Parameters.Add("@Lastname", SqlDbType.Text).Value = model.LastName;

                command.ExecuteNonQuery();
            }
        }

        public void Update(Author model)
        {
            using(SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE GersonAuthor SET name=@Name, lastname=@Lastname where id=@Id";

                command.Parameters.Add("@Name", SqlDbType.Text).Value = model.Name;
                command.Parameters.Add("@Lastname", SqlDbType.Text).Value = model.LastName;
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = model.Id;
                command.ExecuteNonQuery();
            }
        }

        public bool Delete(long model)
        {
            bool result = false;
            using(SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM GersonAuthor where id=@Id";
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = model;

                // verifica se existe esse id
                if (command.ExecuteNonQuery() > 0) result = true;
            }
            return result;
        }

        public Author FindId(params object[] values)
        {
            Author author = null;
            using(SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM GersonAuthor where id=@Id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = values[0];

                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        author = new Author(reader.GetInt64(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }
            return author;
        }

        public Collection<Author> ListAll()
        {
            Collection<Author> listAll = new Collection<Author>();
            using(SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM GersonAuthor";
                using(SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach(DataRow row in table.Rows)
                    {
                        Author author = new Author(
                            int.Parse(row["id"].ToString()),
                            row["name"].ToString(),
                            row["lastName"].ToString()
                        );
                        listAll.Add(author);
                    }
                }
            }
            return listAll;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}