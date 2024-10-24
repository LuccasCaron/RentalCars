# Serviço de Aluguel de Carros

## Descrição
Este projeto desenvolve o back-end de um serviço para gerenciar o aluguel de carros. O serviço utiliza **.NET 8** e é projetado com uma arquitetura em camadas, que inclui as camadas API, Application, Domain, Infra.Data, Infra.IoC, Infra.Email e Messaging. Ele incorpora Identity para autenticação e autorização de usuários. O sistema segue princípios como Domain-Driven Design (DDD) e SOLID, garantindo uma boa estrutura. O banco de dados é gerenciado utilizando PostgreSQL.

Além das funcionalidades de aluguel de carros, o sistema dispara **eventos** através do RabbitMQ para processar ações assíncronas, como notificações por e-mail, implementadas na camada Infra.Email. A camada Messaging cuida da integração com RabbitMQ, contendo os consumidores e a lógica para lidar com eventos, como a criação de aluguel de carro.

Testes automatizados estão incluídos para garantir a confiabilidade da aplicação.

## Tecnologias Utilizadas
**.NET 8**: Framework de desenvolvimento para construir a aplicação.

**PostgreSQL**: Sistema de gerenciamento de banco de dados relacional.

**Identity**: Sistema de autenticação e autorização.

**Entity Framework Core**: ORM para acesso a dados.

**Fluent Validation**: Biblioteca de validação para garantir a integridade dos dados.

**RabbitMQ**: Sistema de mensageria para processamento de eventos.

**Docker e Docker Compose**: Para orquestrar os serviços e facilitar o deploy local.

**XUnit**: Framework para testes automatizados.

## Utilização
Este projeto utiliza Docker e Docker Compose para orquestrar os serviços, incluindo PostgreSQL, RabbitMQ e a API.

#Passos para utilizar o projeto:
**1** - Clone o repositório:
   ```bash
   git clone <URL-do-repositório>
   ```
**2** - Valide as variáveis de ambiente
    no arquivo appsettings.json para garantir que os dados de envio de e-mail estejam corretos.
    
**3** - Navegue até o diretório raiz do projeto


**4** - Suba os containers com Docker Compose:
   ```bash
   docker-compose up -d


