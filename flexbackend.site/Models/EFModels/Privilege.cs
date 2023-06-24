namespace flexbackend.site.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Privilege
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Privilege()
        {
            MembershipLevelPrivileges = new HashSet<MembershipLevelPrivilege>();
            MembershipLevelPrivileges1 = new HashSet<MembershipLevelPrivilege>();
        }

        public int PrivilegeId { get; set; }

        [Required]
        [StringLength(30)]
        public string PrivilegeName { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MembershipLevelPrivilege> MembershipLevelPrivileges { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MembershipLevelPrivilege> MembershipLevelPrivileges1 { get; set; }
    }
}
