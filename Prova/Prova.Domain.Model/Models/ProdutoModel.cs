namespace Prova.Domain.Model.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }

        public string? Desc_produto { get; set; }

        public string? Situacao { get; set; } 

        public DateTime Data_fab { get; set; }

        public DateTime Data_val { get; set; }

        public FornecedorModel? Fornecedor { get; set; }

    }
}
