namespace flexbackend.site.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Speaker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Speaker()
        {
            Activities = new HashSet<Activity>();
            Activities1 = new HashSet<Activity>();
            OneToOneReservations = new HashSet<OneToOneReservation>();
            OneToOneReservations1 = new HashSet<OneToOneReservation>();
        }

        public int SpeakerId { get; set; }

        [Required]
        [StringLength(50)]
        public string SpeakerName { get; set; }

        [StringLength(10)]
        public string SpeakerPhone { get; set; }

        public int fk_SpeakerFieldId { get; set; }

        [StringLength(300)]
        public string SpeakerImg { get; set; }

        public int? fk_SpeakerBranchId { get; set; }

        [StringLength(500)]
        public string SpeakerDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities1 { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Branch Branch1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OneToOneReservation> OneToOneReservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OneToOneReservation> OneToOneReservations1 { get; set; }

        public virtual SpeakerField SpeakerField { get; set; }

        public virtual SpeakerField SpeakerField1 { get; set; }
    }
}
