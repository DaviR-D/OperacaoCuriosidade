using Api.Modules.Authentication.Domain;
using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Shared.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Api.Modules.Authentication.Application.Commands.CreateUser
{
    public class CreateUserHandler(UserRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (CreateUserCommand)input;
            UserValidator validator = new(command.User);
            if (!validator.ValidateUser()) return new CreateUserResponse("invalid data");
            var user = command.User;
            if (!VerifyAvailableEmail(user.Email)) return new CreateUserResponse("email already in use");

            string salt = Guid.NewGuid().ToString();
            string password = user.Password + salt;
            byte[] encodedPassword = Encoding.UTF8.GetBytes(password);
            byte[] passwordHash = SHA256.HashData(encodedPassword);

            User newUser = new(Guid.NewGuid(), user.Name, user.Email, Convert.ToBase64String(passwordHash), salt);
            repository.Create(newUser);

            return new CreateUserResponse();
        }
        public bool VerifyAvailableEmail(string email)
        {
            User? existingEmail = repository.GetAll().FirstOrDefault(user => user.Email == email);
            if (existingEmail != null) return false;
            return true;
        }
    }
}
