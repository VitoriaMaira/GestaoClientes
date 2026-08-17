# Gestão de Clientes

Aplicação .NET 10 para cadastro, consulta, atualização, inativação e reativação de clientes e seus endereços.

## Tecnologias

ASP.NET Core Web API, Blazor, SQL Server, Entity Framework Core 10, xUnit e Swagger.

## Organização

`Domain` contém entidades e regras; `Application`, contratos e serviços; `Infrastructure`, EF Core, repositório, migration e seeder; `Api`, endpoints e tratamento de erros; `Blazor`, interface HTTP; `UnitTests`, testes automatizados.

## Executar no Visual Studio

1. Abra `GestaoClientes.sln`.
2. Ajuste a conexão `SqlServer` em `src/GestaoClientes.Api/appsettings.json` caso não use LocalDB.
3. Defina a API como projeto de inicialização e execute.
4. Acesse `https://localhost:<porta>/swagger` para testar os endpoints.

As migrations são aplicadas na inicialização e o seeder cria três clientes apenas se a tabela estiver vazia. Para executar manualmente, use `dotnet ef database update --project src/GestaoClientes.Infrastructure --startup-project src/GestaoClientes.Api`. Execute os testes com `dotnet test`.

## Regras principais

CPF e e-mail são únicos; CPF e e-mail são validados; a data de nascimento não pode ser futura; o cliente inicia ativo; exclusão é lógica; todo cliente possui endereço e há somente um endereço principal.
