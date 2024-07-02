using DesafioWeb.Domain.Clients.Entities;
using DesafioWeb.Domain.Clients.Services.Commands;

namespace DesafioWeb.Domain.Clients.Services.Interfaces
{
    public interface IClienteServico
    {
        void Incluir(Cliente cliente);
        void Excluir(int idCliente);
        IEnumerable<Cliente> Pesquisar(FiltrarClienteComando comandoFiltrar);
        void Alterar(Cliente cliente);
    }
}
