using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teste_Back_end_Predify2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeName = table.Column<string>(nullable: false),
                    Cnpj = table.Column<string>(maxLength: 14, nullable: false),
                    Uf = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CpfCnpj = table.Column<string>(maxLength: 14, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    RG = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessSuppliers",
                columns: table => new
                {
                    BusinessId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSuppliers", x => new { x.BusinessId, x.SupplierId });
                    table.ForeignKey(
                        name: "FK_BusinessSuppliers_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessSuppliers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    SupplierID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "Cnpj", "TradeName", "Uf" },
                values: new object[,]
                {
                    { 1, "12345678912345", "Nome Fantasia", "São Paulo" },
                    { 2, "01234567891111", "Fantasy Name", "Joinville" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Birthdate", "CpfCnpj", "CreatedAt", "Name", "RG" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12345678901444", new DateTime(2022, 8, 9, 7, 58, 59, 243, DateTimeKind.Local).AddTicks(7348), "Primeiro Fornecedor", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12345678901123", new DateTime(2022, 8, 9, 7, 58, 59, 243, DateTimeKind.Local).AddTicks(7348), "Segundo Fornecedor", null }
                });

            migrationBuilder.InsertData(
                table: "BusinessSuppliers",
                columns: new[] { "BusinessId", "SupplierId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "BusinessSuppliers",
                columns: new[] { "BusinessId", "SupplierId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "Number", "SupplierID", "Type" },
                values: new object[] { 1, "(11) 99999-7777", 1, "Mobile" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSuppliers_SupplierId",
                table: "BusinessSuppliers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_SupplierID",
                table: "Phones",
                column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessSuppliers");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
