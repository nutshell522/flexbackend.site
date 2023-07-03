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
using Members.dll.Models.ViewsModels;

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

		//忘記密碼
		public void SaveNewPassword(string newpassword, string account)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				conn.Open();

				string sql = @"update Staffs set [Password] = @Password where Account = @Account;";
				conn.Execute(sql, new { Password = newpassword, Account = account });
			}
		}

		public void CreateStaff(StaffsCreateDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				conn.Open();

				string sql = @"INSERT INTO Staffs ([Name],Birthday,Gender,Age,Email,Mobile,Account,Password,fk_PermissionsId,fk_TitleId,fk_DepartmentId)
VALUES  (@Name,@Birthday,@Gender,@Age,@Email,@Mobile,@Account,@Password,@fk_PermissionsId,@fk_TitleId,@fk_DepartmentId);";
				conn.Execute(sql, dto);
			}

		}


		//Read
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

		public void DeleteStaff(int staffId)
		{

			using (var conn = new SqlConnection(_connStr))
			{
				conn.Open();

				string sql = @"DELETE FROM Staffs
WHERE StaffId = @StaffId;";
				conn.Execute(sql, new { StaffId = staffId });
			}
		}
	}
}
