using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SiparisDetaylari",
                table: "SiparisDetaylari");

            migrationBuilder.DropIndex(
                name: "IX_SiparisDetaylari_Siparis_Id",
                table: "SiparisDetaylari");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SiparisDetaylari");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiparisDetaylari",
                table: "SiparisDetaylari",
                columns: new[] { "Siparis_Id", "Urun_Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SiparisDetaylari",
                table: "SiparisDetaylari");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SiparisDetaylari",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiparisDetaylari",
                table: "SiparisDetaylari",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylari_Siparis_Id",
                table: "SiparisDetaylari",
                column: "Siparis_Id");
        }
    }
}
