# API de Ordens de Produção

Esta é uma API REST desenvolvida em .NET para gerenciar ordens de produção e apontamentos. Ela permite consultar ordens, visualizar produções por usuário e registrar novos apontamentos de produção, incluindo uma série de validações de negócio.

## Descrição da API

A API foi projetada para ser o back-end de um sistema de controle de produção. Ela se conecta a um banco de dados SQL Server para persistir e consultar os dados, seguindo uma arquitetura em camadas (Controller, Service, Repository) para uma clara separação de responsabilidades.

### Funcionalidades Principais

- **Consultar Ordens de Produção**: Retorna uma lista detalhada de todas as ordens de produção, incluindo informações do produto e dos materiais necessários.
- **Consultar Produção por Usuário**: Retorna todos os apontamentos de produção realizados por um usuário específico, identificado por seu e-mail.
- **Registrar Apontamento de Produção**: Permite que um usuário registre um novo apontamento de produção. Este endpoint realiza uma série de validações para garantir a integridade dos dados, como:
  - Verificação da existência do usuário.
  - Validação da data do apontamento contra o período permitido para o usuário.
  - Verificação da existência da ordem de produção.
  - Validação da quantidade produzida.
  - Validação do material utilizado.
  - Validação do tempo de ciclo.

## Tecnologias Utilizadas

- **.NET 6** (ou superior)
- **ASP.NET Core** para a construção da API
- **Entity Framework Core** como ORM para interação com o banco de dados
- **SQL Server** como sistema de gerenciamento de banco de dados
- **Swagger (OpenAPI)** para documentação e teste interativo da API

---

## 🚀 Como Rodar o Projeto

Siga os passos abaixo para configurar e executar o projeto em seu ambiente de desenvolvimento.

### Pré-requisitos

- **.NET SDK**: Certifique-se de ter o .NET SDK (versão 6 ou superior) instalado.
- **SQL Server**: Você precisa de uma instância do SQL Server (pode ser a versão Express, Developer ou outra) em execução.
- **Um editor de código**: Visual Studio 2022, VS Code, ou JetBrains Rider.

### 1. Clone o Repositório

```bash
git clone <https://github.com/Wellington-1991/CSharp-api-orders.git>
cd api-orders
```

### 2. Configure a Conexão com o Banco de Dados

A API precisa saber como se conectar ao seu banco de dados SQL Server.

1.  Abra o arquivo `appsettings.json` na raiz do projeto.
2.  Encontre a seção `ConnectionStrings`.
3.  Altere o valor de `DefaultConnection` para a string de conexão do seu SQL Server.

    **Exemplo para SQL Server local:**
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=ApiOrdersDb;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```
    *   `Server`: O endereço da sua instância SQL Server (ex: `localhost`, `.\SQLEXPRESS`).
    *   `Database`: O nome que você quer dar ao banco de dados (ex: `ApiOrdersDb`). O EF Core irá criá-lo para você.

### 3. Aplique as Migrations do Banco de Dados

O Entity Framework Core usará as "migrations" para criar o banco de dados e todas as tabelas para você, com base nos modelos definidos no código.

Abra um terminal ou o "Package Manager Console" no diretório do projeto (`api-orders`) e execute o seguinte comando:

```bash
dotnet ef database update
```

Este comando irá ler as configurações de migração do projeto, conectar-se ao banco de dados e criar todas as tabelas (`Order`, `Product`, `User`, etc.).

### 4. Execute a Aplicação

Agora você está pronto para iniciar a API.

**Via Visual Studio:**
- Pressione `F5` ou o botão de play "Start Debugging".

**Via linha de comando:**
```bash
dotnet run
```

A API estará em execução, geralmente em `http://localhost:5000` ou `https://localhost:5001`. O terminal mostrará o endereço exato.

### 5. Teste com o Swagger

Após iniciar a aplicação, abra seu navegador e acesse a URL da interface do Swagger, que geralmente é:

**`http://localhost:[SUA_PORTA]/swagger`**

Lá, você encontrará uma documentação interativa de todos os endpoints, permitindo que você os teste diretamente pelo navegador.

---

## 📖 Documentação dos Endpoints

### GET /api/orders/getOrders

- **Descrição**: Retorna uma lista de todas as ordens de produção cadastradas.
- **Resposta de Sucesso (200 OK)**:
  ```json
  {
    "orders": [
      {
        "order": "111",
        "quantity": 100.00,
        "productCode": "111",
        "productDescription": "Produto 111",
        "image": "image2.jpg",
        "cycleTime": 36.30,
        "materials": [
          { "materialCode": "ABC-001", "materialDescription": "Material ABC 001" }
        ]
      }
    ]
  }
  ```

### GET /api/orders/getProduction

- **Descrição**: Retorna a lista de produções de um usuário específico.
- **Parâmetros**:
  - `email` (string, query): O e-mail do usuário a ser consultado.
- **Exemplo de Chamada**: `/api/orders/getProduction?email=teste@sequor.com.br`
- **Resposta de Sucesso (200 OK)**:
  ```json
  {
    "productions": [
      { "order": "AAA1", "date": "2022-02-13T10:33:03", "quantity": 1, "materialCode": "A1B", "cycleTime": 30.3 }
    ]
  }
  ```
  *Se o usuário não tiver produções, retorna uma lista vazia: `{ "productions": [] }`.*

### POST /api/orders/setProduction

- **Descrição**: Registra um novo apontamento de produção.
- **Corpo da Requisição (Body)**:
  ```json
  {
    "email": "teste@sequor.com.br",
    "order": "111",
    "productionDate": "2024-05-21",
    "productionTime": "10:30:00",
    "quantity": 10.0,
    "materialCode": "ABC-001",
    "cycleTime": 35.0
  }
  ```
- **Resposta de Sucesso (200 OK)**:
  ```json
  {
    "status": 200,
    "type": "S",
    "description": "Apontamento realizado com sucesso."
  }
  ```
- **Resposta de Erro de Validação (400 Bad Request)**:
  ```json
  {
    "status": 201,
    "type": "E",
    "description": "Falha no apontamento - Usuário não cadastrado!"
  }
  ```



