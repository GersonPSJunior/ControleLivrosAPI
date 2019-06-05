using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleLivros.Dao
{
    // estou criando uma interface tipada ou seja posso utilizar qualquer entidade nela
    // where está fazendo a restrição de tipo generico
    // class o argumento de tipo deve ser um tipo de referência
    // new() o argumento de tipo deve ter um construtor público sem parâmetros
    interface IDao<T> : IDisposable
        where T: class, new()
    {
        // inserir registro
        void Insert(T model);
        // atualiza registro
        void Update(T model);
        // deleta registro
        bool Delete(long id);
        // busca registro especifico
        // params recebe quantidade variavel de parametros
        T FindId(params Object[] values);
        // retorna todos os registros
        Collection<T> ListAll();
    }
}
