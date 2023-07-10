namespace EFModels.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectTagItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectTagItem()
        {
            ProjectTagItems1 = new HashSet<ProjectTagItem>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fk_ProjectTagId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(254)]
        public string fk_ProductId { get; set; }

        public int ProjectTagItem1_fk_ProjectTagId { get; set; }

        [Required]
        [StringLength(254)]
        public string ProjectTagItem1_fk_ProductId { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectTagItem> ProjectTagItems1 { get; set; }

        public virtual ProjectTagItem ProjectTagItem1 { get; set; }
    }
}
