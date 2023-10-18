using Business.CustomExceptions;
using Data.Models;
using System.Text.RegularExpressions;

namespace Business.ExtensionMethods
{
    public static class InputValidation
    {
        public static bool CheckUserData(this User user)
        {
            try
            {
                if (user == null)
                {
                    throw new UserNotFoundException("User doesn't have data.");
                }
            }
            catch (UserNotFoundException)
            {
                return false;
            }

            if (!Regex.IsMatch(user.Name, @"^([A-Z])\w+$") || !Regex.IsMatch(user.Surname, @"^([A-Z])\w+$") || !Regex.IsMatch(user.Number, @"^\d{9}$"))
                return false;

            return true;
        }
    }
}
