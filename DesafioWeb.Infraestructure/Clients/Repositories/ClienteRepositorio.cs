using DesafioWeb.Domain.Clients.Entities;
using DesafioWeb.Domain.Clients.Repositories;
using DesafioWeb.Domain.Clients.Services.Commands;
using DesafioWeb.Domain.Generics.Exceptions;
using DesafioWeb.Domain.ValueObects;
using DesafioWeb.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioWeb.Infraestructure.Clients.Repositories
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly AppDbContext _context;
        private readonly IEnderecoRepositorio _enderecoRepositorio;

        public ClienteRepositorio(AppDbContext context,
                                  IEnderecoRepositorio enderecoRepositorio)
        {
            _context = context;
            _enderecoRepositorio = enderecoRepositorio;
        }

        public void Alterar(Cliente cliente)
        {
            try
            {
                _context.Entry(cliente).Reload();

                cliente.Alteracao = DateTime.Now.Date;
                _enderecoRepositorio.Alterar(cliente.Endereco);
                _context.Clientes.Update(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar cliente: {ex.InnerException?.Message}", ex);
            }
        }

        public void AlterarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public Cliente? Buscar(int idCliente)
        {
            return _context.Clientes.Find(idCliente);
        }

        public void Excluir(int idCliente)
        {
            try
            {
                var cliente = _context.Clientes.Find(idCliente);
                if (cliente == null)
                {
                    throw new RegraDeNegocioExcecao("Cliente nao encontrado");
                }
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Erro ao excluir cliente: {ex.Message}", ex);
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
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir cliente: {ex.InnerException?.Message}", ex);
            }
        }

        public IQueryable<Cliente> Pesquisar(FiltrarClienteComando filtros)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(filtros.CodigoCliente))
            {
                query = query.Where(c => c.Codigo.Contains(filtros.CodigoCliente));
            }

            if (!string.IsNullOrEmpty(filtros.CpfCnpjCliente))
            {
                query = query.Where(c => c.CpfCnpj.Contains(filtros.CpfCnpjCliente));
            }

            if (!string.IsNullOrEmpty(filtros.Nome))
            {
                query = query.Where(c => c.Nome.Contains(filtros.Nome));
            }

            return query;
        }
    }
}
