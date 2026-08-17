# Gestão de Clientes

Aplicação web em .NET 10 para cadastro, consulta, atualização, inativação e
reativação de clientes, incluindo o gerenciamento de seus endereços.

O projeto foi desenvolvido como uma evolução de um CRUD tradicional. Além das
operações básicas, ele protege regras de negócio, separa responsabilidades entre
as camadas e entrega um fluxo completo entre frontend, API e banco de dados.

## Diferenciais do projeto

- **Arquitetura organizada:** separação entre Domain, Application,
  Infrastructure, API e Blazor, seguindo DDD de forma simples e adequada ao
  tamanho da aplicação;
- **Domínio protegido:** regras importantes ficam nas entidades e nos casos de
  uso, evitando lógica de negócio espalhada pelos controllers;
- **Exclusão lógica:** clientes não são apagados do banco. Eles podem ser
  inativados e posteriormente reativados, preservando o histórico;
- **Endereços completos:** um cliente pode possuir vários endereços, mas apenas
  um pode ser principal e o único endereço não pode ser excluído;
- **Paginação eficiente:** filtros e paginação são aplicados diretamente no SQL
  Server, sem carregar todos os registros na memória;
- **Contratos bem definidos:** a API utiliza DTOs específicos para cadastro,
  atualização, consulta e gerenciamento de endereços, sem expor as entidades
  do domínio;
- **Erros padronizados:** validações e exceções são tratadas de forma
  centralizada, com respostas consistentes e códigos HTTP adequados;
- **Frontend desacoplado:** o Blazor possui modelos próprios e consome a API
  exclusivamente por HTTP, sem acessar diretamente a Application ou o banco;
- **Banco reproduzível:** migrations criam a estrutura e um seeder idempotente
  disponibiliza dados de demonstração sem duplicá-los;
- **Qualidade verificada:** regras críticas possuem testes automatizados e o
  projeto foi validado com um fluxo real de cadastro, edição, endereços,
  inativação e reativação;
- **Execução simplificada:** um perfil do Visual Studio inicia API e Blazor
  juntos, enquanto o arquivo `.http` permite testar toda a API rapidamente.

## Tecnologias

- ASP.NET Core Web API e Swagger/OpenAPI;
- Blazor com renderização interativa no servidor;
- SQL Server e Entity Framework Core 10;
- EF Core Migrations e Seeder;
- xUnit para testes automatizados.

## Arquitetura

A solução utiliza DDD simplificado e mantém as responsabilidades separadas:

```text
src/
├── GestaoClientes.Api             # endpoints, Swagger e tratamento de erros
├── GestaoClientes.Application     # casos de uso e contratos
├── GestaoClientes.Domain          # entidades e regras de negócio
├── GestaoClientes.Infrastructure  # EF Core, repositórios, migrations e seeder
└── GestaoClientes.Blazor          # páginas, modelos e cliente HTTP

tests/
└── GestaoClientes.UnitTests
```

O Blazor consome a API por HTTP e não acessa o banco de dados diretamente. Os
controllers são pequenos, os casos de uso coordenam as operações e as entidades
protegem as regras do domínio.

## Executar no Visual Studio

Pré-requisitos:

- Visual Studio com a carga de trabalho **ASP.NET e desenvolvimento Web**;
- SDK do .NET 10;
- SQL Server LocalDB ou outra instância do SQL Server.

Passos:

1. Abra `GestaoClientes.sln` no Visual Studio.
2. Selecione o perfil de inicialização **API e Blazor**.
3. Execute com `F5` ou `Ctrl+F5`.
4. O Swagger e o frontend serão abertos automaticamente.

Endereços locais padrão:

- API: `https://localhost:7216`;
- Swagger: `https://localhost:7216/swagger`;
- Blazor: `https://localhost:7100`.

Se utilizar outro SQL Server, altere a conexão `SqlServer` em
`src/GestaoClientes.Api/appsettings.json`. Se mudar a porta da API, atualize
`ApiUrl` em `src/GestaoClientes.Blazor/appsettings.json`.

## Banco de dados

Na inicialização da API:

1. as migrations são aplicadas;
2. o banco `GestaoClientesDb` é criado quando necessário;
3. o seeder inclui três clientes de demonstração somente quando a tabela está vazia.

Para aplicar as migrations manualmente:

```bash
dotnet ef database update --project src/GestaoClientes.Infrastructure --startup-project src/GestaoClientes.Api
```

## Funcionalidades

- listagem com filtros por nome, CPF e status;
- paginação realizada no banco;
- cadastro e edição de clientes;
- detalhes do cliente;
- ativação e inativação com confirmação;
- adição, edição e exclusão de endereços;
- definição do endereço principal;
- validações no frontend, domínio e API;
- mensagens padronizadas e tratamento centralizado de erros.

## Principais endpoints

```text
POST   /api/clientes
GET    /api/clientes
GET    /api/clientes/{id}
PUT    /api/clientes/{id}
DELETE /api/clientes/{id}
PUT    /api/clientes/{id}/ativar

GET    /api/clientes/{id}/enderecos
POST   /api/clientes/{id}/enderecos
PUT    /api/clientes/{id}/enderecos/{enderecoId}
DELETE /api/clientes/{id}/enderecos/{enderecoId}
PUT    /api/clientes/{id}/enderecos/{enderecoId}/principal
```

O arquivo `src/GestaoClientes.Api/GestaoClientes.Api.http` contém um fluxo
completo para testar os endpoints pelo Visual Studio.

## Regras principais

- CPF e e-mail devem ser válidos e únicos;
- a data de nascimento não pode ser futura;
- todo cliente inicia ativo e possui pelo menos um endereço;
- a exclusão de cliente é lógica;
- somente um endereço pode ser principal;
- o único endereço do cliente não pode ser excluído.

## Testes

Execute:

```bash
dotnet test
```

Os 17 testes automatizados cobrem as regras principais de cliente, CPF e
endereços, incluindo cenários de sucesso e violações das regras de negócio.

## Repositório

[github.com/VitoriaMaira/GestaoClientes](https://github.com/VitoriaMaira/GestaoClientes)
