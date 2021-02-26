using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentProcessor.Migrations
{
    public partial class PaymentProcessor_DB_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreditCardNumber = table.Column<string>(type: "TEXT", unicode: false, nullable: false),
                    CardHolder = table.Column<string>(type: "TEXT", unicode: false, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    SecurityCode = table.Column<string>(type: "TEXT", unicode: false, maxLength: 3, nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 19, scale: 4, nullable: false),
                    State = table.Column<string>(type: "TEXT", unicode: false, maxLength: 9, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
