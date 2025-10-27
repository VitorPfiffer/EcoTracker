# language: pt
Funcionalidade: Gerenciamento de WasteDisposal
  Como um usuário do sistema
  Eu quero gerenciar os descartes de resíduos
  Para criar, visualizar, atualizar e remover registros de descarte

  Cenário: Busca 1 WasteDisposal
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/waste-disposals/a8aeb6ec-4ebd-4726-9fac-dd8560e10764"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas\WasteDisposal\singleWasteDisposal.schema.json"

  Cenário: Busca WasteDisposal Paginado
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/waste-disposals?Page=1&PageSize=10"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas\WasteDisposal\arrayWasteDisposal.schema.json"

  Cenário: Criar WasteDisposal com sucesso
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição POST para "api/v1/waste-disposals" com o payload:
    """
    {
      "wasteType": "organico",
      "quantity": 3,
      "unit": "kg",
      "date": "2025-05-12T14:30:00",
      "userId": "af507245-61e2-4af6-a617-14dd4b4c10dd"
    }
    """
    Então devo receber o status code 201
    Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | True  |

  Cenário: Atualizar WasteDisposal com sucesso
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição PUT para "api/v1/waste-disposals/a8aeb6ec-4ebd-4726-9fac-dd8560e10764" com o payload:
    """
    {
      "wasteType": "organico",
      "quantity": 3,
      "unit": "kg",
      "date": "2025-05-12T14:30:00",
      "userId": "af507245-61e2-4af6-a617-14dd4b4c10dd"
    }
    """
    Então devo receber o status code 201
    Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | True  |

  Cenário: Deletar WasteDisposal inexistente
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio uma requisição DELETE para "api/v1/waste-disposals/567a2035-7999-455e-898a-9ca39fefa7d9"
    Então devo receber o status code 400
    Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | False |

  Cenário: Lista WasteDisposal por usuario
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/waste-disposals/user/af507245-61e2-4af6-a617-14dd4b4c10dd"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas\WasteDisposal\userWasteDisposal.schema.json"

  Cenário: Lista Tipos de Residuos por mês
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/waste-disposals/monthly?year=2025&month=10"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas\WasteDisposal\monthly.schema.json"




