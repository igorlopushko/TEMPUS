using System;
using TEMPUS.WebSite.Contexts;

namespace TEMPUS.WebSite.Models.Account
{
    [Serializable]
    public class UserMenuViewModel
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsAuthenticated { get; private set; }

        public UserMenuViewModel(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            IsAuthenticated = UserContext.Current != null;
        }
    }
}