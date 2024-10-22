
# Ursulla Game Project ##

## Project Status
![Issues](https://img.shields.io/github/issues/celinhodaltro/Server-Online)
![Build Status](https://github.com/celinhodaltro/Server-Online/actions/workflows/dotnet-desktop.yml/badge.svg)


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
│   ├── /Server.Bussiness           # Regras de negócios
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
    ├── Github.Documents  # Documentos do Git
    │    └── ReadMe.md    # Documentação do projeto
    │
    └── Drawio.Diagrams 
            └── diagram.png    # Diagrama do projeto (Fluxo)
```


## Tecnologias

- **Backend**: ASP.NET Core, Entity Framework Core
- **Frontend**: Blazor, MudBlazor
- **Database**: MySql
- **Game Client**: Unity
- **Documentation**: Draw.io, Markdown

## Getting Started

### Requisitos


Libs:
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/en/) (opcional, para ferramentas de desenvolvimento da WebApp)

Ferramentas de desenvolvimento
- [visual Studio 2022](https://visualstudio.microsoft.com/vs/community/) (Ideal para um desenvolvimento mais funcional) 
- [Visual Studio Code](https://code.visualstudio.com/) (Eu prefiro o Visual Studio 2022, porem essa é uma otima opção tambem!)


### Rodando o projeto:

#### 1. Clonar o Repositório

```bash
git clone https://github.com/celinhodaltro/Server-Online.git
cd Server
```

#### 2. Buildando projeto

Navegue até o diretório do servidor e instale as dependências necessárias:

```bash
cd Main
dotnet restore
dotnet run
```

- Para um desenvolvimeto mais rapido e funcional eu recomendo setar 3 projetos como projetos de inicialização: Server.Start | Server.API | Application.WebApp

#### 3. Executar o Cliente de Jogo

- O client Unity (Application.Client) requer uma build própria.
- Se preferir o cliente padrão do tibia tambem funciona, porem recomendo que consiga a source do mesmo para ter um desenvolvimento sem nenhum limite.


##### Links para o build do cliente:
- [Unity](https://slavi.gitbook.io/opentibiaunity/getting-started/running-the-game) - C#, Eu escolhi essa opção por ter como objetivo principal um servidor 100% em c# para facilitar o desenvolvimento
- [Otcv8](https://github.com/OTCv8/otclientv8) - C++
- [Edubart](https://github.com/edubart/otclient) - C++ 


## API Documentation
A API oferece endpoints para gerenciar entidades como `Character` e `User`. Para acessar a documentação completa e exemplos de requisições, utilize o Swagger disponível na URL `/swagger` após o servidor estar em execução.

## Contributing
Contribuições são bem-vindas! Para começar:

1. Faça um Fork do projeto.
2. Crie sua Feature Branch (`git checkout -b feature/nova-funcionalidade`).
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`).
4. Envie o Push para o Branch (`git push origin feature/nova-funcionalidade`).
5. Crie um Pull Request.

## Créditos

* @Caioavidal: Dou uma grande parte do crédito como base do projeto a este projeto: https://github.com/caioavidal/OpenCoreMMO* (Juro que conforme eu for avançando com este projeto irei me dedicar a resolver issues la no projeto do Caio!)
* @Slavi: O cliente Unity consegue tirar os limites do que a gente consegue fazer visualmente em um projeto de tibia, e sair dessa caixinha. *
---

*Projetado com ❤️ por [João Marcelo Daltro Marques](https://github.com/celinhodaltro).*

---

