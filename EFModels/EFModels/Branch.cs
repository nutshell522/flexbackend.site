namespace EFModels.EFModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branch()
        {
            OneToOneReservations = new HashSet<OneToOneReservation>();
            Speakers = new HashSet<Speaker>();
        }

        public int BranchId { get; set; }

        [Required]
        [StringLength(50)]
        public string BranchName { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OneToOneReservation> OneToOneReservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Speaker> Speakers { get; set; }
    }
}
