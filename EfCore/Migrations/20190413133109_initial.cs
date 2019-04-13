using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Script = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductNodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<int>(nullable: true),
                    LevelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductNodes_ProductLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "ProductLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductNodes_ProductNodes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BaseSection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OverrideRule = table.Column<int>(nullable: false),
                    UseParent = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ProductNodeId = table.Column<int>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    KatakanaName = table.Column<string>(nullable: true),
                    IsAllowed = table.Column<bool>(nullable: true),
                    MinAmount = table.Column<decimal>(nullable: true),
                    MaxAmount = table.Column<decimal>(nullable: true),
                    IsApplicable = table.Column<bool>(nullable: true),
                    Cost = table.Column<decimal>(nullable: true),
                    Apr = table.Column<decimal>(nullable: true),
                    InArrears = table.Column<bool>(nullable: true),
                    PaymentFrequency = table.Column<int>(nullable: true),
                    RailId = table.Column<int>(nullable: true),
                    MinTerms = table.Column<int>(nullable: true),
                    MaxTerms = table.Column<int>(nullable: true),
                    Postal_Line1 = table.Column<string>(nullable: true),
                    Postal_Line2 = table.Column<string>(nullable: true),
                    Physical_Line1 = table.Column<string>(nullable: true),
                    Physical_Line2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseSection_ProductNodes_ProductNodeId",
                        column: x => x.ProductNodeId,
                        principalTable: "ProductNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseSection_Rail_RailId",
                        column: x => x.RailId,
                        principalTable: "Rail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseSection_ProductNodeId",
                table: "BaseSection",
                column: "ProductNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSection_RailId",
                table: "BaseSection",
                column: "RailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductNodes_LevelId",
                table: "ProductNodes",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductNodes_ParentId",
                table: "ProductNodes",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseSection");

            migrationBuilder.DropTable(
                name: "ProductNodes");

            migrationBuilder.DropTable(
                name: "Rail");

            migrationBuilder.DropTable(
                name: "ProductLevels");
        }
    }
}
