using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingPropertyofCinemaFormatsandHalls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "AspNetRoles",
            //     columns: table => new
            //     {
            //         Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUsers",
            //     columns: table => new
            //     {
            //         Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         Age = table.Column<int>(type: "int", nullable: false),
            //         UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //         PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //         TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //         LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //         LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //         AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Bookings",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         TransactionNumber = table.Column<int>(type: "int", nullable: false),
            //         BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            //         UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         CinemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         HallId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //         ShowTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         PaymentMethod = table.Column<int>(type: "int", nullable: false),
            //         BookingStatus = table.Column<int>(type: "int", nullable: false),
            //         created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Bookings", x => x.Id);
            //     });

            migrationBuilder.CreateTable(
                name: "CinemaFormats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScreenType = table.Column<int>(type: "int", nullable: false),
                    ScreenTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaFormats", x => x.Id);
                });

            // migrationBuilder.CreateTable(
            //     name: "Cinemas",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         CinemaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //         Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //         created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Cinemas", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Genres",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Genres", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Movies",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Movies", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Seat",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         HallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Seat", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetRoleClaims",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //             column: x => x.RoleId,
            //             principalTable: "AspNetRoles",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUserClaims",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUserLogins",
            //     columns: table => new
            //     {
            //         LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUserRoles",
            //     columns: table => new
            //     {
            //         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //             column: x => x.RoleId,
            //             principalTable: "AspNetRoles",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUserTokens",
            //     columns: table => new
            //     {
            //         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CinemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HallName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatCapacity = table.Column<int>(type: "int", nullable: false),
                    CinemaFormatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halls_CinemaFormats_CinemaFormatId",
                        column: x => x.CinemaFormatId,
                        principalTable: "CinemaFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Halls_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // migrationBuilder.CreateTable(
            //     name: "MovieGenre",
            //     columns: table => new
            //     {
            //         GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_MovieGenre", x => new { x.GenresId, x.MoviesId });
            //         table.ForeignKey(
            //             name: "FK_MovieGenre_Genres_GenresId",
            //             column: x => x.GenresId,
            //             principalTable: "Genres",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_MovieGenre_Movies_MoviesId",
            //             column: x => x.MoviesId,
            //             principalTable: "Movies",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "MovieGenres",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_MovieGenres", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_MovieGenres_Genres_GenresId",
            //             column: x => x.GenresId,
            //             principalTable: "Genres",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_MovieGenres_Movies_MoviesId",
            //             column: x => x.MoviesId,
            //             principalTable: "Movies",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "MovieImages",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         Size = table.Column<int>(type: "int", nullable: false),
            //         Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //         created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_MovieImages", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_MovieImages_Movies_MovieId",
            //             column: x => x.MovieId,
            //             principalTable: "Movies",
            //             principalColumn: "Id");
            //     });

            migrationBuilder.CreateTable(
                name: "Show",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Show", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Show_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // migrationBuilder.InsertData(
            //     table: "Genres",
            //     columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
            //     values: new object[,]
            //     {
            //         { new Guid("3e412481-33bf-45eb-b7a5-2d4e50aaf219"), "Comedy", new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3960), new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3960) },
            //         { new Guid("6d0bfda8-574e-4501-b25c-1527b914df7e"), "Drama", new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3940), new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3950) },
            //         { new Guid("c8fdc6f3-52a2-43f8-97bb-8e07edd3339e"), "Sci-Fi", new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3950), new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3950) },
            //         { new Guid("e1f2d1df-f2b9-4aa3-8451-f21710198db7"), "Action", new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3870), new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3910) },
            //         { new Guid("e3732c5d-a4db-4f27-9bc2-dd69537e0fe5"), "Horror", new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3970), new DateTime(2025, 3, 7, 16, 34, 55, 458, DateTimeKind.Local).AddTicks(3970) }
            //     });

            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetRoleClaims_RoleId",
            //     table: "AspNetRoleClaims",
            //     column: "RoleId");

            // migrationBuilder.CreateIndex(
            //     name: "RoleNameIndex",
            //     table: "AspNetRoles",
            //     column: "NormalizedName",
            //     unique: true,
            //     filter: "[NormalizedName] IS NOT NULL");

            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserClaims_UserId",
            //     table: "AspNetUserClaims",
            //     column: "UserId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserLogins_UserId",
            //     table: "AspNetUserLogins",
            //     column: "UserId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserRoles_RoleId",
            //     table: "AspNetUserRoles",
            //     column: "RoleId");

            // migrationBuilder.CreateIndex(
            //     name: "EmailIndex",
            //     table: "AspNetUsers",
            //     column: "NormalizedEmail");

            // migrationBuilder.CreateIndex(
            //     name: "UserNameIndex",
            //     table: "AspNetUsers",
            //     column: "NormalizedUserName",
            //     unique: true,
            //     filter: "[NormalizedUserName] IS NOT NULL");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Halls_CinemaFormatId",
            //     table: "Halls",
            //     column: "CinemaFormatId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Halls_CinemaId",
            //     table: "Halls",
            //     column: "CinemaId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_MovieGenre_MoviesId",
            //     table: "MovieGenre",
            //     column: "MoviesId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_MovieGenres_GenresId",
            //     table: "MovieGenres",
            //     column: "GenresId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_MovieGenres_MoviesId",
            //     table: "MovieGenres",
            //     column: "MoviesId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_MovieImages_MovieId",
            //     table: "MovieImages",
            //     column: "MovieId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Show_HallId",
            //     table: "Show",
            //     column: "HallId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "MovieGenre");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "MovieImages");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Show");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropTable(
                name: "CinemaFormats");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
