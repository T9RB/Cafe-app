using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    IdDish = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Dishes_pk", x => x.IdDish);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    IdOrder = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Place = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    TypePayment = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    CountClient = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Orders_pk", x => x.IdOrder);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    IdPost = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Posts_pk", x => x.IdPost);
                });

            migrationBuilder.CreateTable(
                name: "StatusesUser",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StatusesUser_pk", x => x.IdStatus);
                });

            migrationBuilder.CreateTable(
                name: "WorkShifts",
                columns: table => new
                {
                    IdShift = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WorkShifts_pk", x => x.IdShift);
                });

            migrationBuilder.CreateTable(
                name: "OrderDish",
                columns: table => new
                {
                    IdList = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdDish = table.Column<int>(type: "INTEGER", nullable: false),
                    IdOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderDish_pk", x => x.IdList);
                    table.ForeignKey(
                        name: "OrderDish_Dishes_IdDish_fk",
                        column: x => x.IdDish,
                        principalTable: "Dishes",
                        principalColumn: "IdDish");
                    table.ForeignKey(
                        name: "OrderDish_Orders_IdOrder_fk",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    FName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Photo = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    EmplContract = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IdPost = table.Column<int>(type: "INTEGER", nullable: false),
                    IdStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Users_pk", x => x.IdUser);
                    table.ForeignKey(
                        name: "Users_Posts_IdPost_fk",
                        column: x => x.IdPost,
                        principalTable: "Posts",
                        principalColumn: "IdPost");
                    table.ForeignKey(
                        name: "Users_StatusesUser_IdStatus_fk",
                        column: x => x.IdStatus,
                        principalTable: "StatusesUser",
                        principalColumn: "IdStatus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserShift",
                columns: table => new
                {
                    IdList = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false),
                    IdShift = table.Column<int>(type: "INTEGER", nullable: false),
                    Place = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserShift_pk", x => x.IdList);
                    table.ForeignKey(
                        name: "UserShift_Users_IdUser_fk",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "UserShift_WorkShifts_IdShift_fk",
                        column: x => x.IdShift,
                        principalTable: "WorkShifts",
                        principalColumn: "IdShift");
                });

            migrationBuilder.CreateTable(
                name: "UsersOrders",
                columns: table => new
                {
                    IdList = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false),
                    IdOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UsersOrders_pk", x => x.IdList);
                    table.ForeignKey(
                        name: "UsersOrders_Orders_IdOrder_fk",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder");
                    table.ForeignKey(
                        name: "UsersOrders_Users_IdUser_fk",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDish_IdDish",
                table: "OrderDish",
                column: "IdDish");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDish_IdOrder",
                table: "OrderDish",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdPost",
                table: "Users",
                column: "IdPost");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdStatus",
                table: "Users",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_UserShift_IdShift",
                table: "UserShift",
                column: "IdShift");

            migrationBuilder.CreateIndex(
                name: "IX_UserShift_IdUser",
                table: "UserShift",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOrders_IdOrder",
                table: "UsersOrders",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOrders_IdUser",
                table: "UsersOrders",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDish");

            migrationBuilder.DropTable(
                name: "UserShift");

            migrationBuilder.DropTable(
                name: "UsersOrders");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "WorkShifts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "StatusesUser");
        }
    }
}
