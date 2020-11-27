﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Armazenagem3L_API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Armazenagem3L");

            migrationBuilder.CreateTable(
                name: "Cargas",
                schema: "Armazenagem3L",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Endereco = table.Column<string>(type: "text", nullable: true),
                    Frete = table.Column<decimal>(type: "numeric", nullable: false),
                    MotoristaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                schema: "Armazenagem3L",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motoristas",
                schema: "Armazenagem3L",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoristas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                schema: "Armazenagem3L",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(8, 3)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Qtd = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CargaProdutos",
                schema: "Armazenagem3L",
                columns: table => new
                {
                    CargaId = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<int>(type: "integer", nullable: false),
                    Qtd = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargaProdutos", x => new { x.CargaId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_CargaProdutos_Cargas_CargaId",
                        column: x => x.CargaId,
                        principalSchema: "Armazenagem3L",
                        principalTable: "Cargas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargaProdutos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalSchema: "Armazenagem3L",
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Armazenagem3L",
                table: "Funcionarios",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Lauro" });

            migrationBuilder.InsertData(
                schema: "Armazenagem3L",
                table: "Motoristas",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Bino" });

            migrationBuilder.InsertData(
                schema: "Armazenagem3L",
                table: "Produtos",
                columns: new[] { "Id", "Nome", "Peso", "Preco", "Qtd" },
                values: new object[,]
                {
                    { 1, "Playstation 5", 1m, 1m, 300 },
                    { 2, "Mouse", 1m, 1m, 300 },
                    { 3, "Teclkado", 1m, 1m, 300 },
                    { 4, "Monitor", 1m, 1m, 300 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargaProdutos_ProdutoId",
                schema: "Armazenagem3L",
                table: "CargaProdutos",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargaProdutos",
                schema: "Armazenagem3L");

            migrationBuilder.DropTable(
                name: "Funcionarios",
                schema: "Armazenagem3L");

            migrationBuilder.DropTable(
                name: "Motoristas",
                schema: "Armazenagem3L");

            migrationBuilder.DropTable(
                name: "Cargas",
                schema: "Armazenagem3L");

            migrationBuilder.DropTable(
                name: "Produtos",
                schema: "Armazenagem3L");
        }
    }
}
