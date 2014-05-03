using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.User
{
    public class Role : Entity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(64)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
