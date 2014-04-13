using System;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Model.ServiceLayer
{
    [Serializable]
    public class UserInfo : Dto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}