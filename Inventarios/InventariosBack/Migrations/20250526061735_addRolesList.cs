using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventariosModel.Migrations
{
    /// <inheritdoc />
    public partial class addRolesList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "users",
                type: "int",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AlterColumn<int>(
                name: "RoleStatus",
                table: "roles",
                type: "int",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductStatus",
                table: "productos",
                type: "int",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "order",
                type: "int",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleId", "RoleName", "RoleStatus" },
                values: new object[,]
                {
                    { 1, "Administrador", 1 },
                    { 2, "Personal Administrativo", 1 },
                    { 3, "Ventas", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ProductStatus",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "order");

            migrationBuilder.AlterColumn<int>(
                name: "RoleStatus",
                table: "roles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "1");
        }
    }
}
