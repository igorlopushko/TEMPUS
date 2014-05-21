using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Models.Project
{
    public class CreateProjectMainInfoViewModel
    {
        [Required]
        [MaxLength(64, ErrorMessage = "Name field length must be less than 64 symbols.")]
        public String Name { get; set; }

        [Required]
        public Guid ManagerId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [RegularExpression("([0][.]([0-9]?)[1-9]?)")]
        public float Time { get; set; }

        [RegularExpression("[[0][.]([0-9]?)[1-9]]?")]
        public float Quality { get; set; }

        [RegularExpression("[0][.]([0-9]?)[1-9]")]
        public float Cost { get; set; }

        [MaxLength(64, ErrorMessage = "Orderer field length must be less than 64 symbols.")]
        public string Orderer { get; set; }

        [Required]
        public Guid PpsClassificationId { get; set; }

        public bool Mandatory { get; set; }

        [MaxLength(64, ErrorMessage = "Receiving Organization field length must be less than 64 symbols.")]
        public string RecievingOrganization { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public IEnumerable<SelectListItem> Managers { get; set; }
        public IEnumerable<SelectListItem> Owners { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> PpsClassifications { get; set; }
    }
}