using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity_Framework.Migrations
{
    public partial class AddOrderSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<long>(
                name: "OrderNo",
                table: "OrderTests",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValueSql: "NEXT VALUE FOR OrderNumber");

            migrationBuilder.AlterColumn<long>(
                name: "OrderNo",
                table: "Orders",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValueSql: "NEXT VALUE FOR OrderNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "OrderNumber");

            migrationBuilder.AlterColumn<long>(
                name: "OrderNo",
                table: "OrderTests",
                type: "bigint",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR OrderNumber",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "OrderNo",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR OrderNumber",
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
