# EcoTracker

O **EcoTracker** é uma aplicação desenvolvida em **.NET 8** para monitoramento. Este guia detalha a arquitetura utilizada e como executar a aplicação em ambientes local e de nuvem.

---

## Execução Local com Docker

A aplicação é containerizada e orquestrada via **Docker Compose**, permitindo a execução rápida e isolada.

### Estratégias de Containerização

* **Configuração de Ambiente:** Utiliza a variável `ASPNET_ENVIRONMENT` para gerenciar diferentes configurações da aplicação.
* **Orquestração:** O Docker Compose permite subir os serviços individualmente ou em conjunto.

### 1. Comandos de Execução

Você pode subir os ambientes de *Staging* e *Production* simultaneamente ou individualmente:

| Objetivo | Comando |
| :--- | :--- |
| **Subir Todos os Containers** | `docker-compose up --build` |
| **Subir Apenas Staging** | `docker-compose up --build staging` |
| **Subir Apenas Production** | `docker-compose up --build production` |

### 2. Acesso à Documentação Local (Swagger)

Após a execução, acesse o **Swagger** para testar a API:

| Ambiente | Endereço (Local) |
| :--- | :--- |
| **Staging** | `http://localhost:8080/api/documentation` |
| **Production** | `http://localhost:8081/api/documentation` |

---

## Pipeline CI/CD (GitHub Actions)

O pipeline foi configurado com **GitHub Actions** para automatizar o processo de Integração e Entrega Contínua nos ambientes *Staging* e *Production*.

### CI (Integração Contínua: Build e Testes)

É executado a cada `push` para as branches **`staging`** e **`main`**.

**Etapas:**

1.  **Restaurar Pacotes:** Restauração de dependências do .NET.
2.  **Build da Aplicação:** Compilação do projeto.
3.  **Execução de Testes:** Execução dos testes automatizados (`dotnet test`).
4.  **Build & Push da Imagem:** Criação e envio da imagem Docker para o **GitHub Container Registry (GHCR)**.

### CD (Entrega Contínua: Deploy)

É executado automaticamente após a conclusão do pipeline de CI.
O Deploy é considerado concluído assim que a imagem é publicada no Registry, pois o Azure Web App está configurado para automaticamente buscar e atualizar a aplicação a partir dessa nova tag.
**Etapas:**

1.  **Deploy para Staging:**
    * **Ambiente:** `ecotracker-staging` (Azure Web App)
    * **Imagem:** Usa a tag **`:staging`**.
    * **Link:** [ecotracker-staging](ecotracker-staging-hcbmfqhafwgfdzd8.brazilsouth-01.azurewebsites.net/api/documentation)

2.  **Deploy para Production:**
    * **Ambiente:** `ecotracker-prod` (Azure Web App)
    * **Imagem:** Usa a tag **`:latest`**.
    * **Link:** [ecotracker-production](ecotracker.azurewebsites.net/api/documentation)

---

## Evidências do Projeto

| Descrição | Imagem |
| :--- | :--- |
| **Docker Compose em Execução** | ![Evidencia Docker Compose](https://i.imgur.com/wB8aMOD.png) |
| **Pipeline GitHub Actions** | ![Evidencia Github Actions](https://i.imgur.com/CxQnuiU.png) |


---

## Tecnologias Utilizadas

| Categoria | Tecnologia | Detalhes |
| :--- | :--- | :--- |
| **Linguagem/Framework** | **.NET 8, C#** | Utiliza **Entity Framework**. |
| **Banco de Dados** | **Oracle** | Banco de dados **remoto**. |
| **Containerização** | **Docker, Docker Compose** | Contêineres para a aplicação. |
| **CI/CD** | **GitHub Actions** | Automação dos pipelines. |
| **Container Registry** | **GitHub Container Registry (GHCR)** | Repositório das imagens Docker. |
| **Deploy** | **Azure Web App** | Ambientes de **Staging** e **Production**. |
| **Controle de Versão** | **Git / GitHub** | |
