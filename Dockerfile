# Usa a imagem oficial do SDK do .NET Core como imagem base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho dentro do contêiner para /app
WORKDIR /app

# Copia o arquivo do projeto (.csproj) para o contêiner e restaura as dependências
#COPY *.csproj ./
COPY ["Produto.Dominio/Dominio.csproj", "Produto.Dominio/"]
COPY ["Infraestrutura/Infraestrutura.csproj", "Infraestrutura/"]
COPY ["Aplicacao/Aplicacao.csproj", "Aplicacao/"]
COPY ["Produtos.API/Produtos.API.csproj", "Produtos.API/"]

RUN dotnet restore "Produtos.API/Produtos.API.csproj"

# Copia o resto do código da aplicação para o contêiner
COPY . .

# Compila a aplicação com a configuração de Release e publica os artefatos no diretório 'out'
RUN dotnet publish Produtos.API/Produtos.API.csproj -c Release -o dist

# Inicia a construção da imagem de tempo de execução utilizando a imagem do ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho para /app na imagem de tempo de execução
WORKDIR /app

# Define a variável de ambiente ASPNETCORE_URLS para que a aplicação aceite tráfego na porta 80 de todas as interfaces de rede
ENV ASPNETCORE_URLS=http://+:8081

# Copia os artefatos publicados da etapa de construção para o diretório de trabalho na imagem de tempo de execução
COPY --from=build /app/dist ./

# Informa ao Docker que a aplicação dentro do contêiner estará escutando na porta 80
EXPOSE 8081

# Define o comando que será executado quando o contêiner iniciar, que nesse caso, inicia a aplicação .NET
ENTRYPOINT ["dotnet", "Produtos.API.dll"]