using Api.Modules.Authentication.Presentation;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Api.Tests.UnitTests.Authentication.Commands
{
    public class AuthenticateTests
    {
        [Fact]
        public void Authenticate_User_With_Correct_Credentials_Test()
        {
            //Arrange
            AuthenticationMockDependencies mockDependencies = new();
            AuthenticationController controller = mockDependencies.ServiceProvider.GetService<AuthenticationController>();
            UserDto userMock = new(
                name: "Maria Clara",
                email: "maria.clara@example.com",
                password: "senhamock"
            );

            UserDto loginCredentials = new(
                email: "maria.clara@example.com",
                password: "senhamock"
            );

            controller.Create(userMock);

            //Act
            var result = controller.Authenticate(loginCredentials);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Authenticate_User_With_Incorrect_Email_Test()
        {
            //Arrange
            AuthenticationMockDependencies mockDependencies = new();
            AuthenticationController controller = mockDependencies.ServiceProvider.GetService<AuthenticationController>();
            UserDto userMock = new(
                name: "Maria Clara",
                email: "maria.clara@example.com",
                password: "senhamock"
            );

            UserDto loginCredentials = new(
                email: "maria@example.com",
                password: "senhamock"
            );

            controller.Create(userMock);

            //Act
            var result = controller.Authenticate(loginCredentials);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public void Authenticate_User_With_Incorrect_Password_Test()
        {
            //Arrange
            AuthenticationMockDependencies mockDependencies = new();
            AuthenticationController controller = mockDependencies.ServiceProvider.GetService<AuthenticationController>();
            UserDto userMock = new(
                name: "Maria Clara",
                email: "maria.clara@example.com",
                password: "senhamock"
            );

            UserDto loginCredentials = new(
                email: "maria.clara@example.com",
                password: "senhamock2"
            );

            controller.Create(userMock);

            //Act
            var result = controller.Authenticate(loginCredentials);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}
