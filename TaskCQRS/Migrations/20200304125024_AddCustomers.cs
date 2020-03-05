using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaskCQRS.Migrations
{
    public partial class AddCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomersData",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    birthdate = table.Column<DateTime>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    gender = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MerchantsData",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    rating = table.Column<double>(nullable: false),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantsData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentsData",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_id = table.Column<int>(nullable: false),
                    name_on_card = table.Column<string>(nullable: true),
                    exp_month = table.Column<string>(nullable: true),
                    exp_year = table.Column<string>(nullable: true),
                    postal_code = table.Column<int>(nullable: false),
                    credit_card_number = table.Column<string>(nullable: true),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsData", x => x.id);
                    table.ForeignKey(
                        name: "FK_PaymentsData_CustomersData_customer_id",
                        column: x => x.customer_id,
                        principalTable: "CustomersData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsData",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    merchant_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsData", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductsData_MerchantsData_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "MerchantsData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsData_customer_id",
                table: "PaymentsData",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsData_merchant_id",
                table: "ProductsData",
                column: "merchant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentsData");

            migrationBuilder.DropTable(
                name: "ProductsData");

            migrationBuilder.DropTable(
                name: "CustomersData");

            migrationBuilder.DropTable(
                name: "MerchantsData");
        }
    }
}
