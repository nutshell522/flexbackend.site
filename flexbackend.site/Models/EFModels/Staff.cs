namespace flexbackend.site.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff
    {
        [Key]
        [Column(Order = 0)]
        public int staffId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string Name { get; set; }

        public byte? Age { get; set; }

        public bool? Gender { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string Mobile { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(300)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(30)]
        public string Account { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(70)]
        public string Password { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dueDate { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fk_PermissionsId { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fk_TitleId { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fk_DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Department Department1 { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        public virtual JobTitle JobTitle1 { get; set; }

        public virtual StaffPermission StaffPermission { get; set; }

        public virtual StaffPermission StaffPermission1 { get; set; }
    }
}
