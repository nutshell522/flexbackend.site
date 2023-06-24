namespace flexbackend.site.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShoesPicture
    {
        [Key]
        public int ShoesPicture_Id { get; set; }

        [StringLength(4000)]
        public string ShoesPictureUrl { get; set; }

        public int? fk_ShoesPictureProduct_Id { get; set; }

        public int? fk_ShoesProductOrder_Id { get; set; }

        public virtual Customized_Shoes Customized_Shoes { get; set; }

        public virtual Customized_Shoes Customized_Shoes1 { get; set; }

        public virtual CustomizedOrder CustomizedOrder { get; set; }

        public virtual CustomizedOrder CustomizedOrder1 { get; set; }
    }
}
