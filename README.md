# Desafio.Produtos.API
API de gerenciamento de produtos utilizando o .Net 8.

## Tecnologias utilizadas
- .Net Core 8
- Entity Framework Core 8
- Sql Server
- Containeirza��o de aplica��o com Docker

# Padr�es utilizados
- DDD
- Repository Pattern
- CQRS Pattern

## Arquitetura
A arquitetura utilizada � a de desenvolvimento em camadas, com foco no dom�nio da aplica��o, utilizando DDD, com as seguintes camadas abaixo:

### API
- Camada de exposi��o das rotas de API dispon�veis.

### Aplicacao
- Camada de aplica��o, contendo os casos de uso da aplica��o e liga��o com os servi�os do dom�nio.

### Dominio
- Camada de dom�nio e cora��o do sistema, contendo os casos de uso do sistemaas, entidades e interfaces de comunica��o com outras camadas.

### Infraestrutura
- Camada de infra que prov� recursos utilizados pelo sistema, tais como a comunica��o com o banco de dados.

### Teste Unitario
- Camada de de testes unit�rios das camadas de aplica��o e dom�nio.


## Primeiros passos

### Requisitos para rodar a aplica��o
Para rodar a aplica��o precisa dos seguintes requisitos instalados na m�quina
- SDK do .Net Core 8
- Docker

#### Rodando a aplica��o
Siga os passos abaixo para rodar a aplica��o com o Docker

##### Rodando a aplica��o via Docker
1. Abra um terminal, acesse a pasta raiz onde est� a  solution e rode o comando abaixo:
```
docker-compose up --build
```

> :exclamation:
> Observa��o: Aguarde at� que o banco de dados fique de p�. Ele demora um pouco utilizando a imagem SqlServer.
>

2. Acesse a url localmente
[http://localhost:8081/swagger/index.html](http://localhost:8081/swagger/index.html)

##### Rodando a aplica��o localmente
1. Para rodar localmente, voc� vai precisar ter instalado o Sql Server Express instalado
1. Crie um banco de dados com o Sql Server Express localmente

2. Ajuste a string de conex�o no arquivo appSetings.json e appSettings.Development.json com o banco criado
```
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ProdutosDb;User ID=sa;Password=@Password123;TrustServerCertificate=True"
  },
```

3. Inicie a aplica��o com o Visual Studio ou digite o comando abaixo na raiz do projeto
```
dotnet run --project "Produtos.API/Produtos.API.csproj"
```

4. Acesse a url
http://localhost:5105/swagger/index.html
[http://localhost:5105/swagger/index.html](http://localhost:5105/swagger/index.html)