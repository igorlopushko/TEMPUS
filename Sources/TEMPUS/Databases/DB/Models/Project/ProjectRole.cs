using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.Project
{
    public class ProjectRole : Entity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(64)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
