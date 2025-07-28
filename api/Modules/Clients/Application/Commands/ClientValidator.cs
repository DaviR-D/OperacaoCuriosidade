using Api.Modules.Clients.Presentation.ClientDTOs;

namespace Api.Modules.Clients.Application.Commands
{
    public class ClientValidator(ClientDto client)
    {
        private readonly ClientDto Client = client;
        public bool ValidateClient(ClientDto client)
        {
            var textFieldsValid =
                ValidateFieldSize(client.Name) &&
                ValidateFieldSize(client.Email) &&
                ValidateFieldSize(client.Status) &&
                ValidateFieldSize(client.Address) &&
                ValidateFieldSize(client.Other) &&
                ValidateFieldSize(client.Interests) &&
                ValidateFieldSize(client.Feelings) &&
                ValidateFieldSize(client.Values);

            var ageValid = client.Age > 18 && client.Age < 120;

            return textFieldsValid && ageValid;
        }
        public bool ValidateFieldSize(string field)
        {
            return (field.Length > 0 && field.Length < 300);
        }

    }
}
