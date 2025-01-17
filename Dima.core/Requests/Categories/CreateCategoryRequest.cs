using System.ComponentModel.DataAnnotations;

namespace Dima.core.Requests.Categories;
public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter no máximo 80 caracteres")]
    public string Title {get; set; } = string.Empty;

    [Required(ErrorMessage ="Descrição inválida")]
    public string Description {get; set; } = string.Empty;
}
