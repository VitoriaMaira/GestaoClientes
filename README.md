# Gestão de Clientes

Aplicação .NET 10 para cadastro, consulta, atualização, inativação e reativação de clientes e seus endereços.

## Tecnologias

ASP.NET Core Web API, Blazor, SQL Server, Entity Framework Core 10, xUnit e Swagger.

## Organização

A solução segue DDD simplificado e a mesma direção de dependências do projeto `LojaPedidos`:

```text
src/
├── GestaoClientes.Api/
│   ├── Configurations/
│   ├── Controllers/
│   └── Filters/
├── GestaoClientes.Application/
│   ├── Clientes/          # casos de uso separados por operação
│   ├── Enderecos/
│   └── Common/            # exceções e respostas padronizadas
├── GestaoClientes.Domain/
│   ├── Entities/
│   ├── Enums/
│   ├── Exceptions/
│   ├── Repositories/
│   └── ValueObjects/
├── GestaoClientes.Infrastructure/
│   ├── DataAccess/Mappings/
│   ├── DataAccess/Repositories/
│   ├── DataAccess/Seeds/
│   └── Migrations/
└── GestaoClientes.Blazor/

tests/
└── GestaoClientes.UnitTests/
```

Os controllers apenas coordenam HTTP. Os casos de uso ficam na Application, as invariantes nas entidades, e o acesso ao SQL Server na Infrastructure. As respostas da API usam o contrato `ApiResponse<T>`.

## Executar no Visual Studio

1. Abra `GestaoClientes.sln`.
2. Ajuste a conexão `SqlServer` em `src/GestaoClientes.Api/appsettings.json` caso não use LocalDB.
3. Defina a API como projeto de inicialização e execute.
4. Acesse `https://localhost:<porta>/swagger` para testar os endpoints.

As migrations são aplicadas na inicialização e o seeder cria três clientes apenas se a tabela estiver vazia. Para executar manualmente, use `dotnet ef database update --project src/GestaoClientes.Infrastructure --startup-project src/GestaoClientes.Api`. Execute os testes com `dotnet test`.

## Regras principais

CPF e e-mail são únicos; CPF e e-mail são validados; a data de nascimento não pode ser futura; o cliente inicia ativo; exclusão é lógica; todo cliente possui endereço e há somente um endereço principal.

## Principais endpoints

- `POST /api/clientes`
- `GET /api/clientes`
- `GET /api/clientes/{id}`
- `PUT /api/clientes/{id}`
- `DELETE /api/clientes/{id}`
- `PUT /api/clientes/{id}/ativar`
- `GET|POST /api/clientes/{id}/enderecos`
- `PUT|DELETE /api/clientes/{id}/enderecos/{enderecoId}`
- `PUT /api/clientes/{id}/enderecos/{enderecoId}/principal`
