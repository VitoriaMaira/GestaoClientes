using GestaoClientes.Blazor.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestaoClientes.Blazor.Services;

public sealed class ClienteApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = CriarJsonOptions();

    public async Task<Paginado<ClienteModel>> ListarAsync(
        int pagina,
        int tamanhoPagina,
        string? nome,
        string? cpf,
        StatusCliente status)
    {
        var parametros = new List<string>
        {
            $"pagina={pagina}",
            $"tamanhoPagina={tamanhoPagina}",
            $"status={status}"
        };

        AdicionarParametro(parametros, "nome", nome);
        AdicionarParametro(parametros, "cpf", cpf);

        var response = await httpClient.GetAsync($"api/clientes?{string.Join('&', parametros)}");
        return await LerRespostaAsync<Paginado<ClienteModel>>(response);
    }

    public async Task<ClienteModel> ObterAsync(int id)
    {
        var response = await httpClient.GetAsync($"api/clientes/{id}");
        return await LerRespostaAsync<ClienteModel>(response);
    }

    public async Task<ClienteModel> CriarAsync(ClienteFormModel model)
    {
        var request = new CriarClienteRequest(
            model.Nome,
            model.Cpf,
            model.Email,
            model.Telefone,
            model.DataNascimento!.Value,
            MapearEndereco(model));

        var response = await httpClient.PostAsJsonAsync("api/clientes", request, JsonOptions);
        return await LerRespostaAsync<ClienteModel>(response);
    }

    public async Task AtualizarAsync(int id, ClienteFormModel model)
    {
        var request = new AtualizarClienteRequest(
            model.Nome,
            model.Cpf,
            model.Email,
            model.Telefone,
            model.DataNascimento!.Value);

        var response = await httpClient.PutAsJsonAsync($"api/clientes/{id}", request, JsonOptions);
        await ValidarRespostaAsync(response);
    }

    public Task InativarAsync(int id)
    {
        return EnviarSemConteudoAsync(HttpMethod.Delete, $"api/clientes/{id}");
    }

    public Task AtivarAsync(int id)
    {
        return EnviarSemConteudoAsync(HttpMethod.Put, $"api/clientes/{id}/ativar");
    }

    public async Task<IReadOnlyCollection<EnderecoModel>> ListarEnderecosAsync(int clienteId)
    {
        var response = await httpClient.GetAsync($"api/clientes/{clienteId}/enderecos");
        await ValidarRespostaAsync(response);

        return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<EnderecoModel>>(JsonOptions)
            ?? [];
    }

    public async Task AdicionarEnderecoAsync(int clienteId, EnderecoFormModel model)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"api/clientes/{clienteId}/enderecos",
            MapearEndereco(model),
            JsonOptions);

        await ValidarRespostaAsync(response);
    }

    public async Task AtualizarEnderecoAsync(
        int clienteId,
        int enderecoId,
        EnderecoFormModel model)
    {
        var response = await httpClient.PutAsJsonAsync(
            $"api/clientes/{clienteId}/enderecos/{enderecoId}",
            MapearEndereco(model),
            JsonOptions);

        await ValidarRespostaAsync(response);
    }

    public Task RemoverEnderecoAsync(int clienteId, int enderecoId)
    {
        return EnviarSemConteudoAsync(
            HttpMethod.Delete,
            $"api/clientes/{clienteId}/enderecos/{enderecoId}");
    }

    public Task DefinirEnderecoPrincipalAsync(int clienteId, int enderecoId)
    {
        return EnviarSemConteudoAsync(
            HttpMethod.Put,
            $"api/clientes/{clienteId}/enderecos/{enderecoId}/principal");
    }

    private async Task EnviarSemConteudoAsync(HttpMethod metodo, string uri)
    {
        using var request = new HttpRequestMessage(metodo, uri);
        var response = await httpClient.SendAsync(request);
        await ValidarRespostaAsync(response);
    }

    private static async Task<T> LerRespostaAsync<T>(HttpResponseMessage response)
    {
        await ValidarRespostaAsync(response);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<T>>(JsonOptions);
        if (apiResponse is null || !apiResponse.Sucesso || apiResponse.Dados is null)
        {
            throw new ClienteApiException(apiResponse?.Mensagem ?? "A API retornou uma resposta inválida.");
        }

        return apiResponse.Dados;
    }

    private static async Task ValidarRespostaAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object?>>(JsonOptions);
        throw new ClienteApiException(
            apiResponse?.Mensagem ?? "Não foi possível concluir a operação.",
            (int)response.StatusCode);
    }

    private static EnderecoRequest MapearEndereco(EnderecoFormModel model)
    {
        return new EnderecoRequest(
            model.Cep,
            model.Logradouro,
            model.Numero,
            model.Complemento,
            model.Bairro,
            model.Cidade,
            model.Estado,
            model.Principal);
    }

    private static EnderecoRequest MapearEndereco(ClienteFormModel model)
    {
        return new EnderecoRequest(
            model.EnderecoCep,
            model.EnderecoLogradouro,
            model.EnderecoNumero,
            model.EnderecoComplemento,
            model.EnderecoBairro,
            model.EnderecoCidade,
            model.EnderecoEstado,
            true);
    }

    private static void AdicionarParametro(
        ICollection<string> parametros,
        string nome,
        string? valor)
    {
        if (!string.IsNullOrWhiteSpace(valor))
        {
            parametros.Add($"{nome}={Uri.EscapeDataString(valor.Trim())}");
        }
    }

    private static JsonSerializerOptions CriarJsonOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }
}

public sealed class ClienteApiException(string mensagem, int? statusCode = null)
    : Exception(mensagem)
{
    public int? StatusCode { get; } = statusCode;
}
