using DesafioWeb.Domain.Clients.Entities;
using DesafioWeb.Domain.Clients.Services.Commands;
using DesafioWeb.Domain.Clients.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWeb.Api.Controllers.Clients
{
    [Route("/api/clientes/")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServico clienteServico;
        private readonly IViaCepServico viaCepServico;
        private readonly IReceitaWsServico receitaWsServico;

        public ClienteController(IClienteServico clienteServico,
                                 IViaCepServico viaCepServico,
                                 IReceitaWsServico receitaWsServico)
        {
            this.clienteServico = clienteServico;
            this.viaCepServico = viaCepServico;
            this.receitaWsServico = receitaWsServico;
        }


        [HttpPost]
        public IActionResult Incluir([FromBody] Cliente cliente)
        {
            try
            {
                cliente.Id = 0;
                clienteServico.Incluir(cliente);
                return Ok("Cliente inserido com sucesso");
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "Nenhuma exceção interna";
                var fullErrorMessage = $"Erro ao incluir cliente: {ex.Message}. Detalhes da exceção interna: {innerExceptionMessage}";

                Console.WriteLine(fullErrorMessage);

                return BadRequest(fullErrorMessage);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromBody] Cliente cliente)
        {
            try
            {
                cliente.Id = id;
                clienteServico.Alterar(cliente);
                return Ok($"Cliente com ID {id} alterado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar cliente com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                clienteServico.Excluir(id);
                return Ok($"Cliente deletado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar cliente");
            }
        }

        [HttpGet]
        [Route("/pesquisar")]
        public ActionResult<IEnumerable<Cliente>> Pesquisar([FromQuery] FiltrarClienteComando filtros)
        {
            try
            {
                IEnumerable<Cliente> clientes = clienteServico.Pesquisar(filtros);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao pesquisar cliente: {ex.Message}");
            }
        }

        [HttpGet("cep/{cep}")]
        public async Task<IActionResult> ConsultarCep(string cep)
        {
            try
            {
                var endereco = await viaCepServico.ConsultarCep(cep);
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao consultar CEP: {ex.Message}");
            }
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> ConsultarCnpj(string cnpj)
        {
            try
            {
                var empresa = await receitaWsServico.ConsultarCnpj(cnpj);
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao consultar CNPJ: {ex.Message}");
            }
        }
    }
}
