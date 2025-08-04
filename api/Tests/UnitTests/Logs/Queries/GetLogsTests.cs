using Api.Modules.Authentication.Domain;
using Api.Modules.Clients.Domain;
using Api.Modules.Logs.Domain;
using Api.Modules.Logs.Presentation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;

namespace Api.Tests.UnitTests.Logs.Queries
{
    public class GetLogsTests
    {
        [Fact]
        public void Get_Logs_With_Valid_Data_Test()
        {
            //Arrange
            LogMockDependencies mockDependencies = new();
            LogController controller = mockDependencies.ServiceProvider.GetService<LogController>();
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
            Guid mockUserId = Guid.NewGuid();
            User userMock = new(
                id: mockUserId,
                name: "Maria Clara",
                email: "maria.clara@example.com",
                password: "senhamock",
                salt: "123"
            );

            mockDependencies.UsersMock.Add(userMock);
            mockDependencies.ClientsMock.Add(clientMock);

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

            Log newLog = new(id: Guid.NewGuid(), userId: mockUserId, clientId: clientMockId, action: "Create", timeStamp: DateTime.UtcNow);
            mockDependencies.LogsMock.Add(newLog);

            //Act
            var result = controller.GetAll(start: 0, increment: 10);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
