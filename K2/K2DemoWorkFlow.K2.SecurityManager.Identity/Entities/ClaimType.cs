using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    [Table("identity.ClaimTypes")]
    public partial class ClaimType
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public bool Required { get; set; }

        public bool IsStatic { get; set; }

        [StringLength(512)]
        public string Regex { get; set; }

        [StringLength(128)]
        public string RegexDescription { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public int ValueType { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedOn { get; set; }
    }
}
