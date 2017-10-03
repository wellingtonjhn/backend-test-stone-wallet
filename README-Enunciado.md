# Desafio do Cartão de Multi-Crédito

Você acabou de criar sua primeira conta no banco e ficou muito feliz por ter seu primeiro cartão de crédito, mas logo ficou desconfortável com o fato de o banco escolher quanto ele acha que você pode gastar nesse cartão por mês: o seu limite de crédito.
Logo você imaginou que poderia abrir contas em outros bancos para ter outros cartões de crédito e assim aumentar o seu limite de crédito.

Como você é uma pessoa que está sempre pensando em facilitar a sua vida, percebeu que essa quantidade de cartões na sua carteira era um desperdício de espaço.
Além de ser difícil de guardar todos, você percebeu que cada cartão tem uma data mensal para você pagar o que consumiu em crédito no último mês.
Por exemplo, um cartão tem que ser pago todo dia 03 do mês e outro todo dia 15.
Um detalhe importante é que você pode pagar o cartão antes da data de vencimento para liberar crédito.

Você prefere usar o cartão que está mais longe de vencer porque terá mais tempo para pagar a conta.
Caso os dois cartões vençam no mesmo dia, você prefere usar aquele que tem menor limite para continuar tendo um cartão com o limite mais alto.
Lembre-se que cada compra é feita em apenas um cartão, então manter um cartão com limite mais alto te dá liberdade de fazer compras grandes.
Somente no caso em que não for possível fazer a compra em um único cartão, o sistema deve dividir a compra em mais cartões.
Para isso, você vai preenchendo os cartões usando as mesmas ordens de prioridade já descritas.
Ou seja, você gasta primeiro do cartão que está mais longe de vencer e "completa" com o próximo cartão mais longe de vencer.
Caso os cartões vençam no mesmo dia, você gasta primeiro do com menor limite e "completa" com o que tem mais limite.

Comentando com seus amigos, você percebeu que não era apenas você que tinha essa necessidade.
Logo veio a ideia genial de criar uma empresa que concentra todos os cartões de crédito de uma pessoa e otimiza compras usando o melhor cartão disponível para essa compra.
Agora, a pessoa vai ter apenas um cartão (sua wallet) e não terá que se preocupar com esses detalhes no seu dia-a-dia.
Ela pode definir qual o limite de crédito que ela quer que sua wallet tenha, respeitando que não pode ser maior que a soma dos limites de todos os seus cartões de crédito.

O melhor cartão deverá ser escolhido com base na data de vencimento da fatura e limite disponível de cada cartão, nesta ordem de prioridade.

Você listou algumas features necessárias para que o produto comece a rodar:

## Gerenciar uma wallet

Wallet é a entidade que representa o seu produto. É nela que todos os cartões estarão armazenados.

### Premissas de uma wallet

* Uma wallet pode possuir vários cartões (motivo disso tudo)
* Uma wallet só pode pertencer a um user
* O limite máximo de uma wallet deve ser a soma de todos os cartões dela
* O usuário pode setar o limite real da wallet, desde que não ultrapasse o limite máximo
* Ter a capacidade de adicionar ou remover um cartão a qualquer momento
* O user deve ser capaz de acessar as informações de sua wallet a qualquer momento (limite setado pelo o user, limite máximo e crédito disponível)
* Executar a ação de uma compra de determinado valor de acordo com as prioridades citadas anteriormente

## Gerenciar um cartão de crédito

O cartão é a entidade que representa o cartão de crédito.

### Premissas de um cartão

* Possuir as propriedades necessárias para realizar uma compra (número, data de vencimento, data de validade, nome impresso, cvv e limite)
* Se capaz de liberar crédito (pagar determinado valor na conta do cartão)

# O que esperamos

A ideia é construir uma API com os endpoints necessários para que o seu produto saia do papel. É importante que seja um projeto completo, seguro, pronto para produção e de fácil manutenção.

Você tem a liberdade de escolher qualquer linguagem para desenvolver esse desafio, mas lembre-se de quanto mais popular ela seja, teremos mais capacidade de sermos justos na avaliação.

## O que avaliaremos

* Se o projeto foi feito por completo
* Se existe uma boa organização com a estutura, a documentação e o uso de controle de versão
* Se existe o uso de boas práticas de desenvolvimento, segurança e otimização
* Se existe uma preocupação com a qualidade sendo assegurada por testes automatizados

# O que não esperamos

Nosso foco está apenas na sua API, portanto não é necessária qualquer interface gráfica.

# Como submeter a solução

Você pode nos mandar um link do repositório no GitHub/BitBucket ou um arquivo ZIP com todo o código desenvolvido.
É muito importante tenha as instruções de execução.

Um plus seria o projeto ser hospedado em alguma plataforma como o Heroku.
Esperamos o link da aplicação neste caso.
