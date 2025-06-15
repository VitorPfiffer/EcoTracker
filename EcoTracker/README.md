# Template .NET API

Este repositório é um template para gerar uma estrutura com 5 projetos em .NET, incluindo uma API com um controller de autenticação e um CRUD completo de usuário. A solução segue a arquitetura DDD (Domain-Driven Design) e já inclui diversas configurações necessárias para acelerar o desenvolvimento.

## Tecnologias Utilizadas

- .NET
- Entity Framework Core
- Serilog
- Notification Manager
- Action Filters

## Estrutura do Projeto

A solução é composta pelos seguintes projetos:

1. **API**: Contém os endpoints da aplicação.
2. **Domain**: Define as entidades e regras de negócio.
3. **Application**: Contém os casos de uso e serviços.
4. **Infrastructure**: Responsável pela persistência de dados e configurações.
5. **Tests**: TODO

## Instalação e Uso

### Instalação do Template

Para instalar este template localmente, execute o seguinte comando:

```sh
 dotnet new --install .
```

### Criando um Novo Projeto a Partir do Template

Após instalar o template, você pode criar um novo projeto baseado nele com o seguinte comando:

```sh
 dotnet new nome-do-template -n NomeDoProjeto
```

Isso criará uma nova pasta `NomeDoProjeto` com toda a estrutura pronta para uso.

### Executando o Projeto

1. Navegue até a pasta do novo projeto:
   ```sh
   cd NomeDoProjeto
   ```
2. Restaure as dependências:
   ```sh
   dotnet restore
   ```
3. Configure as variáveis de ambiente no arquivo `appsettings.json`.
4. Execute as migrações do banco de dados:
   ```sh
   dotnet ef database update
   ```
5. Execute a aplicação:
   ```sh
   dotnet run
   ```

