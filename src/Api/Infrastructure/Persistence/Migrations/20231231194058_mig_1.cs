using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "email_confirmations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_confirmations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entries_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entry_comments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_comments_entries_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entry_comments_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entry_favorites",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry_favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_favorites_entries_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entry_favorites_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entry_votes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry_votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_votes_entries_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entry_votes_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entry_comment_favorites",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry_comment_favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_comment_favorites_entry_comments_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "dbo",
                        principalTable: "entry_comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entry_comment_favorites_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entry_comment_votes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry_comment_votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_comment_votes_entry_comments_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "dbo",
                        principalTable: "entry_comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entry_comment_votes_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entries_UserId",
                schema: "dbo",
                table: "entries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_comment_favorites_EntryCommentId",
                schema: "dbo",
                table: "entry_comment_favorites",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_comment_favorites_UserId",
                schema: "dbo",
                table: "entry_comment_favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_comment_votes_EntryCommentId",
                schema: "dbo",
                table: "entry_comment_votes",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_comment_votes_UserId",
                schema: "dbo",
                table: "entry_comment_votes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_comments_EntryId",
                schema: "dbo",
                table: "entry_comments",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_comments_UserId",
                schema: "dbo",
                table: "entry_comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_favorites_EntryId",
                schema: "dbo",
                table: "entry_favorites",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_favorites_UserId",
                schema: "dbo",
                table: "entry_favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_votes_EntryId",
                schema: "dbo",
                table: "entry_votes",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entry_votes_UserId",
                schema: "dbo",
                table: "entry_votes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email_confirmations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entry_comment_favorites",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entry_comment_votes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entry_favorites",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entry_votes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entry_comments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "users",
                schema: "dbo");
        }
    }
}
