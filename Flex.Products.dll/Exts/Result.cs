using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Models.Infra.Exts
{
	public class Result
	{
		public bool IsSucces { get; set; }
		public bool IsFailed => !IsSucces;
		public string ErroeMessage { get; set; }
		public static Result Success()=>new Result { IsSucces=true,ErroeMessage=null};
		
		public static Result Fail(string errormessage)=>new Result { IsSucces = false, ErroeMessage = errormessage };
	}
}
