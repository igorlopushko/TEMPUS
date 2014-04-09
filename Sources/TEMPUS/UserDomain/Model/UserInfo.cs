using System;
using TEMPUS.BaseDomain.Model;

namespace TEMPUS.UserDomain.Model
{
    [Serializable]
    public class UserInfo : Dto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}