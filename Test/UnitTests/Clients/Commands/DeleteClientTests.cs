using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;

namespace Test.UnitTests.Clients.Commands
{
    public class DeleteClientTests
    {
        [Fact]
        public void Delete_Existing_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
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
            var result = controller.Delete(clientMockId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Delete_Locked_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
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
            clientMock.EditLock = DateTime.UtcNow.AddMinutes(10);
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
            var result = controller.Delete(clientMockId);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public void Delete_Already_Deleted_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
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
            var result = controller.Delete(clientMockId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Delete_Inexistent_Client_Test()
        {
            //Arrange
            ClientMockDependencies mockDependencies = new ClientMockDependencies();
            ClientController controller = mockDependencies.ServiceProvider.GetService<ClientController>();
            Guid inexintentClientMockId = Guid.NewGuid();

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
            var result = controller.Delete(inexintentClientMockId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
