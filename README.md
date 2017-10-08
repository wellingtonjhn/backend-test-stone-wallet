# Stone Wallet

[![build-status](https://pipelines-badges-service.useast.staging.atlassian.io/badge/atlassian/confluence-web-components.svg)](https://bitbucket.org/wellingtonjhn/stone-wallet/addon/pipelines/home) 

Possível solução para o Desafio do Cartão de Multi-Crédito para a vaga de backend developer na Stone Pagamentos

## Sobre o projeto

Essa api foi criada utilizando as seguintes tecnologias:

* Microsoft ASP.NET Core 2.0
* Padrão Mediator com a bibilioteca [MediatR](https://github.com/jbogard/MediatR)
* Validação de comandos (fail-fast) via middleware usando a biblioteca [Fluent Validation](https://github.com/JeremySkinner/FluentValidation)
* Log com a biblioteca [Log4Net](https://github.com/apache/logging-log4net/)
* Visualização dos logs da aplicação no [PaperTrail](https://papertrailapp.com) 
* Autenticação de usuário baseada em JWT [Json Web Token](https://tools.ietf.org/html/rfc7519)
* Banco de dados PostgreSQL no Heroku [Heroku Data](https://www.heroku.com/postgres)

## Como rodar esse projeto localmente

Primeiramente será necessário instalar o [.Net Core 2.0 SDK](https://www.microsoft.com/net/download/core), em seguida use os seguintes comandos no terminal para clonar e executar o projeto:

```
git clone https://wellingtonjhn@bitbucket.org/wellingtonjhn/stone-wallet.git
cd stone-wallet\StoneWallet
dotnet build
dotnet run --project .\StoneWallet.Api\StoneWallet.Api.csproj
```

