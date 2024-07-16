using System.ComponentModel.DataAnnotations;
using Dima.core.Enums;

namespace Dima.core.Requests.Transactions;
public class CreateTransactionRequest : Request
{
    [Required(ErrorMessage = "Título inválido")]
    public string Title {get; set; } = string.Empty;

    [Required(ErrorMessage ="Tipo inválido")]
    public EtransactionType Type {get; set;}

    [Required(ErrorMessage ="Categoria Inválida")]
    public decimal Amount { get; set;}

    [Required(ErrorMessage ="Data inválida")]
    public DateTime? PaidOrReceveidAt { get; set; }

    [Required(ErrorMessage = "Categoria invalida")]

    public long CategoryId { get; set; }


}