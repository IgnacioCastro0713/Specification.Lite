using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestMoreDeepEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestMoreDeepEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestNestedEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestNestedEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestDeepEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    MoreDeepId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDeepEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestDeepEntities_TestMoreDeepEntities_MoreDeepId",
                        column: x => x.MoreDeepId,
                        principalTable: "TestMoreDeepEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestRelatedEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    Salary = table.Column<int>(type: "INTEGER", nullable: false),
                    NestedId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRelatedEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestRelatedEntities_TestNestedEntities_NestedId",
                        column: x => x.NestedId,
                        principalTable: "TestNestedEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestDeepEntityTestNestedEntity",
                columns: table => new
                {
                    DeepsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestNestedEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDeepEntityTestNestedEntity", x => new { x.DeepsId, x.TestNestedEntityId });
                    table.ForeignKey(
                        name: "FK_TestDeepEntityTestNestedEntity_TestDeepEntities_DeepsId",
                        column: x => x.DeepsId,
                        principalTable: "TestDeepEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestDeepEntityTestNestedEntity_TestNestedEntities_TestNestedEntityId",
                        column: x => x.TestNestedEntityId,
                        principalTable: "TestNestedEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestEntityWithRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    RelatedId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntityWithRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestEntityWithRelations_TestRelatedEntities_RelatedId",
                        column: x => x.RelatedId,
                        principalTable: "TestRelatedEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestDeepEntities_MoreDeepId",
                table: "TestDeepEntities",
                column: "MoreDeepId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDeepEntityTestNestedEntity_TestNestedEntityId",
                table: "TestDeepEntityTestNestedEntity",
                column: "TestNestedEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TestEntityWithRelations_RelatedId",
                table: "TestEntityWithRelations",
                column: "RelatedId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRelatedEntities_NestedId",
                table: "TestRelatedEntities",
                column: "NestedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestDeepEntityTestNestedEntity");

            migrationBuilder.DropTable(
                name: "TestEntities");

            migrationBuilder.DropTable(
                name: "TestEntityWithRelations");

            migrationBuilder.DropTable(
                name: "TestDeepEntities");

            migrationBuilder.DropTable(
                name: "TestRelatedEntities");

            migrationBuilder.DropTable(
                name: "TestMoreDeepEntities");

            migrationBuilder.DropTable(
                name: "TestNestedEntities");
        }
    }
}
