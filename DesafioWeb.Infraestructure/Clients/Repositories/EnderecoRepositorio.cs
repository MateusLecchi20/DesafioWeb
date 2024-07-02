using DesafioWeb.Domain.Clients.Repositories;
using DesafioWeb.Domain.ValueObects;
using DesafioWeb.Infraestructure.Data;

namespace DesafioWeb.Infraestructure.Clients.Repositories
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        private readonly AppDbContext context;
        public EnderecoRepositorio(AppDbContext context)
        {
            this.context = context;
        }
        public void Alterar(Endereco endereco)
        {
            context.Enderecos.Update(endereco);
            context.SaveChanges();
        }

        public Endereco? Buscar(int idEndereco)
        {
            return context.Enderecos.Find(idEndereco);
        }

        public void Excluir(int idEndereco)
        {
            var endereco = context.Enderecos.Find(idEndereco);
            if (endereco != null)
            {
                context.Enderecos.Remove(endereco);
                context.SaveChanges();
            }
        }

        public void Incluir(Endereco endereco)
        {
            context.Enderecos.Add(endereco);
            context.SaveChanges();
        }
    }
}
