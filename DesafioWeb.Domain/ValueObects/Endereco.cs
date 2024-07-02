using System.ComponentModel.DataAnnotations;

namespace DesafioWeb.Domain.ValueObects
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Cep obrigatorio")]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UnidadeFederativa { get; set; }
    }
}
