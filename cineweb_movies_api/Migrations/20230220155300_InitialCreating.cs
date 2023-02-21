using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace cineweb_movies_api.Migrations
{
    public partial class InitialCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CPF = table.Column<string>(type: "text", nullable: true),
                    NomeCliente = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "filme",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime", nullable: false),
                    Genero = table.Column<string>(type: "text", nullable: false),
                    Poster = table.Column<byte[]>(type: "varbinary(4000)", nullable: true),
                    Sinopse = table.Column<string>(type: "text", nullable: false),
                    HomeMovie = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ingresso",
                columns: table => new
                {
                    IdIngresso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FilmeId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingresso", x => x.IdIngresso);
                    table.ForeignKey(
                        name: "FK_ingresso_filme_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FilmeId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    IdIngresso = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pedido_filme_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedido_ingresso_ingresso",
                        column: x => x.IdIngresso,
                        principalTable: "ingresso",
                        principalColumn: "IdIngresso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingresso_FilmeId",
                table: "ingresso",
                column: "FilmeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pedido_FilmeId",
                table: "pedido",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_ingresso",
                table: "pedido",
                column: "IdIngresso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "ingresso");

            migrationBuilder.DropTable(
                name: "filme");
        }
    }
}
