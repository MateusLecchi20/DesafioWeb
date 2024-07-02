using DesafioWeb.Domain.ValueObects;

namespace DesafioWeb.Domain.Clients.Services.Interfaces
{
    public interface IViaCepServico
    {
        Task<Endereco> ConsultarCep(string cep);
    }
}
