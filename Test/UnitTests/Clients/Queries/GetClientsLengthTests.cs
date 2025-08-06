using Api.Modules.Clients.Application.Queries.GetClientsLength;
using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Test.UnitTests.Clients.Queries
{
    public class GetClientsLengthTests
    {
        [Fact]
        public void Get_Clients_Length_Test()
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
            var result = controller.GetLength();
            var castResult = (OkObjectResult)result;
            var response = (GetClientsLengthResponse)castResult.Value;

            //Assert
            Assert.Equal(1, response.Length);
        }
    }
}
