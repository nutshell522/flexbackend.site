using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Members.dll.Models.Interfaces;
using Members.dll.Models.Dtos;
using System.Configuration;

namespace Members.dll.Models.lnfra.DapperRepositories
{
	//Repository用來存取資料庫
	public class StaffDapperRepository : IStaffRepository
	{
		private string _connStr;
		public StaffDapperRepository()
		{
			_connStr = ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;//不應該寫在這裡
		}

		IEnumerable<StaffsIndexDto> IStaffRepository.GetStaffs()
		{
			string sql = @"SELECT StaffId,D.DepartmentName as [Department],TitleName,[Name],Age,Gender,Email,LevelName,dueDate
FROM Staffs as S
JOIN Departments as D ON S.fk_DepartmentId=D.DepartmentId
JOIN JobTitles as J ON S.fk_TitleId=J.TitleId
JOIN StaffPermissions as SP ON S.fk_PermissionsId=SP.PermissionsId;";

			//IEnumerable<StaffsIndexDto> staffList = new SqlConnection(_connStr).Query<StaffsIndexDto>(sql, new { Account = staffAccount });
			//return staffList.ToList();
			using (var conn = new SqlConnection(_connStr))
			{
				return conn.Query<StaffsIndexDto>(sql);
			}
		}
	}
}
