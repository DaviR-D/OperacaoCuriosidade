using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation;
using Microsoft.AspNetCore.Mvc;
using Test.UnitTests.Clients;
using Xunit;

namespace Test.UnitTests.Clients.Queries
{
    public class SearchClientsTests
    {
        [Fact]
        public void Search_Clients_With_Filled_List_Test()
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
            var result = controller.SearchClients(start: 0, increment: 10, query: "test");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Search_Clients_With_Empty_List_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();

            //Act
            var result = controller.SearchClients(start: 0, increment: 10, query: "test");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Search_Clients_Without_Query_Test()
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
            var result = controller.SearchClients(start: 0, increment: 10);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
