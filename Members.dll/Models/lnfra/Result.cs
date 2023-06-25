using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.lnfra
{
	public class Result
	{
		public bool IsSuccess { get; private set; } 
		public string ErrorMessage { get; private set; } 

		public static Result Success()=>new Result { IsSuccess=true,ErrorMessage=null};

		public static Result Fail(string errormessage) => new Result { IsSuccess = false, ErrorMessage = errormessage };
	}
}
