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
    public class UserDao : IDao<User>, IDisposable
    {
        private IConnection connection;
        public UserDao(IConnection connection)
        {
            this.connection = connection;
        }

        public void Insert(User model)
        {
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO GersonUser(name,login, password) VALUES (@Name,@Login,@Password)";

                command.Parameters.Add("@Name", SqlDbType.Text).Value = model.Name;
                command.Parameters.Add("@Login", SqlDbType.Text).Value = model.Login;
                command.Parameters.Add("@Password", SqlDbType.Text).Value = model.Password;
                command.ExecuteNonQuery();
            }
        }

        public void Update(User model)
        {
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE GersonUser SET name=@Name, login=@Login, password=@Password where id=@Id";

                command.Parameters.Add("@Name", SqlDbType.Text).Value = model.Name;
                command.Parameters.Add("@Login", SqlDbType.Text).Value = model.Login;
                command.Parameters.Add("@Password", SqlDbType.Text).Value = model.Password;
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
                command.CommandText = "DELETE FROM GersonUser where id=@Id";
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = model;

                // verifica se existe esse id
                if (command.ExecuteNonQuery() > 0) result = true;
            }
            return result;
        }

        public User FindId(params object[] values)
        {
            User user = null;
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM GersonUser where id=@id";

                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = values[0];

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user = new User(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }
                }
            }
            return user;
        }

        public Collection<User> ListAll()
        {
            Collection<User> listAll = new Collection<User>();
            using (SqlCommand command = connection.Find().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM GersonUser";
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        User user = new User(
                            int.Parse(row["id"].ToString()),
                            row["name"].ToString(),
                            row["login"].ToString(),
                            row["password"].ToString()
                        );
                        listAll.Add(user);
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