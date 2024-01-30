using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_APi.Migrations
{
    public partial class stateFiled2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_states_Stateid",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Stateid",
                table: "Candidates",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_Stateid",
                table: "Candidates",
                newName: "IX_Candidates_StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_states_StateId",
                table: "Candidates",
                column: "StateId",
                principalTable: "states",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_states_StateId",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Candidates",
                newName: "Stateid");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_StateId",
                table: "Candidates",
                newName: "IX_Candidates_Stateid");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_states_Stateid",
                table: "Candidates",
                column: "Stateid",
                principalTable: "states",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
