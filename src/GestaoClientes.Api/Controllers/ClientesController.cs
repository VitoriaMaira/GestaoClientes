using GestaoClientes.Application.Clientes.AlterarStatus;
using GestaoClientes.Application.Clientes.Atualizar;
using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Application.Clientes.Consultar;
using GestaoClientes.Application.Clientes.Criar;
using GestaoClientes.Application.Clientes.Listar;
using GestaoClientes.Application.Enderecos;
using GestaoClientes.Application.Common.Responses;
using Microsoft.AspNetCore.Mvc;
namespace GestaoClientes.Api.Controllers;
[ApiController,Route("api/clientes")]
public sealed class ClientesController(ICriarClienteUseCase criar,IListarClientesUseCase listar,IConsultarClienteUseCase consultar,IAtualizarClienteUseCase atualizar,IAlterarStatusClienteUseCase status,IGerenciarEnderecosUseCase enderecos) : ControllerBase
{
    [HttpPost] public async Task<IActionResult> Criar(CriarClienteRequest request){var response=await criar.ExecutarAsync(request);return CreatedAtAction(nameof(Obter),new{id=response.Id},ApiResponse<ClienteResponse>.Ok("Cliente cadastrado com sucesso.",response));}
    [HttpGet] public async Task<IActionResult> Listar([FromQuery]ListarClientesQuery query)=>Ok(ApiResponse<Paginado<ClienteResponse>>.Ok("Clientes listados com sucesso.",await listar.ExecutarAsync(query)));
    [HttpGet("{id:int}")] public async Task<IActionResult> Obter(int id)=>Ok(ApiResponse<ClienteResponse>.Ok("Cliente consultado com sucesso.",await consultar.ExecutarAsync(id)));
    [HttpPut("{id:int}")] public async Task<IActionResult> Atualizar(int id,AtualizarClienteRequest request){await atualizar.ExecutarAsync(id,request);return NoContent();}
    [HttpDelete("{id:int}")] public async Task<IActionResult> Inativar(int id){await status.InativarAsync(id);return NoContent();}
    [HttpPut("{id:int}/ativar")] public async Task<IActionResult> Ativar(int id){await status.AtivarAsync(id);return NoContent();}
    [HttpGet("{id:int}/enderecos")] public Task<IReadOnlyCollection<EnderecoResponse>> Enderecos(int id)=>enderecos.ListarAsync(id);
    [HttpPost("{id:int}/enderecos")] public async Task<IActionResult> AdicionarEndereco(int id,EnderecoRequest request){await enderecos.AdicionarAsync(id,request);return NoContent();}
    [HttpPut("{id:int}/enderecos/{enderecoId:int}")] public async Task<IActionResult> AtualizarEndereco(int id,int enderecoId,EnderecoRequest request){await enderecos.AtualizarAsync(id,enderecoId,request);return NoContent();}
    [HttpDelete("{id:int}/enderecos/{enderecoId:int}")] public async Task<IActionResult> RemoverEndereco(int id,int enderecoId){await enderecos.RemoverAsync(id,enderecoId);return NoContent();}
    [HttpPut("{id:int}/enderecos/{enderecoId:int}/principal")] public async Task<IActionResult> Principal(int id,int enderecoId){await enderecos.DefinirPrincipalAsync(id,enderecoId);return NoContent();}
}
