using System.Globalization;
using System.Reflection;

namespace Api.Modules.Registrations
{
    public class RegistrationService(List<RegistrationDto> registrations) : IRegistrationService
    {
        private static readonly Lock _lock = new();

        public void CreateRegistration(RegistrationDto registration)
        {
            lock (_lock)
            {
                registration.Id = Guid.NewGuid();
                registration.Date = DateTime.Now;
                registrations.Add(registration);
            }
        }
        public RegistrationDto GetSingleRegistration(Guid id)
        {
            var registration = registrations.First(r => r.Id == id);
            return registration;
        }

        public RegistrationsPageDto GetRegistrationsPage(int start, int increment, string sortKey, bool descending, string query)
        {
            var sortedRegistrations = SortRegistrations(sortKey, descending);

            var filteredRegistrations = SearchRegistrations(sortedRegistrations, query);

            var slicedRegistrations = SliceRegistrations(filteredRegistrations, start, increment);

            List<RegistrationPreviewDto> registrationPreviewList = [.. slicedRegistrations
                .Select(registration =>
                new RegistrationPreviewDto(
                    registration.Id,
                    registration.Name,
                    registration.Email,
                    registration.Status,
                    registration.Date))];

            var lastMonth = registrations.Where(registration => registration.Date >= DateTime.Now.AddMonths(-1)).Count();
            var pending = registrations.Where(registration => registration.Pending == true).Count();
            var page = new RegistrationsPageDto(registrationPreviewList, registrations.Count, lastMonth, pending);

            return page;
        }

        public List<RegistrationDto> GetAllRegistrations()
        {
            return registrations;
        }
        public void UpdateRegistration(RegistrationDto registration)
        {
            var registrationIndex = registrations.FindIndex(r => r.Id == registration.Id);
            registrations[registrationIndex] = registration;
        }
        public void DeleteRegistration(Guid id)
        {
            var registration = registrations.First(r => r.Id == id);
            registrations.Remove(registration);
        }
        public bool VerifyAvailableEmail(Guid id, string email)
        {
            var existingEmail = registrations.FirstOrDefault(registration => registration.Email == email);
            if (existingEmail != null)
            {
                return existingEmail.Id.Equals(id);
            }
            return true;
        }
        public List<RegistrationDto> SortRegistrations(string sortKey, bool descending)
        {
            var sortProperty = sortKey == "default" ? typeof(RegistrationDto).GetProperty("Id") : typeof(RegistrationDto).GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var sortedRegistrations = descending ? registrations.OrderByDescending(registration => sortProperty.GetValue(registration)) : registrations.OrderBy(registration => sortProperty.GetValue(registration));

            return [.. sortedRegistrations];
        }
        public List<RegistrationDto> SearchRegistrations(List<RegistrationDto> registrations, string query)
        {
            var queryResults = registrations
                .Where(registration => $"{registration.Name} {registration.Email.Split("@")[0]}"
                .Contains(query, StringComparison.CurrentCultureIgnoreCase));

            return [.. queryResults];
        }
        public List<RegistrationDto> SliceRegistrations(List<RegistrationDto> registrations, int start, int increment)
        {
            var slicedRegistrations = registrations
                .Skip(start)
                .Take(increment)
                .ToList();

            return slicedRegistrations;
        }
    }
}