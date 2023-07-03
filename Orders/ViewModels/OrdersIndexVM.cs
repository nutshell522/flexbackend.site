using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EFModels.EFModels;

namespace Orders.ViewModels
{
    public class OrdersIndexVM
    {
        public int Id { get; set; }
		[Display(Name = "訂單時間")]
		public DateTime ordertime { get; set; }
		[Display(Name = "會員編號")]
		public int fk_member_Id { get; set; }
		[Display(Name = "總數量")]
		public int total_quantity { get; set; }
		[Display(Name = "物流編號")]
		public int logistics_company_Id { get; set; }
		[Display(Name = "商品狀態")]
		public int order_status_Id { get; set; }
		[Display(Name = "付款方式")]
		public int pay_method_Id { get; set; }
		[Display(Name = "付款狀態")]
		public int pay_status_Id { get; set; }
		[Display(Name = "優惠")]
		public string coupon_name { get; set; }
		[Display(Name = "折扣")]
		public int? coupon_discount { get; set; }
		[Display(Name = "運費")]
		public int? freight { get; set; }
		[Display(Name = "收件人電話")]
		public string cellphone { get; set; }
		[Display(Name = "收據號碼")]
		public string receipt { get; set; }
		[Display(Name = "收件人名稱")]
		public string receiver { get; set; }
		[Display(Name = "收件人地址")]
		public string recipient_address { get; set; }
		[Display(Name = "商品描述")]
		public string order_description { get; set; }
		[Display(Name = "結單")]
		public int? close_Id { get; set; }
		[Display(Name = "總金額")]
		public int total_price { get; set; }
		public List<OrderItemsVM> orderItems { get; set; }
	}
}
