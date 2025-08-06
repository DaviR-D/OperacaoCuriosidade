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

            if (!validator.ValidateUser())
                return new CreateUserResponse("invalid data");

            var user = command.User;

            if (!VerifyAvailableEmail(user.Email))
                return new CreateUserResponse("email already in use");

            string salt = Guid.NewGuid().ToString();
            string passwordHash = HashPassword(user.Password, salt);

            User newUser = new(
                id: Guid.NewGuid(),
                name: user.Name,
                email: user.Email,
                password: passwordHash,
                salt: salt
                );

            repository.Create(newUser);

            return new CreateUserResponse();
        }
        public string HashPassword(string inputPassword, string salt)
        {
            string password = inputPassword + salt;
            byte[] encodedPassword = Encoding.UTF8.GetBytes(password);
            byte[] passwordHash = SHA256.HashData(encodedPassword);

            return Convert.ToBase64String(passwordHash);
        }
        public bool VerifyAvailableEmail(string email)
        {
            User? existingEmail = repository.GetAll().FirstOrDefault(user => user.Email == email);

            return existingEmail == null;
        }
    }
}
