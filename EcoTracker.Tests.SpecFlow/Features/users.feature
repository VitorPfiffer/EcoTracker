# language: pt
Funcionalidade: Gerenciamento de Users
  Como um usuário do sistema
  Eu quero gerenciar os usuários
  Para criar, visualizar, atualizar e remover usuários

  Cenário: Busca 1 User
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/users/af507245-61e2-4af6-a617-14dd4b4c10dd"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas\User\singleUser.schema.json"

  Cenário: Busca Users Paginado
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/users?Page=1&PageSize=10"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas\User\arrayUser.schema.json"

  Cenário: Criar User com sucesso
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição POST para "api/v1/users" com o payload:
    """
    {
      "email": "vitão@gmail.com",
      "username": "vitão",
      "password": "123456",
      "role": 0
    }
    """
    Então devo receber o status code 400
    Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | False |

  Cenário: Atualizar User com sucesso
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição PUT para "api/v1/users/af507245-61e2-4af6-a617-14dd4b4c10dd" com o payload:
    """
{
  "email": "vitao14@gmail.com",
  "username": "Vitão14",
  "role": 0
}
    """
    Então devo receber o status code 400
    Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | False  |

  Cenário: Deletar User inexistente
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição DELETE para "api/v1/users/d7b32636-c7a6-40e9-8f7b-d2be7d5a21c1"
    Então devo receber o status code 400
    Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | False |
