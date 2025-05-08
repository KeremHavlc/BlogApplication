using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Geçici kolon oluştur
            migrationBuilder.AddColumn<byte[]>(
                name: "Image_temp",
                table: "Posts",
                type: "varbinary(max)",
                nullable: true);

            // 2. Eğer string base64 data varsa, SQL'de bunu dönüştürmek zordur.
            // Manuel bir UPDATE yapılması gerekebilir, genelde dışarıdan (EF dışı)

            // 3. Eski kolonu sil
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Posts");

            // 4. Yeniden ekle (doğru türle)
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Posts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            // 5. Geçici kolonu sil
            migrationBuilder.DropColumn(
                name: "Image_temp",
                table: "Posts");

            // Comments tablosunu değiştirme kısmı aynı kalabilir
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
