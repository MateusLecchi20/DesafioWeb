using DesafioWeb.Domain.Clients.Entities;
using DesafioWeb.Domain.Clients.Repositories;
using DesafioWeb.Domain.Clients.Services.Commands;
using DesafioWeb.Domain.Clients.Services.Interfaces;
using DesafioWeb.Domain.Generics.Exceptions;

namespace DesafioWeb.Domain.Clients.Services
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio clienteRepositorio;
        private readonly IEnderecoRepositorio enderecoRepositorio;

        public ClienteServico(IClienteRepositorio clienteRepositorio,
                              IEnderecoRepositorio enderecoRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
            this.enderecoRepositorio = enderecoRepositorio;
        }

        public void Alterar(Cliente cliente)
        {
            try
            {
                cliente.Alteracao = DateTime.Now.Date;
                enderecoRepositorio.Alterar(cliente.Endereco);
                clienteRepositorio.Alterar(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar cliente: {ex.InnerException?.Message}", ex);
            }
        }

        public void Excluir(int idCliente)
        {
            try
            {
                clienteRepositorio.Excluir(idCliente);
            }
            catch (RegraDeNegocioExcecao)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir cliente: {ex.Message}", ex);
            }
        }

        public void Incluir(Cliente cliente)
        {
            try
            {
                enderecoRepositorio.Incluir(cliente.Endereco);
                cliente.EnderecoId = cliente.Endereco.Id;

                cliente.Inclusao = DateTime.Now.Date;
                cliente.Alteracao = DateTime.Now.Date;
                clienteRepositorio.Incluir(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir cliente: {ex.InnerException?.Message}", ex);
            }
        }

        public IEnumerable<Cliente> Pesquisar(FiltrarClienteComando comandoFiltrar)
        {
            return clienteRepositorio.Pesquisar(comandoFiltrar).ToList();
        }
    }
}
