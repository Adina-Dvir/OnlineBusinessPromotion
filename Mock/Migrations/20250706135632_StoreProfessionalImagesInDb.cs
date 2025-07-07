using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    /// <inheritdoc />
    public partial class StoreProfessionalImagesInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProfessionalImages",
                newName: "FileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "ProfessionalImages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "ProfessionalImages");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ProfessionalImages",
                newName: "ImageUrl");
        }
    }
}
