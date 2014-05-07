using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.Project
{
    public class Department : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(64)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}