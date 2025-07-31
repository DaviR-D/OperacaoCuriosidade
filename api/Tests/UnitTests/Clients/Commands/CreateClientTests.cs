using Api.Modules.Clients.Presentation;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Api.Tests.UnitTests.Clients.Commands
{
    public class CreateClientTests()
    {
        [Fact]
        public void Create_Client_With_Valid_Data_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            ClientDto clientMock = new(
                name: "Maria Clara",
                email: "maria.clara@example.com",
                status: "Inactive",
                pending: true,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );

            //Act
            var result = controller.Create(clientMock);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Create_Client_With_Invalid_Email_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            ClientDto clientMock = new(
                name: "Maria Clara",
                email: "maria.claraexample.com",
                status: "Inactive",
                pending: true,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );

            //Act
            var result = controller.Create(clientMock);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        public void Create_Client_With_Invalid_Name_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            ClientDto clientMock = new(
                name: "Maria Clara1",
                email: "maria.clara@example.com",
                status: "Inactive",
                pending: true,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );

            //Act
            var result = controller.Create(clientMock);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        public void Create_Client_With_Existing_Email_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            mockDependencies.ClientsMock.Add(new(
                Guid.NewGuid(),
                name: "Fernando Lima",
                email: "fernando.lima@example.com",
                status: "Active",
                pending: false,
                date: DateTime.Now,
                age: 40,
                address: "Rua F, 101",
                other: "Gerente de projetos com experiência em tecnologia.",
                interests: "Tecnologia, Viagens",
                feelings: "Satisfeito",
                values: "Inovação, Colaboração"
            ));
            ClientDto clientMock = new(
                name: "Maria Clara",
                email: "fernando.lima@example.com",
                status: "Inactive",
                pending: true,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );

            //Act
            var result = controller.Create(clientMock);

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public void Create_Client_With_Empty_Field_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();

            ClientDto clientMock = new(
                name: "Maria Clara",
                email: "fernando.lima@example.com",
                status: "",
                pending: true,
                age: 28,
                address: "Rua das Flores, 123",
                other: "Artista plástica com exposições em galerias locais.",
                interests: "Artes, Pintura",
                feelings: "Reflexiva",
                values: "Autenticidade, Beleza"
            );

            //Act
            var result = controller.Create(clientMock);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }
    }
}
