using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ImageGalley.Data.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DisplayAvailability = table.Column<bool>(nullable: false),
                    MaximumCartQuantity = table.Column<int>(nullable: false),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    MinimumCartQuantity = table.Column<int>(nullable: false),
                    MinimumStockQuantity = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NotifyForQuantityBelow = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    SeoUrl = table.Column<string>(nullable: true),
                    SpecialPrice = table.Column<decimal>(nullable: true),
                    SpecialPriceEndDate = table.Column<DateTime>(nullable: true),
                    SpecialPriceStartDate = table.Column<DateTime>(nullable: true),
                    StockQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImageMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImageMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImageMapping_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImageMapping_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageMapping_ImageId",
                table: "ProductImageMapping",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageMapping_ProductId",
                table: "ProductImageMapping",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImageMapping");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
