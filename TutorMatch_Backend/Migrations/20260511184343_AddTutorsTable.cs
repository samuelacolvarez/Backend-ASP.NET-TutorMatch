using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorMatch_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTutorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tutor_TutorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlots_Tutor_TutorId",
                table: "AvailabilitySlots");

            migrationBuilder.DropForeignKey(
                name: "FK_TutoringOffers_Tutor_TutorId",
                table: "TutoringOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tutor",
                table: "Tutor");

            migrationBuilder.RenameTable(
                name: "Tutor",
                newName: "Tutors");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tutors",
                table: "Tutors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tutors_TutorId",
                table: "AspNetUsers",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlots_Tutors_TutorId",
                table: "AvailabilitySlots",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutoringOffers_Tutors_TutorId",
                table: "TutoringOffers",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tutors_TutorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlots_Tutors_TutorId",
                table: "AvailabilitySlots");

            migrationBuilder.DropForeignKey(
                name: "FK_TutoringOffers_Tutors_TutorId",
                table: "TutoringOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tutors",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Tutors",
                newName: "Tutor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tutor",
                table: "Tutor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tutor_TutorId",
                table: "AspNetUsers",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlots_Tutor_TutorId",
                table: "AvailabilitySlots",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutoringOffers_Tutor_TutorId",
                table: "TutoringOffers",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
