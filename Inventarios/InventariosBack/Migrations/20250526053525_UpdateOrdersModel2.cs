using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventariosModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrdersModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_users_CreatedUserId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_CreatedUserId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "order");

            migrationBuilder.CreateIndex(
                name: "IX_order_CreatedBy",
                table: "order",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_order_users_CreatedBy",
                table: "order",
                column: "CreatedBy",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_users_CreatedBy",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_CreatedBy",
                table: "order");

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_order_CreatedUserId",
                table: "order",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_users_CreatedUserId",
                table: "order",
                column: "CreatedUserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
