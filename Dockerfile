FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY RentalCars.sln ./

RUN mkdir -p src/RentalCars.API src/RentalCars.Application src/RentalCars.Domain src/RentalCars.Infra.Data src/RentalCars.Infra.IoC tests/RentalCars.Domain.Tests

COPY src/RentalCars.API/*.csproj ./src/RentalCars.API/
COPY src/RentalCars.Application/*.csproj ./src/RentalCars.Application/
COPY src/RentalCars.Domain/*.csproj ./src/RentalCars.Domain/
COPY src/RentalCars.Infra.Data/*.csproj ./src/RentalCars.Infra.Data/
COPY src/RentalCars.Infra.IoC/*.csproj ./src/RentalCars.Infra.IoC/
COPY tests/RentalCars.Domain.Tests/*.csproj ./tests/RentalCars.Domain.Tests/

RUN dotnet restore RentalCars.sln

RUN echo "Arquivos após restore:" && ls -R /app

COPY ./src ./src
COPY ./tests ./tests

RUN dotnet publish src/RentalCars.API/RentalCars.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /runtime-app

COPY --from=build-env /app/out ./ 

EXPOSE 8080 

ENTRYPOINT ["dotnet", "RentalCars.API.dll"]
