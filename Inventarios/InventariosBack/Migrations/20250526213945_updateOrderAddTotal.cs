using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventariosModel.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderAddTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "order",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "order");
        }
    }
}
