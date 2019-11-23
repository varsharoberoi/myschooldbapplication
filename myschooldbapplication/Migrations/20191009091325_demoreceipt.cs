using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace myschooldbapplication.Migrations
{
    public partial class demoreceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiptStudent",
                columns: table => new
                {
                    Receiptno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PayDate = table.Column<DateTime>(nullable: false),
                    Pay_mode = table.Column<string>(nullable: true),
                    Chequeno = table.Column<string>(nullable: true),
                    Chequedt = table.Column<DateTime>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    BankBranch = table.Column<string>(nullable: true),
                    Transactionno = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptStudent", x => x.Receiptno);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admissiopay_receiptno",
                table: "admissiopay",
                column: "receiptno");

            migrationBuilder.AddForeignKey(
                name: "FK_admissiopay_ReceiptStudent_receiptno",
                table: "admissiopay",
                column: "receiptno",
                principalTable: "ReceiptStudent",
                principalColumn: "Receiptno",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_admissiopay_ReceiptStudent_receiptno",
                table: "admissiopay");

            migrationBuilder.DropTable(
                name: "ReceiptStudent");

            migrationBuilder.DropIndex(
                name: "IX_admissiopay_receiptno",
                table: "admissiopay");
        }
    }
}
