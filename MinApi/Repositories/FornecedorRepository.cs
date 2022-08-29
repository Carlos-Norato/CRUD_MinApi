using System.Data;
using MiniApi.Factory;
using MiniApi.Interfaces;
using MiniApi.Models;
using Dapper;

namespace MiniApi.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly IDbConnection _connection;

        public FornecedorRepository()
        {
            _connection = new SqlFactory().SqlConnection();
        }

      
        public IEnumerable<FornecedorModel> GetFornecedors()

        {
            var fornecedores = new List<FornecedorModel>();
            var query = "SELECT * FROM prova.fornecedor";        
            using (_connection)
            {
                fornecedores = _connection.Query<FornecedorModel>(query).ToList();
            }
            return fornecedores;
        }

        public bool InsertFornecedor(FornecedorModel fornecedor)
        {
            var query = 
            @"
            INSERT INTO fornecedor (desc_fornecedor, cnpj)
            VALUES(@desc, @cnpj)
            ";

            var parametros = new {desc = fornecedor.Desc_fornecedor, cnpj = fornecedor.Cnpj};
            int result = 0;
            using (_connection)
            {
                result = _connection.Execute(query,parametros);
            }
            return (result != 0 ? true:false); 
        }

        public bool UpdateFornecedorDesc(string descricao, int id_fornecedor)
        {
            var query = 
            @"UPDATE prova.fornecedor
            SET desc_fornecedor = @desc
            WHERE id = @id_fornecedor
            ";
            var parametros = new {desc = descricao, id_fornecedor = id_fornecedor};
            int result =0;
            using (_connection)
            {
                result = _connection.Execute(query,parametros);
            }

            return (result != 0 ? true:false); 
        }
   
        public bool Delete(int id)
        {
            var query = $"DELETE FROM prova.fornecedor WHERE id = @id_fornecedor";
            var parametros = new {id_fornecedor = id};
            int result = 0;
            using (_connection)
            {
                result = _connection.Execute(query, parametros);
            }

            return (result != 0 ? true:false); 
        }

        public FornecedorModel GetFornecedor(int id)
        {
            var fornecedore = new FornecedorModel();
            var query = "SELECT * FROM prova.fornecedor WHERE id = @id_forn";        
            var parametros = new {id_forn = id};
            using (_connection)
            {
                fornecedore = _connection.Query<FornecedorModel>(query, parametros).Single();
            }
            return fornecedore;
        }
    }

}