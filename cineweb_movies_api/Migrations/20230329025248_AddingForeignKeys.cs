using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinewebmoviesapi.Migrations
{
    /// <inheritdoc />
    public partial class AddingForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngressoIdIngresso",
                table: "pedido",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pedido_FilmeId",
                table: "pedido",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_IngressoIdIngresso",
                table: "pedido",
                column: "IngressoIdIngresso");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_filme_FilmeId",
                table: "pedido",
                column: "FilmeId",
                principalTable: "filme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_ingresso_IngressoIdIngresso",
                table: "pedido",
                column: "IngressoIdIngresso",
                principalTable: "ingresso",
                principalColumn: "IdIngresso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedido_filme_FilmeId",
                table: "pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_pedido_ingresso_IngressoIdIngresso",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_pedido_FilmeId",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_pedido_IngressoIdIngresso",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "IngressoIdIngresso",
                table: "pedido");
        }
    }
}
