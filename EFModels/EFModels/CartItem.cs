namespace EFModels.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartItem
    {
        public int CartItemId { get; set; }

        public int fk_CardId { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(700)]
        public string Description { get; set; }

        public int Qty { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
