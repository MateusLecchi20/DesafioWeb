using DesafioWeb.Domain.ValueObects;

namespace DesafioWeb.Domain.Clients.Repositories
{
    public interface IEnderecoRepositorio
    {
        void Incluir(Endereco endereco);
        void Alterar(Endereco endereco);
        void Excluir(int idEndereco);
        Endereco? Buscar(int idEndereco);
    }
}
