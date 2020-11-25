using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Armazenagem3L_API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Armazenagem3L");

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
                    table.ForeignKey(
                        name: "FK_Cargas_Motoristas_MotoristaId",
                        column: x => x.MotoristaId,
                        principalSchema: "Armazenagem3L",
                        principalTable: "Motoristas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                schema: "Armazenagem3L",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Peso = table.Column<decimal>(type: "numeric", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    CargaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Cargas_CargaId",
                        column: x => x.CargaId,
                        principalSchema: "Armazenagem3L",
                        principalTable: "Cargas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_Cargas_MotoristaId",
                schema: "Armazenagem3L",
                table: "Cargas",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CargaId",
                schema: "Armazenagem3L",
                table: "Produtos",
                column: "CargaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios",
                schema: "Armazenagem3L");

            migrationBuilder.DropTable(
                name: "Produtos",
                schema: "Armazenagem3L");

            migrationBuilder.DropTable(
                name: "Cargas",
                schema: "Armazenagem3L");

            migrationBuilder.DropTable(
                name: "Motoristas",
                schema: "Armazenagem3L");
        }
    }
}
