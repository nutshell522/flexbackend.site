namespace EFModels.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShoesColorCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoesColorCategory()
        {
            Customized_materials = new HashSet<Customized_materials>();
            CustomizedShoesPoes = new HashSet<CustomizedShoesPo>();
        }

        [Key]
        public int ShoesColorId { get; set; }

        [Required]
        [StringLength(50)]
        public string ColorName { get; set; }

        [StringLength(100)]
        public string ColorCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customized_materials> Customized_materials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedShoesPo> CustomizedShoesPoes { get; set; }
    }
}
