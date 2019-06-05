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
    public class BookDao : IDao<Book>, IDisposable
    {
        private IConnection connection;
        public BookDao(IConnection connection)
        {
            this.connection = connection;
        }

        public void Insert(Book model)
        {
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO GersonBook([name], [status]) VALUES (@Name,@Status)";

                command.Parameters.Add("@Name", SqlDbType.Text).Value = model.Name;
                command.Parameters.Add("@Status", SqlDbType.Text).Value = model.Status;
                command.ExecuteNonQuery();
            }
        }

        public void Update(Book model)
        {
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE GersonBook SET Name=@Name, Status=@Status where Id=@Id";

                command.Parameters.Add("@Name", SqlDbType.Text).Value = model.Name;
                command.Parameters.Add("@Status", SqlDbType.Text).Value = model.Status;
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = model.Id;
                command.ExecuteNonQuery();
            }
        }

        public bool Delete(long model)
        {
            bool result = false;
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM GersonBook where Id=@Id";
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = model;

                // verifica se existe esse id
                if (command.ExecuteNonQuery() > 0) result = true;
            }
            return result;
        }

        public Book FindId(params object[] values)
        {
            Book book = null;
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM GersonBook where Id=@id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = values[0];

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        book = new Book(reader.GetInt64(0), reader.GetString(1));
                    }
                }
            }
            return book;
        }

        public Collection<Book> ListAll()
        {
            Collection<Book> listAll = new Collection<Book>();
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM GersonBook";
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book(
                            int.Parse(row["Id"].ToString()),
                            row["Name"].ToString()
                        );
                        listAll.Add(book);
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