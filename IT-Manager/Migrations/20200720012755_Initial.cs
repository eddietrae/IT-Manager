using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace itmanager.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Severities",
                columns: table => new
                {
                    SeverityId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Severities", x => x.SeverityId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreAlias = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Authority = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",SqlServerValueGenerationStrategy.IdentityColumn),
                    ShortDescription = table.Column<string>(maxLength: 50, nullable: false),
                    DetailedDescription = table.Column<string>(nullable: false),
                    SeverityId = table.Column<string>(nullable: false),
                    Severity = table.Column<string>(nullable: true),
                    StatusId = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StoreId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Severities",
                columns: new[] { "SeverityId", "Name" },
                values: new object[,]
                {
                    { "1", "Low" },
                    { "2", "Medium" },
                    { "3", "High" },
                    { "4", "Emergency" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { "1", "In Queue" },
                    { "2", "In Progress" },
                    { "3", "Complete" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreId", "City", "State", "StoreAlias", "StreetAddress", "Zip" },
                values: new object[,]
                {
                    { 1, "Bettendorf", "IA", "1375", "2900 Devils Glen Road", "52722" },
                    { 2, "Belton", "MO", "5035", "1307 East North Avenue", "64012" },
                    { 3, "Austin", "MN", "1889", "1307 18th Ave NW", "55912" },
                    { 4, "Rock Island", "IL", "4205", "2930 18th Avenue", "61201" },
                    { 5, "West Des Moines", "IA", "1601", "1700 Valley West Drive", "50266" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Authority", "FirstName", "LastName", "Password", "StoreId" },
                values: new object[,]
                {
                    { 5001, "Employee", "Paul", "Ford", "Password", 1 },
                    { 81112, "Admin", "Trevor", "Miller", "Password", 2 },
                    { 82345, "Employee", "Shane", "Blume", "Password", 3 },
                    { 88812, "Admin", "Trae", "Eddie", "Password", 4 },
                    { 86421, "Admin", "Hillary", "Murphy", "Password", 5 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "DetailedDescription", "EmployeeId", "Severity", "SeverityId", "ShortDescription", "Status", "StatusId", "StoreId" },
                values: new object[,]
                {
                    { 1, "First upload of a ticket to make sure we can connect to a database", 5001, null, "1", "First Ticket", null, "1", 1 },
                    { 2, "My register is broken..... mehhhhhhhhhhhhhhh", 81112, null, "2", "Second Ticket", null, "1", 2 },
                    { 3, "This register is causing us issues.", 82345, null, "3", "Third Ticket", null, "1", 3 },
                    { 4, "Nothing like pharmacy screwing up....... blah", 88812, null, "1", "Fouth Ticket", null, "1", 4 },
                    { 5, "The whole store is down", 86421, null, "4", "Fifth Ticket", null, "1", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreId",
                table: "Employees",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EmployeeId",
                table: "Tickets",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StoreId",
                table: "Tickets",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Severities");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
