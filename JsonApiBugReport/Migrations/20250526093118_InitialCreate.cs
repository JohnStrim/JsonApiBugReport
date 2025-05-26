using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonApiBugReport.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSystemAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsApiUser = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceGroups_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceGroups_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitGroups_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitGroups_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowOrder = table.Column<bool>(type: "bit", nullable: false),
                    SupportOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenshotLinkEnabled = table.Column<int>(type: "int", nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: true),
                    UnitGroupId = table.Column<int>(type: "int", nullable: true),
                    CopyProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    IsAddon = table.Column<bool>(type: "bit", nullable: false),
                    IsBundle = table.Column<bool>(type: "bit", nullable: false),
                    IsProductGroup = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<int>(type: "int", nullable: false),
                    UniqueCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTaxable = table.Column<int>(type: "int", nullable: true),
                    ProductGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrialDuration = table.Column<int>(type: "int", nullable: true),
                    TrialNo = table.Column<int>(type: "int", nullable: true),
                    AllowsCustomEndDate = table.Column<bool>(type: "bit", nullable: true),
                    SupportsMonthlyBillingFrequency = table.Column<bool>(type: "bit", nullable: true),
                    IncludeAllProducts = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_PriceGroups_PriceGroupId",
                        column: x => x.PriceGroupId,
                        principalTable: "PriceGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Products_CopyProductId",
                        column: x => x.CopyProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Products_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_UnitGroups_UnitGroupId",
                        column: x => x.UnitGroupId,
                        principalTable: "UnitGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mnemonic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(32,10)", precision: 32, scale: 10, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_UnitGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "UnitGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceGroups_CreatedById",
                table: "PriceGroups",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PriceGroups_UpdatedById",
                table: "PriceGroups",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CopyProductId",
                table: "Products",
                column: "CopyProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                table: "Products",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PriceGroupId",
                table: "Products",
                column: "PriceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductGroupId",
                table: "Products",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitGroupId",
                table: "Products",
                column: "UnitGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdatedById",
                table: "Products",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroups_CreatedById",
                table: "UnitGroups",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroups_UpdatedById",
                table: "UnitGroups",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Units_ParentId",
                table: "Units",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "PriceGroups");

            migrationBuilder.DropTable(
                name: "UnitGroups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
