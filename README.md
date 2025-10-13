# Projeto - EcoTracker

## Como executar localmente com Docker

Para rodar a aplicação EcoTracker localmente utilizando Docker, siga os passos abaixo:

1. Suba todos os Containers utilizando Docker Compose
```bash
docker-compose up --build
```

isso vai subir os containers de production e staging

acesse os respectivos aplicacoes

"8080:80/api/documentation para staging 
"8080:81"/api/documentation para production

para subir cada um inidvidualmente basta
```bash
docker-compose up --build staging

docker-compose up --build production
```

Pipeline CI/CD

O pipeline de CI/CD foi configurado utilizando GitHub Actions, contemplando os ambientes staging (homolog) e production:

CI (Build e Testes)

Executa a cada push para as branches staging e main.

Etapas:

Restaurar pacotes .NET

Build da aplicação

Execução de testes automatizados (dotnet test)

Build e push da imagem Docker para o GitHub Container Registry (GHCR)

CD (Deploy)

Executa após a pipeline de CI.

Etapas:

Deploy automático para o Web App de staging (ecotracker-staging) usando a imagem gerada :staging

Deploy automático para produção (ecotracker-prod) usando a imagem respectiva :latest

Containerização

A aplicação é containerizada usando Docker.

Estratégias adotadas:

Configuração de variáveis de ambiente via ASPNET_ENVIROMENT

Orquestração da aplicação e banco de dados pelo Docker Compose

Possibilidade de subir serviços individualmente ou em conjunto

![Evidencia Docker Compose](https://i.imgur.com/wB8aMOD.png)
![Evidencia Github Actions](https://i.imgur.com/CxQnuiU.png)

link para o deploy em Staging: [ecotracker-staging](ecotracker-staging-hcbmfqhafwgfdzd8.brazilsouth-01.azurewebsites.net/api/documentation)

link para deploy em Production: [ecotracker-production](ecotracker.azurewebsites.net/api/documentation)

Tecnologias utilizadas

Linguagem/Framework: .NET 8, C#, Entity Framework

Banco de dados: Oracle (remoto)

Containerização: Docker, Docker Compose

CI/CD: GitHub Actions

Deploy: Azure Web App (staging e production)

Controle de versão: Git / GitHub




