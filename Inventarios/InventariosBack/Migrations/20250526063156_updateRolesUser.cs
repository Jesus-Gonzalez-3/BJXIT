using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventariosModel.Migrations
{
    /// <inheritdoc />
    public partial class updateRolesUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_RolesRoleId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_RolesRoleId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RolesRoleId",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_RoleId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_RoleId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "RolesRoleId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_RolesRoleId",
                table: "users",
                column: "RolesRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_RolesRoleId",
                table: "users",
                column: "RolesRoleId",
                principalTable: "roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
