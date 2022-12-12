using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba_proyecto_1.Migrations
{
    public partial class Migracion_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libro_Autor_AutorId",
                table: "Libro");

            migrationBuilder.DropForeignKey(
                name: "FK_Libro_Genero_GeneroId",
                table: "Libro");

            migrationBuilder.DropIndex(
                name: "IX_Libro_AutorId",
                table: "Libro");

            migrationBuilder.DropIndex(
                name: "IX_Libro_GeneroId",
                table: "Libro");

            migrationBuilder.AddColumn<int>(
                name: "GeneroClass",
                table: "Genero",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AutorClass",
                table: "Autor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genero_GeneroClass",
                table: "Genero",
                column: "GeneroClass");

            migrationBuilder.CreateIndex(
                name: "IX_Autor_AutorClass",
                table: "Autor",
                column: "AutorClass");

            migrationBuilder.AddForeignKey(
                name: "FK_Autor_Libro_AutorClass",
                table: "Autor",
                column: "AutorClass",
                principalTable: "Libro",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genero_Libro_GeneroClass",
                table: "Genero",
                column: "GeneroClass",
                principalTable: "Libro",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autor_Libro_AutorClass",
                table: "Autor");

            migrationBuilder.DropForeignKey(
                name: "FK_Genero_Libro_GeneroClass",
                table: "Genero");

            migrationBuilder.DropIndex(
                name: "IX_Genero_GeneroClass",
                table: "Genero");

            migrationBuilder.DropIndex(
                name: "IX_Autor_AutorClass",
                table: "Autor");

            migrationBuilder.DropColumn(
                name: "GeneroClass",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "AutorClass",
                table: "Autor");

            migrationBuilder.CreateIndex(
                name: "IX_Libro_AutorId",
                table: "Libro",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Libro_GeneroId",
                table: "Libro",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libro_Autor_AutorId",
                table: "Libro",
                column: "AutorId",
                principalTable: "Autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libro_Genero_GeneroId",
                table: "Libro",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
