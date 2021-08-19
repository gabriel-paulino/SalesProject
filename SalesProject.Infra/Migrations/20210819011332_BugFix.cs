using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesProject.Infra.Migrations
{
    public partial class BugFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PrevisaoMensal",
                table: "Produto",
                type: "int",
                nullable: false,
                comment: "Previsão mensal mínima combinada para o Produto.",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "Previsão mensal mínima combinada para o Produto.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PrevisaoMensal",
                table: "Produto",
                type: "float",
                nullable: false,
                comment: "Previsão mensal mínima combinada para o Produto.",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Previsão mensal mínima combinada para o Produto.");
        }
    }
}
