using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DesafioWeb.Domain.Clients.Enumerators;
using DesafioWeb.Domain.ValueObects;

namespace DesafioWeb.Domain.Clients.Entities
{
    public class Cliente
    {
        [Range(1, 999999, ErrorMessage = "Id fora do range")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Codigo obrigatorio")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Tipo obrigatorio")]
        public TipoPessoaEnum Tipo { get; set; }

        [Required(ErrorMessage = "CpfCnpj obrigatorio")]
        public string CpfCnpj { get; set; }
        public int RgOuInscricaoEstadual { get; set; }

        [Required(ErrorMessage = "Nome obrigatorio")]
        public string Nome { get; set; }
        public string NomeAbreviado { get; set; }

        [JsonIgnore]
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        [Required(ErrorMessage = "Email obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone obrigatorio")]
        public string Telefone { get; set; }
        public DateTime Inclusao { get; set; }
        public DateTime Alteracao { get; set; }

        public Cliente()
        {

        }

        public Cliente(int id, string codigo, TipoPessoaEnum tipo, string cpfCnpj, int rgOuInscricaoEstadual, string nome, string nomeAbreviado, Endereco endereco, string email, string telefone, DateTime inclusao, DateTime alteracao)
        {
            Id = id;
            Codigo = codigo;
            Tipo = tipo;
            CpfCnpj = cpfCnpj;
            RgOuInscricaoEstadual = rgOuInscricaoEstadual;
            Nome = nome;
            NomeAbreviado = nomeAbreviado;
            EnderecoId = endereco.Id;
            Endereco = endereco;
            Email = email;
            Telefone = telefone;
            Inclusao = inclusao;
            Alteracao = alteracao;
        }
    }
}
