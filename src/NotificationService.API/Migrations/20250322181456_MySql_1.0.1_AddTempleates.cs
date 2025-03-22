using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotificationService.API.Migrations
{
    /// <inheritdoc />
    public partial class MySql_101_AddTempleates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_TemplateType_TypeId",
                table: "Templates");

            migrationBuilder.DropTable(
                name: "TemplateType");

            migrationBuilder.DropIndex(
                name: "IX_Templates_TypeId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Templates");

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Name", "Subject", "Text" },
                values: new object[,]
                {
                    { 1, "Registration", "Potvrzení registrace", "Dobrý den {name},\n\nvítejte v našem sportcentruMáme velkou radost, že jste se rozhodli stát se součástí naší komunity.\nAbyste mohli naplno využívat všech možností, doporučujeme se přihlásit a prozkoumat svůj nový účet.\nPokud budete mít jakékoli otázky nebo budete potřebovat pomoc, neváhejte nás kontaktovat.\nDěkujeme za registraci a přejeme mnoho skvělých zážitků!\n\nTým podpory zákazníků" },
                    { 2, "Verification", "Ověření e-mailu", "Dobrý den {name},\n\nprosíme o ověření vaší e-mailové adresy kliknutím na následující odkaz:\n{link}\n\nPokud jste tuto žádost neodeslali, ignorujte tento e-mail.\n\nDěkujeme,\nTým podpory zákazníků" },
                    { 3, "PasswordReset", "Obnovení hesla", "Dobrý den {name},\n\npro obnovení hesla klikněte na následující odkaz:\n{link}\n\nPokud jste tuto žádost neodeslali, ignorujte tento e-mail.\n\nDěkujeme,\nTým podpory zákazníků" },
                    { 4, "ReservationConfirmation", "Potvrzení rezervace", "Dobrý den {name},\n\nvaše rezervace byla úspěšně vytvořena.\nDatum a čas: {datetime}\n\nPokud máte jakékoli dotazy nebo potřebujete změnit rezervaci, neváhejte nás kontaktovat.\n\nDěkujeme,\nTým podpory zákazníků" },
                    { 5, "ReservationCancellation", "Zrušení rezervace", "Dobrý den {name},\n\nvaše rezervace byla zrušena.\nDatum a čas: {datetime}\n\nPokud jste tuto žádost neodeslali, ignorujte tento e-mail.\n\nDěkujeme,\nTým podpory zákazníků" },
                    { 6, "Registration", "Registration Confirmation", "Hello {name},\n\nwelcome to our sports center.We are very excited that you have decided to become part of our community.\nTo fully utilize all the features, we recommend logging in and exploring your new account.\nIf you have any questions or need assistance, please do not hesitate to contact us.\nThank you for registering and we wish you many great experiences!\n\nCustomer Support Team" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TemplateType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TypeId",
                table: "Templates",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_TemplateType_TypeId",
                table: "Templates",
                column: "TypeId",
                principalTable: "TemplateType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
