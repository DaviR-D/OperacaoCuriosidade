using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation;
using Microsoft.AspNetCore.Mvc;
using Test.UnitTests.Clients;
using Xunit;

namespace Test.UnitTests.Clients.Queries
{
    public class VerifyAvailableEmailTests
    {
        [Fact]
        public void Verify_Available_Email_When_Positive_For_New_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            Guid clientMockId = Guid.NewGuid();
            Client clientMock = new(
                id: clientMockId,
                name: "Maria Clara",
                email: "maria.clara@example.com",
                status: "Inactive",
                pending: true,
                date: DateTime.Now,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );
            mockDependencies.ClientsMock.Add(clientMock);

            //Act
            var result = controller.CheckEmail("joao@example.com");
            var castResult = (OkObjectResult)result;
            var response = (VerifyAvailableEmailResponse)castResult.Value;

            //Assert
            Assert.True(response.IsAvailable);
        }

        [Fact]
        public void Verify_Available_Email_When_Positive_For_Existing_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            Guid clientMockId = Guid.NewGuid();
            Client clientMock = new(
                id: clientMockId,
                name: "Maria Clara",
                email: "maria.clara@example.com",
                status: "Inactive",
                pending: true,
                date: DateTime.Now,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );
            mockDependencies.ClientsMock.Add(clientMock);

            //Act
            var result = controller.CheckEmail("joao@example.com", Guid.NewGuid());
            var castResult = (OkObjectResult)result;
            var response = (VerifyAvailableEmailResponse)castResult.Value;

            //Assert
            Assert.True(response.IsAvailable);
        }

        [Fact]
        public void Verify_Available_Email_When_Positive_For_Same_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            Guid clientMockId = Guid.NewGuid();
            Client clientMock = new(
                id: clientMockId,
                name: "Maria Clara",
                email: "maria.clara@example.com",
                status: "Inactive",
                pending: true,
                date: DateTime.Now,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );
            mockDependencies.ClientsMock.Add(clientMock);

            //Act
            var result = controller.CheckEmail("maria.clara@example.com", clientMockId);
            var castResult = (OkObjectResult)result;
            var response = (VerifyAvailableEmailResponse)castResult.Value;

            //Assert
            Assert.True(response.IsAvailable);
        }

        [Fact]
        public void Verify_Available_Email_When_Negative_For_New_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            Guid clientMockId = Guid.NewGuid();
            Client clientMock = new(
                id: clientMockId,
                name: "Maria Clara",
                email: "maria.clara@example.com",
                status: "Inactive",
                pending: true,
                date: DateTime.Now,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );
            mockDependencies.ClientsMock.Add(clientMock);

            //Act
            var result = controller.CheckEmail("maria.clara@example.com");
            var castResult = (OkObjectResult)result;
            var response = (VerifyAvailableEmailResponse)castResult.Value;

            //Assert
            Assert.False(response.IsAvailable);
        }

        [Fact]
        public void Verify_Available_Email_When_Negative_For_Existing_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            Guid clientMockId = Guid.NewGuid();
            Client clientMock = new(
                id: clientMockId,
                name: "Maria Clara",
                email: "maria.clara@example.com",
                status: "Inactive",
                pending: true,
                date: DateTime.Now,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );
            mockDependencies.ClientsMock.Add(clientMock);

            //Act
            var result = controller.CheckEmail("maria.clara@example.com", Guid.NewGuid());
            var castResult = (OkObjectResult)result;
            var response = (VerifyAvailableEmailResponse)castResult.Value;

            //Assert
            Assert.False(response.IsAvailable);
        }
    }
}
