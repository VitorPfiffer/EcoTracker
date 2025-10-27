# language: pt
Funcionalidade: Login de usuário
  Como um usuário do sistema
  Eu quero autenticar com credenciais válidas
  Para acessar as funcionalidades protegidas da aplicação

  Cenário: Login bem-sucedido
    Dado que eu informei o email "admin@gmail.com"
    E a senha "admin123"
    Quando eu estou autenticado em "api/v1/auth/login"
    Então devo receber o status code 201
    E o token de autenticação deve ser retornado

  Cenário: Login com credenciais inválidas
    Dado que eu informei o email "admin"
    E a senha "senha_errada"
    Quando eu estou autenticado em "api/v1/auth/login"
    Então devo receber o status code 400
    E o erro deve ser retornado

  Cenário: Login com campos em branco
    Dado que eu informei o email ""
    E a senha ""
    Quando eu estou autenticado em "api/v1/auth/login"
    Então devo receber o status code 400
    E o erro deve ser retornado
