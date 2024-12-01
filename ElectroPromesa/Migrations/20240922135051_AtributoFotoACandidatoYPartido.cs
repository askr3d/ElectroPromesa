using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroPromesa.Migrations
{
    /// <inheritdoc />
    public partial class AtributoFotoACandidatoYPartido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Partidos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Candidatos");
        }
    }
}
