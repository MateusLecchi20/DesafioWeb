using DesafioWeb.Domain.Clients.Entities;
using DesafioWeb.Domain.Clients.Services.Commands;
using DesafioWeb.Domain.ValueObects;

namespace DesafioWeb.Domain.Clients.Repositories
{
    public interface IClienteRepositorio
    {
        void Incluir(Cliente cliente);
        void Alterar(Cliente cliente);
        void Excluir(int idCliente);
        Cliente? Buscar(int idCliente);
        IQueryable<Cliente> Pesquisar(FiltrarClienteComando filtros);
        void AlterarEndereco(Endereco endereco);
    }
}
