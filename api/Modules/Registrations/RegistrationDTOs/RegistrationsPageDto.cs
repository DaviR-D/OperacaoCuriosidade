namespace Api.Modules.Registrations
{
    public class RegistrationsPageDto
    {
        public List<RegistrationPreviewDto> registrations { get; set; }
        public int registrationsLength { get; set; }
        public int lastMonthRegistrations { get; set; }
        public int pendingRegistrations {  get; set; }

        public RegistrationsPageDto(List<RegistrationPreviewDto> r, int length, int lastMonth, int pending)
        {
            registrations = r;
            registrationsLength = length;
            lastMonthRegistrations = lastMonth;
            pendingRegistrations = pending;
        }
    }
}
