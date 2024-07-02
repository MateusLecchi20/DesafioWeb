using DesafioWeb.Domain.Clients.Services.Interfaces;
using DesafioWeb.Domain.ValueObects;
using Newtonsoft.Json;

namespace DesafioWeb.Domain.Clients.Services
{
    public class ViaCepServico : IViaCepServico
    {
        private readonly HttpClient httpClient;

        public ViaCepServico(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Endereco> ConsultarCep(string cep)
        {
            var response = await httpClient.GetAsync($"{cep}/json/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<Endereco>(content);

            if (endereco == null)
            {
                throw new Exception("Endereco nao encontrado");
            }

            return endereco;
        }
    }
}
