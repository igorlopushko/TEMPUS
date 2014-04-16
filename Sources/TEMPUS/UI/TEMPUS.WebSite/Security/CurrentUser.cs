using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.WebSite.Security
{
    public class CurrentUser
    {
        private static readonly IUserQueryService _userQueryService;

        public static UserInfo User { get; private set; }

        public static void LogIn(string login, string password)
        {
            User = new UserInfo
            {
                Login = login,
                Password = password
            };
        }

        public static void LogOut()
        {
            User = null;
        }
    }
}