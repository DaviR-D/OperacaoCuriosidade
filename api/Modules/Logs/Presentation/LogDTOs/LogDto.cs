namespace Api.Modules.Logs.Presentation.LogDTOs
{
    public class LogDto(string userEmail, string clientEmail, string action, DateTime timeStamp)
    {
        public string UserEmail { get; set; } = userEmail;
        public string ClientEmail { get; set; } = clientEmail;
        public string Action { get; set; } = action;
        public DateTime TimeStamp { get; set; } = timeStamp;
    }
}
