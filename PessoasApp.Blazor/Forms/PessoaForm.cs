using System.ComponentModel.DataAnnotations;

namespace PessoasApp.Blazor.Forms {
    public class PessoaForm {

        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da pessoa.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o email da pessoa.")]
        [EmailAddress(ErrorMessage = "Por favor, informe um email válido.")]
        [MaxLength(150, ErrorMessage = "O email deve ter no máximo {1} caracteres.")]
        public string? Email { get; set; }
    }
}
