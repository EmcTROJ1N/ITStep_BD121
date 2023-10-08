using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
"INSERT INTO ExamDB.dbo.AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (N'9da66f30-9bab-49be-8cd0-4cae993dd060', N'Adminn', N'ADMINN', N'2@bk.ru', N'2@BK.RU', 0, N'AQAAAAIAAYagAAAAEEXi3DsC9/SJ9umjMU0Xj14rinOL3iRcUv7qGLI6mlEtN1zqdHgv4/yb2TRrn/fR4Q==', N'JUBFNHPIA5ONANXOMRYXWSQYDKNQCXGD', N'284c5b37-b150-4f40-b775-aab17ffcf2e3', null, 0, 0, null, 1, 0); " +
"INSERT INTO ExamDB.dbo.AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (N'112858b4-aeb5-44ab-9e83-91f93c3afead', N'Admin', N'ADMIN', null);" +
"INSERT INTO ExamDB.dbo.AspNetUserRoles (UserId, RoleId) VALUES (N'9da66f30-9bab-49be-8cd0-4cae993dd060', N'112858b4-aeb5-44ab-9e83-91f93c3afead');" +
        "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
