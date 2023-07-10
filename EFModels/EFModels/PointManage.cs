namespace EFModels.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PointManage")]
    public partial class PointManage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PointManage()
        {
            PointManage1 = new HashSet<PointManage>();
        }

        public int PointManageId { get; set; }

        public bool GetOrDeduct { get; set; }

        public int Amount { get; set; }

        public int fk_TypeId { get; set; }

        public int? TypeProductId { get; set; }

        public DateTime? PointExpirationDate { get; set; }

        public int PointManage2_PointManageId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PointManage> PointManage1 { get; set; }

        public virtual PointManage PointManage2 { get; set; }

        public virtual Type Type { get; set; }
    }
}
