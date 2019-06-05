using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleLivros.Util
{
    public interface IConnection : IDisposable
    {
        // metodo para abrir conexão
        SqlConnection Open();

        // quando a conexão estiver aberta, a busca não precisa abrir novamente
        SqlConnection Find();

        // fechar conexão
        void Close();
    }
}
