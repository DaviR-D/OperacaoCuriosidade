using Api.Modules.Authentication.Domain;
using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Shared.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Api.Modules.Authentication.Application.Commands.CreateUser
{
    public class CreateUserHandler(UserRepository repository) : IRequestHandler<Task<IRequestOutput?>, IRequestInput>
    {
        public async Task<IRequestOutput?> HandleAsync(IRequestInput input)
        {
            var command = (CreateUserCommand)input;
            UserValidator validator = new(command.User);

            if (!validator.ValidateUser())
                return new CreateUserResponse("invalid data");

            var user = command.User;

            if (!await VerifyAvailableEmail(user.Email))
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

            await repository.Create(newUser);

            return new CreateUserResponse();
        }
        public string HashPassword(string inputPassword, string salt)
        {
            string password = inputPassword + salt;
            byte[] encodedPassword = Encoding.UTF8.GetBytes(password);
            byte[] passwordHash = SHA256.HashData(encodedPassword);

            return Convert.ToBase64String(passwordHash);
        }
        public async Task<bool> VerifyAvailableEmail(string email)
        {
            List<User?> existingEmailTask = await repository.GetAll();
            var existingEmail = existingEmailTask.FirstOrDefault(user => user.Email == email);

            return existingEmail == null;
        }
    }
}
