using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.User
{
    public class UserRoleRelation : Entity
    {
        [Key, ForeignKey("User")]
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Key, ForeignKey("Role")]
        [Column(Order = 2)]
        public Guid RoleId { get; set; }


        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}