# language: pt
Funcionalidade: Busca Líderes
  Como um usuário do sistema
  Eu quero visualizar os líderes da competição

  Cenário: Busca Líderes
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/leaderboard"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas/Leaderboard/leaderboard.schema.json"
    Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | True  |

  Cenário: Busca Resumo Líderes
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Quando eu envio a requisição para o endpoint "api/v1/leaderboard/summary"
    Então devo receber o status code 200
    E a resposta deve seguir esse schema "Schemas/Leaderboard/summary.schema.json"
    Então os valores esperados da resposta JSON devem ser
      | campo   | valor |
      | success | True  |