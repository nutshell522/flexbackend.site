using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.ViewModels
{
    public class OrdersIndexVM
    {
        public int Id { get; set; }

        public DateTime ordertime { get; set; }

        //public int fk_member_Id { get; set; }

        public int total_quantity { get; set; }

       // public int logistics_company_Id { get; set; }

       // public int order_status_Id { get; set; }

        //public int pay_method_Id { get; set; }

        //public int pay_status_Id { get; set; }

        public string coupon_name { get; set; }

        public int? coupon_discount { get; set; }

        public int? freight { get; set; }

        public string cellphone { get; set; }

        public string receipt { get; set; }

        public string receiver { get; set; }

        public string recipient_address { get; set; }

        public string order_description { get; set; }

        //public int? close_Id { get; set; }
    }
}
