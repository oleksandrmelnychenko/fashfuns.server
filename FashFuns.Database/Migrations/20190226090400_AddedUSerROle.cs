using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FashFuns.Database.Migrations
{
    public partial class AddedUSerROle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_UserIdentityRoleType_UserRoleTypeId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserIdentityRoleType",
                table: "UserIdentityRoleType");

            migrationBuilder.RenameTable(
                name: "UserIdentityRoleType",
                newName: "UserIdentityRoleTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "UserRoles",
                nullable: true,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "UserIdentityRoleTypes",
                nullable: true,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserIdentityRoleTypes",
                table: "UserIdentityRoleTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_UserIdentityRoleTypes_UserRoleTypeId",
                table: "UserRoles",
                column: "UserRoleTypeId",
                principalTable: "UserIdentityRoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_UserIdentityRoleTypes_UserRoleTypeId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserIdentityRoleTypes",
                table: "UserIdentityRoleTypes");

            migrationBuilder.RenameTable(
                name: "UserIdentityRoleTypes",
                newName: "UserIdentityRoleType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "UserRoles",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "UserIdentityRoleType",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserIdentityRoleType",
                table: "UserIdentityRoleType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_UserIdentityRoleType_UserRoleTypeId",
                table: "UserRoles",
                column: "UserRoleTypeId",
                principalTable: "UserIdentityRoleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
