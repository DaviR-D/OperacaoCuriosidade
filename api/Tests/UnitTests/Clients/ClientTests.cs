using Api.Modules.Clients.Presentation;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Xunit;

namespace Api.Tests.UnitTests.Clients
{
    public class ClientTests()
    {
        [Fact]
        public void Create_Client_With_Valid_Data_Test()
        {
            //Arrange
            ClientController controller = new();
            ClientDto clientMock = new(
                    name: "João Pedro",
                    email: "joao.pedro@example.com",
                    status: "Active",
                    pending: false,
                    age: 31,
                    address: "Avenida J, 505",
                    other: "Músico e compositor com várias apresentações.",
                    interests: "Música, Composição",
                    feelings: "Inspirado",
                    values: "Criatividade, Expressão"
                );

            //Act
            var result = controller.Create( clientMock );

            //Assert
            Assert.IsType<Ok>( result );
        }        
    }
}
