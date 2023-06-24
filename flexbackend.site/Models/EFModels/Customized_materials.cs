namespace flexbackend.site.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customized_materials
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customized_materials()
        {
            CustomizedOrders = new HashSet<CustomizedOrder>();
            CustomizedOrders1 = new HashSet<CustomizedOrder>();
            CustomizedOrders2 = new HashSet<CustomizedOrder>();
            CustomizedOrders3 = new HashSet<CustomizedOrder>();
            CustomizedOrders4 = new HashSet<CustomizedOrder>();
            CustomizedOrders5 = new HashSet<CustomizedOrder>();
            CustomizedOrders6 = new HashSet<CustomizedOrder>();
            CustomizedOrders7 = new HashSet<CustomizedOrder>();
            CustomizedOrders8 = new HashSet<CustomizedOrder>();
            CustomizedOrders9 = new HashSet<CustomizedOrder>();
        }

        [Key]
        public int Shoesmaterial_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string material_Name { get; set; }

        public int? material_ColorId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders5 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders6 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders7 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders8 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomizedOrder> CustomizedOrders9 { get; set; }

        public virtual ShoesColorCategory ShoesColorCategory { get; set; }

        public virtual ShoesColorCategory ShoesColorCategory1 { get; set; }
    }
}
