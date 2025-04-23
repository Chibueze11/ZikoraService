using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZikoraService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dsts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocOne",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IDImage",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "Pin",
                table: "Customer",
                newName: "PIN");

            migrationBuilder.RenameColumn(
                name: "Signature",
                table: "Customer",
                newName: "SignatureUrl");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Customer",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "NOKPhone",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "NOKName",
                table: "Customer",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "MName",
                table: "Customer",
                newName: "IDImageUrl");

            migrationBuilder.RenameColumn(
                name: "LName",
                table: "Customer",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Customer",
                newName: "DocumentOne");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customer",
                newName: "MyCustomerKey");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ReferralId",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PIN",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Customer",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Customer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextOfKinName",
                table: "Customer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextOfKinPhone",
                table: "Customer",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NextOfKinName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NextOfKinPhone",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "PIN",
                table: "Customer",
                newName: "Pin");

            migrationBuilder.RenameColumn(
                name: "SignatureUrl",
                table: "Customer",
                newName: "Signature");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Customer",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customer",
                newName: "NOKPhone");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Customer",
                newName: "NOKName");

            migrationBuilder.RenameColumn(
                name: "IDImageUrl",
                table: "Customer",
                newName: "MName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customer",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "DocumentOne",
                table: "Customer",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "MyCustomerKey",
                table: "Customer",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferralId",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pin",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customer",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocOne",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IDImage",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
