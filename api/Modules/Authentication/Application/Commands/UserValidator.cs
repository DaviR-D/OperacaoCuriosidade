using Api.Modules.Authentication.Presentation.UserDTOs;
using Api.Shared;

namespace Api.Modules.Authentication.Application.Commands
{
    public class UserValidator(UserDto user) : ValidatorBase
    {
        private readonly UserDto _user = user;

        public bool ValidateUser()
        {
            var textFieldsValid =
               ValidateFieldSize(_user.Name) &&
               ValidateFieldSize(_user.Email) &&
               ValidateFieldSize(_user.Password);

            var regexMatch = ValidateEmail(_user.Email) && ValidateName(_user.Name);

            return textFieldsValid && regexMatch;
        }
    }
}
