using System.Data;
using MiniApi.Factory;
using MiniApi.Interfaces;
using MiniApi.Models;
using Dapper;

namespace MiniApi.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _connection;

        public ProdutoRepository()
        {
            _connection = new SqlFactory().SqlConnection();
        }

        public bool Delete(int id)
        {
            var query = "UPDATE prova.produto SET situacao = 'I' WHERE id = @id_produto";
            var parametros = new {id_produto = id};
            var result = 0;
            using (_connection)
            {
                result = _connection.Execute(query,parametros);                
            }
            return (result != 0 ? true:false); 
        }

        public ProdutoModel GetProduto(int id_produto)
        {
            var produto = new ProdutoModel();
            var query = 
            @"SELECT * FROM prova.produto P
            INNER JOIN prova.fornecedor F ON P.id_fornecedor = F.id  
            WHERE P.situacao = 'A' AND P.id = @id_p";      
            var parametros = new {id_p = id_produto};  
            using (_connection)
            {
                produto = _connection.Query<ProdutoModel,FornecedorModel,ProdutoModel>(
                    query,
                    map: (prod, forn)=>{
                        prod.Fornecedor = forn;
                        return prod;
                    },
                    parametros,
                    splitOn: "id").Single();
                
                }
                
                return produto;
            }

         public IEnumerable<ProdutoModel> GetProdutos(int page, int tamanho, string order, string search)
         {
             var produtos = new List<ProdutoModel>();
             var query = "SELECT * FROM prova.produto P INNER JOIN prova.fornecedor F ON P.id_fornecedor = F.id WHERE situacao = 'A'";
             if (!String.IsNullOrEmpty(search))
             {
                 query += $" AND desc_produto LIKE '%{search}%'";
             }
             switch (order)
             {
                 case "desc_produto":
                     query += " ORDER BY desc_produto";
                     break;
                 case "data_fab":
                     query += " ORDER BY data_fab";
                     break;
                 case "data_val":
                     query += " ORDER BY data_val";
                     break;
             }
             query += " LIMIT @tam OFFSET @page ";

             var parametros = new {page = (page-1)*tamanho, tam = tamanho};      

             using (_connection)
             {
                 produtos = _connection.Query<ProdutoModel,FornecedorModel,ProdutoModel>(
                    query,
                    map: (prod, forn)=>{
                        prod.Fornecedor = forn;
                        return prod;
                    },
                    parametros,
                    splitOn: "id").ToList();
             }
             return produtos;
         }

         public bool InsertProduto(string descricao, DateTime data_fab, DateTime data_val, int id_forn)
         {
             var query = 
             @"
             INSERT INTO produto (desc_produto, situacao, data_fab, data_val, id_fornecedor)
             VALUES (@desc, 'A', @date_fab, @date_val, @id_fornecedo);
             ";
             var parametros = new {desc = descricao, date_fab = data_fab.Date, date_val = data_val.Date, id_fornecedo = id_forn};
             int result = 0;
             using (_connection)
             {
                 if (data_fab.CompareTo(data_val) < 0)
                 {
                    result = _connection.Execute(query, parametros);
                    
                 }   
             }
             return (result != 0 ? true:false); 
         }

         public bool Update(int id_prod,string descricao, DateTime data_fab, DateTime data_val, int id_forn)
         {
             var query = @"
             UPDATE prova.produto 
             SET desc_produto = @desc, data_fab = @date_fab, data_val = @date_val, id_fornecedor = @fornecedor
             WHERE id = @id_produto
             ";
             var parametros = new {desc = descricao, date_fab = data_fab.Date, date_val = data_val.Date, fornecedor = id_forn, id_produto = id_prod};
             int result = 0;
             using (_connection)
             {
                 if (data_fab.CompareTo(data_val) < 0)
                 {
                     result = _connection.Execute(query,parametros);
                 }
             }
             return (result != 0 ? true:false); 
         }

    }
}
