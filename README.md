
# Ursulla Game Project ##

## Project Status
![Issues](https://img.shields.io/github/issues/celinhodaltro/Server-Online)
![Build Status](https://github.com/celinhodaltro/Server-Online/actions/workflows/dotnet-desktop.yml/badge.svg)




## Conteudos

- [Overview](#overview)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Overview

**Ursulla** é um projeto de jogo e aplicação web que visa fornecer uma experiência integrada entre uma aplicação de jogo nativa e uma interface web moderna. O sistema foi projetado com uma arquitetura modular para garantir escalabilidade, facilidade de manutenção e suporte a novas funcionalidades.

## Estrutura

A estrutura do projeto é dividida em três principais módulos:

- **Server**: Responsável pela lógica de negócios, API e manipulação de dados.
- **Clients**: Contém as aplicações que interagem com os usuários, tanto o jogo quanto a aplicação web.
- **Docs**: Documentação técnica e diagramas de arquitetura.

### Arquitetura

```bash
/Ursulla
│
├── /Server
│   ├── /Server.API                 # Endpoints da API
│   ├── /Server.Entities            # Entidades do domínio (Character, Game, User)
│   ├── /Server.Bussiness            # Regras de negócios
│   ├── /Server.Util                # Utilitários comuns do servidor
│   └── /Server.Provider            # Acesso a dados e lógica de persistência
│
├── /Application
│   ├── /Application.Services   # Código fonte para requisição com o back (Pensar em alterar nome para Application.Request)
│   ├── /Application.GameClient # Client do jogo
│   └── /Application.WebApp     # Aplicação web Blazor
│       ├── /Pages              # Páginas da aplicação Blazor
│       └── /Shared             # Componentes compartilhados da aplicação Blazor
│
└── /Docs
    ├── ArchitectureDiagram.drawio  # Diagrama da arquitetura do projeto
    └── ReadMe.md                   # Documentação do projeto
```


## Tecnologias

- **Backend**: ASP.NET Core, Entity Framework Core
- **Frontend**: Blazor, MudBlazor
- **Database**: MySql
- **Game Client**: Unity
- **Documentation**: Draw.io, Markdown

## Getting Started

### Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/en/) (opcional, para ferramentas de desenvolvimento da WebApp)

### Rodando o projeto:

#### 1. Clonar o Repositório

```bash
git clone https://github.com/yourusername/ursulla.git
cd Ursulla
```

#### 2. Configurar o Servidor

Navegue até o diretório do servidor e instale as dependências necessárias:

```bash
cd Server
dotnet restore
dotnet run
```

#### 3. Executar o Cliente de Jogo

No diretório `GameApp`, compile e execute a aplicação:


#### 4. Executar a Aplicação Web

Navegue até o diretório da `WebApp` e execute a aplicação:

```bash
cd Clients/WebApp
dotnet run
```

## API Documentation

A API oferece endpoints para gerenciar entidades como `Character` e `User`. Para acessar a documentação completa e exemplos de requisições, utilize o Swagger disponível na URL `/swagger` após o servidor estar em execução.

## Contributing

Contribuições são bem-vindas! Para começar:

1. Faça um Fork do projeto.
2. Crie sua Feature Branch (`git checkout -b feature/nova-funcionalidade`).
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`).
4. Envie o Push para o Branch (`git push origin feature/nova-funcionalidade`).
5. Crie um Pull Request.

## License

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para mais detalhes.\
*Dou total credito como base do projeto a este projeto do @caioavidal: https://github.com/caioavidal/OpenCoreMMO* (Juro que conforme eu for avançando com este projeto irei me dedicar a resolver issues la no projeto do Caio!)

---

*Projetado com ❤️ por [João Marcelo Daltro Marques](https://github.com/celinhodaltro).*

---

