using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleLivros.Util
{
    public class ConnectionFactory : IConnection,IDisposable
    {
        private SqlConnection MyConnection;
        public ConnectionFactory()
        {
            MyConnection = new SqlConnection("Connection database");
        }

        public SqlConnection Open()
        {
            if(MyConnection.State == ConnectionState.Closed) MyConnection.Open();
            
            return MyConnection;
        }
        public SqlConnection Find()
        {
            return this.Open();
        }

        public void Close()
        {
            if(MyConnection.State == ConnectionState.Open) MyConnection.Close();
        }

        public void Dispose()
        {
            this.Close();
            GC.SuppressFinalize(this);
        }
    }
}