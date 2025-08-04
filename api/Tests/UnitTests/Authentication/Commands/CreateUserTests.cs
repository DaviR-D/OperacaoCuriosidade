
using Api.Modules.Authentication.Presentation;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Api.Tests.UnitTests.Authentication.Commands
{
    public class CreateUserTests
    {
        [Fact]
        public void Create_User_With_Valid_Data_Test()
        {
            //Arrange
            AuthenticationMockDependencies mockDependencies = new();
            AuthenticationController controller = mockDependencies.ServiceProvider.GetService<AuthenticationController>();
            UserDto userMock = new(
                name: "Maria Clara",
                email: "maria.clara@example.com",
                password: "senhamock"
            );

            //Act
            var result = controller.Create(userMock);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Create_User_With_Invalid_Email_Test()
        {
            //Arrange
            AuthenticationMockDependencies mockDependencies = new();
            AuthenticationController controller = mockDependencies.ServiceProvider.GetService<AuthenticationController>();
            UserDto userMock = new(
                name: "Maria Clara",
                email: "maria.claraexample.com",
                password: "senhamock"
            );

            //Act
            var result = controller.Create(userMock);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        public void Create_User_With_Existing_Email_Test()
        {
            //Arrange
            AuthenticationMockDependencies mockDependencies = new();
            AuthenticationController controller = mockDependencies.ServiceProvider.GetService<AuthenticationController>();
            mockDependencies.UsersMock.Add(new(
                id: Guid.NewGuid(),
                name: "Maria Clara",
                email: "maria.clara@example.com",
                password: "senhamock",
                salt: "123"
            ));

            UserDto userMock = new(
                name: "Maria Clara",
                email: "maria.clara@example.com",
                password: "senhamock"
            );

            //Act
            var result = controller.Create(userMock);

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public void Create_User_With_Empty_Password_Test()
        {
            //Arrange
            AuthenticationMockDependencies mockDependencies = new();
            AuthenticationController controller = mockDependencies.ServiceProvider.GetService<AuthenticationController>();
            UserDto userMock = new(
                name: "Maria Clara",
                email: "maria.claraexample.com",
                password: ""
            );

            //Act
            var result = controller.Create(userMock);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }
    }
}
