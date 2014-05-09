using System;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Model.ServiceLayer
{
    [Serializable]
    public class UserMood : Dto
    {
        public UserId UserId { get; set; }

        public DateTime Date { get; set; }

        public string UserName { get; set; }

        public int Rate { get; set; }
    }
}