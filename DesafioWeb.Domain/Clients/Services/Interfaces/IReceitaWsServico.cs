using DesafioWeb.Domain.Clients.Entities;

namespace DesafioWeb.Domain.Clients.Services.Interfaces
{
    public interface IReceitaWsServico
    {
        Task<Cliente> ConsultarCnpj(string cnpj);
    }
}
