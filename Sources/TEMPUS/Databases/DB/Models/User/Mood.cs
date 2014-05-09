using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.User
{
    public class Mood : Entity
    {
        [Key, ForeignKey("User")]
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression("([1-5])")]
        public int Rate { get; set; }

        public virtual User User { get; set; }
    }
}
