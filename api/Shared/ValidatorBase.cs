using System.Text.RegularExpressions;

namespace Api.Shared
{
    public abstract class ValidatorBase
    {
        protected static bool ValidateFieldSize(string field, int maxSize = 300)
        {
            return (field.Length > 0 && field.Length <= maxSize);
        }
        protected static bool ValidateEmail(string email)
        {
            Regex regex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }
        protected static bool ValidateName(string name)
        {
            Regex regex = new(@"^[^0-9!@#$%*+={}?<>()]*$");
            return regex.IsMatch(name);
        }
    }
}
