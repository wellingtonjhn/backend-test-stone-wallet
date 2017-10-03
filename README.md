# Wallet

[![build-status](https://pipelines-badges-service.useast.staging.atlassian.io/badge/atlassian/confluence-web-components.svg)](https://bitbucket.org/wellingtonjhn/stone-wallet/addon/pipelines/home) 

Resolução do Desafio do Cartão de Multi-Crédito para a vaga de backend developer na Stone Pagamentos

## Sobre o projeto

Essa api foi criada utilizando as seguintes tecnologias:

* Microsoft ASP.NET Core 2.0
* Padrão Mediator com a bibilioteca [MediatR](https://github.com/jbogard/MediatR)
* Validação de comandos (fail-fast) via middleware usando a biblioteca [Fluent Validation](https://github.com/JeremySkinner/FluentValidation)


## Como rodar esse projeto localmente

Primeiramente será necessário instalar o [.Net Core 2.0 SDK](https://www.microsoft.com/net/download/core), em seguida use os seguintes comandos para clonar e executar o projeto:

```
git clone https://wellingtonjhn@bitbucket.org/wellingtonjhn/stone-wallet.git
cd stone-wallet\StoneWallet
dotnet build
dotnet run --project .\StoneWallet.Api\StoneWallet.Api.csproj
```

