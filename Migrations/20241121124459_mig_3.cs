using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetaylari_Siparisler_SiparisId",
                table: "SiparisDetaylari");

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetaylari_Siparisler_SiparisId",
                table: "SiparisDetaylari",
                column: "SiparisId",
                principalTable: "Siparisler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetaylari_Siparisler_SiparisId",
                table: "SiparisDetaylari");

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetaylari_Siparisler_SiparisId",
                table: "SiparisDetaylari",
                column: "SiparisId",
                principalTable: "Siparisler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
