using Microsoft.EntityFrameworkCore.Migrations;

namespace Pessoas.Data.Migrations
{
    public partial class migrationEnderecos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereços_Pessoa_PessoaId",
                table: "Endereços");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereços",
                table: "Endereços");

            migrationBuilder.RenameTable(
                name: "Endereços",
                newName: "Enderecos");

            migrationBuilder.RenameIndex(
                name: "IX_Endereços_PessoaId",
                table: "Enderecos",
                newName: "IX_Enderecos_PessoaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Pessoa_PessoaId",
                table: "Enderecos",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Pessoa_PessoaId",
                table: "Enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "Endereços");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_PessoaId",
                table: "Endereços",
                newName: "IX_Endereços_PessoaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereços",
                table: "Endereços",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereços_Pessoa_PessoaId",
                table: "Endereços",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
