using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared;

namespace Api.Modules.Clients.Application.Commands
{
    public class ClientValidator(ClientDto client) : ValidatorBase
    {
        private readonly ClientDto _client = client;
        public bool ValidateClient()
        {
            var textFieldsValid =
                ValidateFieldSize(_client.Name) &&
                ValidateFieldSize(_client.Email) &&
                ValidateFieldSize(_client.Status) &&
                ValidateFieldSize(_client.Address) &&
                ValidateFieldSize(_client.Other) &&
                ValidateFieldSize(_client.Interests) &&
                ValidateFieldSize(_client.Feelings) &&
                ValidateFieldSize(_client.Values);

            var ageValid = _client.Age >= 18 && _client.Age <= 120;

            var regexMatch = ValidateEmail(_client.Email) && ValidateName(_client.Name);

            return textFieldsValid && ageValid && regexMatch;
        }
    }
}
