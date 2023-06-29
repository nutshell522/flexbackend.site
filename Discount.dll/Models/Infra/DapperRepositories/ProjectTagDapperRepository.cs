using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;
using Discount.dll.Models.ViewModels;
using Discount.dll.Models.Dtos;
using Discount.dll.Models.Interfaces;
using System.Security.Cryptography;
using System.Data;
using System.Security.Principal;

namespace Discount.dll.Models.Infra.DapperRepositories
{
    public class ProjectTagDapperRepository: IProjectTagRepository
    {
        private readonly string _connStr;

        public ProjectTagDapperRepository()
        {
            _connStr = ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
        }

        public IEnumerable<ProjectTagIndexDto> SearchProjectTags(string projectTagName=null, bool getCompleteResult = false)
        {
            #region 組合sql語法
            string sql = @"SELECT pt.ProjectTagId AS ProjectTagId, pt.ProjectTagName AS ProjectTagName,
STUFF(
    (SELECT ', ' + p.ProductName
    FROM ProjectTagItems AS pti
    INNER JOIN Products AS p ON p.ProductId = pti.fk_ProductId
    INNER JOIN ProductSubCategories AS psc ON psc.ProductSubCategoryId = p.fk_ProductSubCategoryID
    INNER JOIN ProductCategories AS pc ON psc.fk_ProductCategoryId = pc.ProductCategoryId
    INNER JOIN SalesCategories AS sc ON pc.fk_SalesCategoryId = sc.SalesCategoryId
    WHERE pti.fk_ProjectTagId = pt.ProjectTagId
    FOR XML PATH('')
    ), 1, 2, ''
) AS ProductItems,
pt.CreateAt AS CreateAt, pt.ModifiedAt AS ModifiedAt, pt.Status AS Status
FROM ProjectTags AS pt
";

            string where = string.Empty;
            
            if (!string.IsNullOrEmpty(projectTagName))
            {
                where += " And pt.ProjectTagName LIKE '%' + @ProjectTagName + '%'";
            }

            if (!getCompleteResult)
            {
                where += " And pt.status = 1";
            }
            where = string.IsNullOrEmpty(where) ? string.Empty : " WHERE " + where.Substring(5);

            sql += where + " ORDER BY pt.Status DESC, pt.ProjectTagId DESC";

            #endregion

            IEnumerable<ProjectTagIndexDto> projectTags = new SqlConnection(_connStr)
                .Query<ProjectTagIndexDto>(sql, new { ProjectTagName = projectTagName });

            return projectTags.ToList();
        }

        public void UpdateProjectTag(ProjectTagStatusChangeDto dto)
        {

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();

                string sql = @"UPDATE ProjectTags
                       SET ModifiedAt = GETDATE(),
                           Status = @Status
                       WHERE ProjectTagId = @ProjectTagId";

                connection.Execute(sql, new
                {
                    ProjectTagId = dto.ProjectTagId,
                    Status = dto.Status
                });
            }
        }

        public int CreateProjectTag(ProjectTagEditNameDto dto)
        {
            int projectId = 0;

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();

                string sql = @"INSERT INTO ProjectTags (ProjectTagName, CreateAt,ModifiedAt,Status)
                       VALUES (@ProjectTagName, GETDATE(),GETDATE(),1);
                       SELECT SCOPE_IDENTITY();";

                projectId = connection.ExecuteScalar<int>(sql, new
                {
                    ProjectTagName = dto.ProjectTagName
                });
            }

            return projectId;
        }

        public void UpdateProjectTag(ProjectTagEditNameDto dto)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();

                string sql = @"UPDATE ProjectTags
                       SET ProjectTagName = @ProjectTagName,
                           ModifiedAt = GETDATE()
                       WHERE ProjectTagId = @ProjectTagId";

                connection.Execute(sql, new
                {
                    ProjectTagId = dto.ProjectTagId,
                    ProjectTagName = dto.ProjectTagName
                });
            }
        }

        public ProjectTagEditNameDto GetProjectTag(int? Id)
        {
            string sql = $@"
        SELECT pt.ProjectTagId AS ProjectTagId, pt.ProjectTagName AS ProjectTagName, pt.Status AS Status
        FROM ProjectTags AS pt
        WHERE pt.ProjectTagId = ISNULL(@Id, (SELECT TOP 1 ProjectTagId FROM ProjectTags WHERE Status = 1 ORDER BY ProjectTagId DESC))
        ORDER BY pt.ProjectTagId DESC
    ";

            using (IDbConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<ProjectTagEditNameDto>(sql, new { Id });
            }
        }

        public bool ExistsTagName(string tagName)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM ProjectTags WHERE ProjectTagName = @TagName";
                int count = connection.ExecuteScalar<int>(sql, new { TagName = tagName });

                return count > 0;
            }
        }

        public bool ExistsTagName(string tagName, int excludeId)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM ProjectTags WHERE ProjectTagName = @TagName AND ProjectTagId != @ExcludeId";
                int count = connection.ExecuteScalar<int>(sql, new { TagName = tagName, ExcludeId = excludeId });

                return count > 0;
            }
        }
    }
}
