using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.Project
{
    public class ProjectRoleRelation : Entity
    {
        [Key, ForeignKey("Project")]
        [Column(Order = 1)]
        public Guid ProjectId { get; set; }

        [Key, ForeignKey("ProjectRole")]
        [Column(Order = 2)]
        public Guid ProjectRoleId { get; set; }

        [Key, ForeignKey("User")]
        [Column(Order = 3)]
        public Guid UserId { get; set; }

        public Project Project { get; set; }
        public ProjectRole ProjectRole { get; set; }
        public TEMPUS.DB.Models.User.User User { get; set; }
    }
}