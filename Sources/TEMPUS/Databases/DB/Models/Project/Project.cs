using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.Project
{
    public class Project : Entity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(64)]
        public string ProjectOrderer { get; set; }

        [MaxLength(64)]
        public string RecievingOrganization { get; set; }

        public bool Mandatory { get; set; }

        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [ForeignKey("PpsClassification")]
        public Guid PpsClassificationId { get; set; }

        public virtual PpsClassification PpsClassification { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public IEnumerable<ProjectRoleRelation> TeamMembers { get; set; }
    }
}