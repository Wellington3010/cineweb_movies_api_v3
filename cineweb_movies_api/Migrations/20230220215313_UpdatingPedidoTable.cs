using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cineweb_movies_api.Migrations
{
    public partial class UpdatingPedidoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CodigoPedido",
                table: "pedido",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoPedido",
                table: "pedido");
        }
    }
}
