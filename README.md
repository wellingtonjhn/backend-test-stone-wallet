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
* Autenticação de usuário baseada em token JWT [Json Web Token](https://tools.ietf.org/html/rfc7519)
* Banco de dados SQL com [Heroku Postgres](https://www.heroku.com/postgres)
* Documentação da API gerada com o [Swagger](https://swagger.io/)


## Pontos de melhoria
* Corrigir bug ao iniciar a aplicação com **dot net run** via terminal, onde as configurações não são carregadas;
* Remover lançamento de exceptions do dominio e substituir por [Domain Notifications](https://martinfowler.com/eaaDev/Notification.html).

## Como rodar esse projeto em seu computador

Primeiramente será necessário instalar o [.Net Core 2.0 SDK](https://www.microsoft.com/net/download/core), em seguida use os seguintes comandos no terminal para clonar e executar o projeto:

```
git clone https://bitbucket.org/wellingtonjhn/stone-wallet
```

> Abra a Solution no Visual Studio, marque o projeto **StoneWallet.Api.csproj** como default e pressione a tecla F5 para executar a aplicação.

## Documentação

Esse projeto conta com uma documentação dos endpoints da API feita através do [Swagger](https://swagger.io/).
Após rodar a aplicação basta acessar a url "http://localhost:{port}/docs/", onde {port} deverá ser substituído pela porta em que a aplicação está rodando em seu computador.

## Endpoints da API

Os exemplos abaixo utilizam a ferramenta de linha de comando [CURL](https://curl.haxx.se/), mas você também pode utilizar o [Postman](https://www.getpostman.com/) para realizar as chamadas na api. Neste caso, basta importar para o Postman o arquivo [Stone Wallet.postman_collection.json](https://bitbucket.org/wellingtonjhn/stone-wallet/src) que está na raiz do repositório.

#### Registrar um novo usuário
```
curl -X POST http://localhost:51177/api/accounts/register \
  -H 'content-type: application/json' \
  -d '{
	"name" : "Wellington Nascimento",
	"email" : "wellington@email.com",
	"password" : "pass"
}'
```

#### Autenticar usuário
```
curl -X POST http://localhost:51177/api/accounts/login \
  -H 'content-type: application/json' \
  -d '{
	"email" : "wellington@email.com",
	"password" : "pass"
}'
```

#### Consultar informações do usuário (requer usuário logado)
```
curl -X GET http://localhost:51177/api/accounts/profile \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9 ...'
```

#### Alterar a senha do usuário logado (requer usuário logado)
```
curl -X PUT http://localhost:51177/api/accounts/profile/password \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9 ...' \
  -H 'content-type: application/json' \
  -d '{	"newPassword" : "new pass",	"newPasswordConfirmation" : "new pass" }'
```

#### Consultar informações da Wallet (requer usuário logado)
```
curl -X GET http://localhost:51177/api/wallets \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9 ...'
```

#### Adicionar Cartão de Crédito (requer usuário logado)
```
curl -X POST http://localhost:51177/api/wallets/creditcards \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9 ...' \
  -H 'content-type: application/json' \
  -d '{
	"number" : 1234567890987654,
	"dueDate" : "2017-10-15",
	"expirationDate" : "2022-03-12",
	"printedName" : "Wellington Nascimento",
	"cvv" : 567,
	"creditLimit" : 1500
}'
```

#### Alterar limite da Wallet (requer usuário logado)
```
curl -X PUT http://localhost:51177/api/wallets/limit \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9 ...' \
  -H 'content-type: application/json' \
  -d '{ "limit" : 1000 }'
```

#### Remover Cartão de Crédito (requer usuário logado)
```
curl -X DELETE http://localhost:51177/api/wallets/creditcards \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9 ...' \
  -H 'content-type: application/json' \
  -d '{ "creditCardId" : "77898ef0-152c-473d-aed2-0269ec910363" }'
```

#### Realizar uma Comprar através da Wallet (requer usuário logado)
```
curl -X POST http://localhost:51177/api/wallets/purchase \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9 ...' \
  -H 'content-type: application/json' \
  -d '{ "amount": 800 }'
```

#### Pagar um Cartão de Crédito (requer usuário logado)
```
curl -X POST \
  http://localhost:51177/api/wallets/creditcards/payment \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9 ...' \
  -H 'content-type: application/json' \
  -d '{	"cardId" : "379d2521-bffa-4941-8669-4f18f0e63777", "amount" : 500 }'
```
