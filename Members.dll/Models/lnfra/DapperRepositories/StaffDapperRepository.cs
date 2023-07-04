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
using Members.dll.Models.Dtos.Staff;
using System.Security.Principal;

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

		public StaffDetailDto StaffDetail(int staffId)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				conn.Open();

				string sql = @"SELECT StaffId,D.DepartmentName as [Department],TitleName,[Name],Age,Gender,Mobile,Email,Birthday,LevelName,DueDate
FROM Staffs as S
JOIN Departments as D ON S.fk_DepartmentId=D.DepartmentId
JOIN JobTitles as J ON S.fk_TitleId=J.TitleId
JOIN StaffPermissions as SP ON S.fk_PermissionsId=SP.PermissionsId
WHERE StaffId=@staffId;";
				return conn.QueryFirstOrDefault<StaffDetailDto>(sql, new { StaffId = staffId });
			}
		}

		//		public void EditStaff(EditStaffDto dto)
		//		{
		//			using (var conn = new SqlConnection(_connStr))
		//			{
		//				conn.Open();

		//				string sql = @"UPDATE Staffs
		//SET 
		//    Department = @Department,
		//    JobTitle = @TitleName,
		//    [Name] = @Name,
		//    Age = @Age,
		//    Gender = @Gender,
		//    Mobile = @Mobile,
		//    Email = @Email,
		//    Birthday = @Birthday,
		//    LevelName = @LevelName,
		//    DueDate = @DueDate
		//FROM Staffs as S
		//JOIN Departments as D ON S.fk_DepartmentId = D.DepartmentId
		//JOIN JobTitles as J ON S.fk_TitleId = J.TitleId
		//JOIN StaffPermissions as SP ON S.fk_PermissionsId = SP.PermissionsId
		//WHERE StaffId = @staffId;";
		//				conn.Execute(sql,dto);
		//			}
		//		}
		public EditStaffDto EditStaff(EditStaffDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				conn.Open();

				// 使用 JOIN 子句執行 SELECT 語句
				string selectSql = @"
SELECT StaffId, D.DepartmentName AS Department, J.TitleName, S.Name, S.Age, S.Gender, S.Mobile, S.Email, S.Birthday, S.LevelName, S.DueDate
FROM Staffs AS S
JOIN Departments AS D ON S.fk_DepartmentId = D.DepartmentId
JOIN JobTitles AS J ON S.fk_TitleId = J.TitleId
JOIN StaffPermissions AS SP ON S.fk_PermissionsId = SP.PermissionsId
WHERE StaffId = @staffId;";
				var staffData = conn.QueryFirstOrDefault<EditStaffDto>(selectSql, new { dto });

				if (staffData != null)
				{
					// 手動構建 UPDATE 語法
					string updateSql = @"
UPDATE Staffs
SET 
    Department = @Department,
    JobTitle = @TitleName,
    [Name] = @Name,
    Age = @Age,
    Gender = @Gender,
    Mobile = @Mobile,
    Email = @Email,
    Birthday = @Birthday,
    LevelName = @LevelName,
    DueDate = @DueDate
WHERE StaffId = @StaffId;";

					// 使用 Dapper 的 conn.Execute() 方法執行 UPDATE 語法
					conn.Execute(updateSql, new
					{
						Department = staffData.Department,
						TitleName = staffData.TitleName,
						Name = staffData.Name,
						Age = staffData.Age,
						Gender = staffData.Gender,
						Mobile = staffData.Mobile,
						Email = staffData.Email,
						Birthday = staffData.Birthday,
						LevelName = staffData.LevelName,
						DueDate = staffData.DueDate,
						StaffId = staffData.StaffId
					});

				}

				// 返回修改後的 staffData 物件
				return staffData;
			}
		}


	}
}
