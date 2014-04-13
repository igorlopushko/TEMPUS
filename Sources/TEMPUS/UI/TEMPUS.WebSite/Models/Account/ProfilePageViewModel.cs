using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEMPUS.WebSite.Models.Account
{
    public class ProfilePageViewModel
    {
        public string FirstName { get; private set; }

        public ProfilePageViewModel(string firstName)
        {
            FirstName = firstName;
        }
    }
}