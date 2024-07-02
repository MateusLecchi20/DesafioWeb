using DesafioWeb.Domain.Clients.Entities;
using DesafioWeb.Domain.Clients.Services.Interfaces;
using DesafioWeb.Domain.ValueObects;
using Newtonsoft.Json;

namespace DesafioWeb.Domain.Clients.Services
{
    public class ReceitaWsServico : IReceitaWsServico
    {
        private readonly HttpClient httpClient;
        private readonly IViaCepServico viaCepServico;

        public ReceitaWsServico(HttpClient httpClient, IViaCepServico viaCepServico)
        {
            this.httpClient = httpClient;
            this.viaCepServico = viaCepServico;
        }

        public async Task<Cliente> ConsultarCnpj(string cnpj)
        {
            var response = await httpClient.GetAsync($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var empresa = JsonConvert.DeserializeObject<dynamic>(content);

            if (empresa == null)
            {
                throw new Exception("Empresa nao encontrada");
            }

            var cliente = new Cliente()
            {
                Codigo = empresa.Codigo,
                Nome = empresa.Nome,
                CpfCnpj = empresa.cnpj,
                Email = empresa.email,
                Telefone = empresa.telefone,
                Endereco = new Endereco
                {
                    Cep = empresa.cep,
                    Logradouro = empresa.logradouro,
                    Numero = empresa.numero,
                    Complemento = empresa.complemento,
                    Bairro = empresa.bairro,
                    UnidadeFederativa = empresa.uf
                }
            };

            return cliente;
        }
    }
}
