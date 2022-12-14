using Prova.Domain.Model.Models;

namespace Prova.Domain.Interfaces
{
    public interface IFornecedorRepository
    {
        IEnumerable<FornecedorModel> GetFornecedors();

        FornecedorModel GetFornecedor(int id);

        bool InsertFornecedor(FornecedorModel fornecedor);

        bool UpdateFornecedorDesc(string desc, int id_fornecedor);

        bool Delete(int id); 
       
    }
}