namespace EFModels.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomizedOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomizedOrder()
        {
            ShoesPictures = new HashSet<ShoesPicture>();
            ShoesPictures1 = new HashSet<ShoesPicture>();
        }

        [Key]
        public int Customized_Id { get; set; }

        [Required]
        [StringLength(6000)]
        public string Customized_number { get; set; }

        public int? Customized_Shoes_Id { get; set; }

        public int? Fk_ForMemberCustomized_Id { get; set; }

        public int? Customized_Eyelet { get; set; }

        public int? Customized_EdgeProtection { get; set; }

        public int? Customized_Rear { get; set; }

        public int? Customized_Tongue { get; set; }

        public int? Customized_Toe { get; set; }

        [StringLength(254)]
        public string Remark { get; set; }

        public DateTime? OrderCreateTime { get; set; }

        public DateTime? OrderEditTime { get; set; }

        public virtual Customized_materials Customized_materials { get; set; }

        public virtual Customized_materials Customized_materials1 { get; set; }

        public virtual Customized_materials Customized_materials2 { get; set; }

        public virtual Customized_materials Customized_materials3 { get; set; }

        public virtual Customized_materials Customized_materials4 { get; set; }

        public virtual Customized_materials Customized_materials5 { get; set; }

        public virtual Customized_materials Customized_materials6 { get; set; }

        public virtual Customized_materials Customized_materials7 { get; set; }

        public virtual Customized_materials Customized_materials8 { get; set; }

        public virtual Customized_materials Customized_materials9 { get; set; }

        public virtual CustomizedShoesPo CustomizedShoesPo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoesPicture> ShoesPictures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoesPicture> ShoesPictures1 { get; set; }

        public virtual Member Member { get; set; }
    }
}
