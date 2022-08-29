using MiniApi.Models;

namespace MiniApi.Interfaces
{
    public interface IProdutoRepository
    {
        IEnumerable<ProdutoModel> GetProdutos(int page, int tamanho, string order, string search);

        ProdutoModel GetProduto(int id);

        bool InsertProduto(string descricao, DateTime data_fab, DateTime data_val, int id_forn);

        bool Delete(int id);

        bool Update(int id_prod,string descricao, DateTime data_fab, DateTime data_val, int id_forn);

    }
}