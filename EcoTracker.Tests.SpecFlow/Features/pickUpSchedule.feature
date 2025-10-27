# language: pt
Funcionalidade: Gerenciamento de PickUpSchedule
  Como um usuário do sistema
  Eu quero gerenciar os agendamentos de coleta
  Para criar, visualizar, atualizar e remover agendamentos
  Cenário: Busca 1 PickUpSchedule
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/pickup-schedule/b4fae3e0-10d2-4655-adfc-b22d9035b5ce"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas\PickUpSchedule\singlePickUpSchedule.schema.json"
  Cenário: Busca PickUpSchedule Paginado
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/pickup-schedule?Page=1&PageSize=10"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas\PickUpSchedule\arrayPickUpSchedule.schema.json"

  Cenário: Criar PickUpSchedule com sucesso
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição POST para "api/v1/pickup-schedule" com o payload:
    """
    {
      "street": "Rua Exemplo",
      "number": "123",
      "neighborhood": "Bairro Exemplo",
      "city": "Cidade Exemplo",
      "state": "PR",
      "postalCode": "12345-678",
      "wasteType": "lixo",
      "scheduledDate": "now"
    }
    """
    Então devo receber o status code 201
     Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | True  |




   Cenário: Atualizar PickUpSchedule com sucesso
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição PUT para "api/v1/pickup-schedule/b4fae3e0-10d2-4655-adfc-b22d9035b5ce" com o payload:
    """
    {
      "street": "Rua Exemplo",
      "number": "123",
      "neighborhood": "Bairro Exemplo",
      "city": "Cidade Exemplo",
      "state": "PR",
      "postalCode": "12345-678",
      "wasteType": "lixo",
      "scheduledDate": "now"
    }
    """
    Então devo receber o status code 201
     Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | True  |

    

   Cenário: Deletar PickUpSchedule inexistente
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição DELETE para "api/v1/pickup-schedule/4279a3df-e838-4338-9414-813cab9a9e5f"
    Então devo receber o status code 400
     Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | False  |