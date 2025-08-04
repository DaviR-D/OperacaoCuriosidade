using Api.Modules.Authentication.Domain;
using Api.Modules.Clients.Domain;
using Api.Modules.Logs.Presentation;
using Api.Modules.Logs.Presentation.LogDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;

namespace Api.Tests.UnitTests.Logs.Commands
{
    public class CreateLog
    {
        [Fact]
        public void Create_Log_With_Valid_Data_Test()
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
                salt:"123"
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

            CreateLogDto newLog = new(id: Guid.NewGuid(), userId: mockUserId, clientId: clientMockId, action: "Create", timeStamp: DateTime.UtcNow);

            //Act
            var result = controller.Create(newLog);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
