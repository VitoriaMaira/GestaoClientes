# Gestão de Clientes

Solução .NET 9 em camadas para gerir clientes e endereços, com API, Blazor, SQL Server/EF Core e xUnit.

Abra `GestaoClientes.sln` no Visual Studio. A conexão LocalDB está em `src/GestaoClientes.Api/appsettings.json`; ajuste-a se necessário. Execute a API e acesse `/swagger`. As migrations criam as tabelas e o seeder insere três clientes sem duplicá-los. Use `dotnet test` para os testes.

As regras incluem CPF/e-mail únicos, data de nascimento válida, exclusão lógica, ao menos um endereço e somente um endereço principal.
