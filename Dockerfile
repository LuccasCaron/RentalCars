# Use a imagem do SDK do .NET para compilar o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copie o arquivo de solução
COPY RentalCars.sln ./

# Certifique-se de que os diretórios de cada projeto existem
RUN mkdir -p src/RentalCars.API src/RentalCars.Application src/RentalCars.Domain src/RentalCars.Infra.Data src/RentalCars.Infra.IoC tests/RentalCars.Domain.Tests

# Copie os arquivos de projeto de cada projeto
COPY src/RentalCars.API/*.csproj ./src/RentalCars.API/
COPY src/RentalCars.Application/*.csproj ./src/RentalCars.Application/
COPY src/RentalCars.Domain/*.csproj ./src/RentalCars.Domain/
COPY src/RentalCars.Infra.Data/*.csproj ./src/RentalCars.Infra.Data/
COPY src/RentalCars.Infra.IoC/*.csproj ./src/RentalCars.Infra.IoC/
COPY tests/RentalCars.Domain.Tests/*.csproj ./tests/RentalCars.Domain.Tests/

# Listar arquivos após cópia dos arquivos de projeto
RUN echo "Arquivos copiados:" && ls -R /app

# Restaure as dependências
RUN dotnet restore RentalCars.sln

# Listar arquivos após a restauração
RUN echo "Arquivos após restore:" && ls -R /app

# Copie todos os arquivos restantes de cada projeto
COPY ./src ./src
COPY ./tests ./tests

# Publique o projeto da API
RUN dotnet publish src/RentalCars.API/RentalCars.API.csproj -c Release -o out

# Use a imagem do ASP.NET para o runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /runtime-app

# Copie os arquivos publicados da etapa de build
COPY --from=build-env /app/out ./ 

# Exponha a porta que o aplicativo escutará
EXPOSE 8080 

# Defina o ponto de entrada do contêiner
ENTRYPOINT ["dotnet", "RentalCars.API.dll"]
