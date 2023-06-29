namespace EFModels.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShoesCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoesCategory()
        {
            Customized_Shoes = new HashSet<Customized_Shoes>();
        }

        public int ShoesCategoryId { get; set; }

        [Required]
        [StringLength(254)]
        public string ShoesCategoryName { get; set; }

        public DateTime? CategoryCreateTime { get; set; }

        public DateTime? CategoryEditTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customized_Shoes> Customized_Shoes { get; set; }
    }
}