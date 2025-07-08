namespace Api.Modules.Registrations;

public interface IRegistrationService
{
    void CreateRegistration(RegistrationDto registration);
    RegistrationDto GetSingleRegistration(string id);
    List<RegistrationDto> GetAllRegistrations();
    void UpdateRegistration(RegistrationDto registration);
    void DeleteRegistration(string id);

}