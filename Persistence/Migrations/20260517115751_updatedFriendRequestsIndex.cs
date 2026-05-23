using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatedFriendRequestsIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_SentBy_SentTo",
                table: "FriendRequests");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SentBy_SentTo",
                table: "FriendRequests",
                columns: new[] { "SentBy", "SentTo" },
                unique: true,
                filter: "\"Status\" = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_SentBy_SentTo",
                table: "FriendRequests");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SentBy_SentTo",
                table: "FriendRequests",
                columns: new[] { "SentBy", "SentTo" },
                unique: true);
        }
    }
}
