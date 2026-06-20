namespace PessoasApp.Blazor.Models {
    public class Pessoa {

        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }
    }
}
