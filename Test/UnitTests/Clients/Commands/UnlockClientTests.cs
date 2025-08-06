using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;

namespace Test.UnitTests.Clients.Commands
{
    public class UnlockClientTests
    {
        [Fact]
        public void Unlock_Client_With_Valid_Token_Test()
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
            var expireTimeMock = DateTime.UtcNow.AddMinutes(10);
            clientMock.Lock = expireTimeMock;
            mockDependencies.ClientsMock.Add(clientMock);


            var mockUserId = Guid.NewGuid();
            var mockClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, mockUserId.ToString()),
                new(ClaimTypes.Expiration, expireTimeMock.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                new("ClientId", clientMockId.ToString())
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
            var result = controller.Unlock();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Unlock_Client_With_Valid_Token_But_Invalid_Date_Test()
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
            var expireTimeMock = DateTime.UtcNow.AddMinutes(10);
            clientMock.Lock = expireTimeMock;
            mockDependencies.ClientsMock.Add(clientMock);


            var mockUserId = Guid.NewGuid();
            var mockClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, mockUserId.ToString()),
                new(ClaimTypes.Expiration, expireTimeMock.AddMilliseconds(-1).ToString()),
                new("ClientId", clientMockId.ToString())
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
            var result = controller.Unlock();

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}
