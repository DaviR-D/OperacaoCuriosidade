using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;

namespace Test.UnitTests.Clients.Queries
{
    public class GetSingleClientTests
    {
        [Fact]
        public void Get_Single_Existing_Client_Test()
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

            var mockUserId = Guid.NewGuid();
            var mockClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, mockUserId.ToString())
            };
            var mockIdentity = new ClaimsIdentity(mockClaims, "MockAuth");
            var mockUser = new ClaimsPrincipal(mockIdentity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = mockUser
                }
            };

            //Act
            var result = controller.GetSingle(clientMockId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_Single_Deleted_Client_Test()
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
            clientMock.Deleted = true;
            mockDependencies.ClientsMock.Add(clientMock);

            var mockUserId = Guid.NewGuid();
            var mockClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, mockUserId.ToString())
            };
            var mockIdentity = new ClaimsIdentity(mockClaims, "MockAuth");
            var mockUser = new ClaimsPrincipal(mockIdentity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = mockUser
                }
            };

            //Act
            var result = controller.GetSingle(clientMockId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Get_Single_Inexistent_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            Guid clientMockId = Guid.NewGuid();

            var mockUserId = Guid.NewGuid();
            var mockClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, mockUserId.ToString())
            };
            var mockIdentity = new ClaimsIdentity(mockClaims, "MockAuth");
            var mockUser = new ClaimsPrincipal(mockIdentity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = mockUser
                }
            };

            //Act
            var result = controller.GetSingle(clientMockId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
