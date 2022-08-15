using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalApiWithModule.Migrations;

/// <summary>Initial migration.</summary>
public partial class Initial : Migration
{
	/// <summary>Upgrade migration.</summary>
	/// <param name="migrationBuilder">Migration builder.</param>
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "AutoHistory",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				RowId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
				TableName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
				Changed = table.Column<string>(type: "TEXT", nullable: true),
				Kind = table.Column<int>(type: "INTEGER", nullable: false),
				Created = table.Column<DateTime>(type: "TEXT", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AutoHistory", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Persons",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				Forename = table.Column<string>(type: "TEXT", nullable: true),
				IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
				Surname = table.Column<string>(type: "TEXT", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Persons", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Tickets",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				Content = table.Column<string>(type: "TEXT", nullable: true),
				PersonId = table.Column<int>(type: "INTEGER", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Tickets", x => x.Id);
				table.ForeignKey(
					name: "FK_Tickets_Persons_PersonId",
					column: x => x.PersonId,
					principalTable: "Persons",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "TicketNotes",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false)
					.Annotation("Sqlite:Autoincrement", true),
				Note = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false),
				PersonId = table.Column<int>(type: "INTEGER", nullable: false),
				TicketId = table.Column<int>(type: "INTEGER", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_TicketNotes", x => x.Id);
				table.ForeignKey(
					name: "FK_TicketNotes_Persons_PersonId",
					column: x => x.PersonId,
					principalTable: "Persons",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_TicketNotes_Tickets_TicketId",
					column: x => x.TicketId,
					principalTable: "Tickets",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_TicketNotes_PersonId",
			table: "TicketNotes",
			column: "PersonId");

		migrationBuilder.CreateIndex(
			name: "IX_TicketNotes_TicketId",
			table: "TicketNotes",
			column: "TicketId");

		migrationBuilder.CreateIndex(
			name: "IX_Tickets_PersonId",
			table: "Tickets",
			column: "PersonId");
	}

	/// <summary>Downgrade migration.</summary>
	/// <param name="migrationBuilder">Migration builder.</param>
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "AutoHistory");

		migrationBuilder.DropTable(
			name: "TicketNotes");

		migrationBuilder.DropTable(
			name: "Tickets");

		migrationBuilder.DropTable(
			name: "Persons");
	}
}
