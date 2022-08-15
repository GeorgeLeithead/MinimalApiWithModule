namespace MinimalApiWithModuleTests;

using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using MinimalApiWithModule.Models;
using MinimalApiWithModuleTests.Fixtures;
using Xunit;

/// <summary>Seed the Tickets table.</summary>
public static class Seed_TicketNotes
{
	/// <summary>Ticket test data.</summary>
	public static List<TicketNote> DataTicketNote =>
		new()
		{
			new TicketNote { Note = "Note 1 for person 1 ticket 2", PersonId = (Seed_Persons.DataPerson.Count) - 1, TicketId = (Seed_Tickets.DataTicket.Count) - 1 },
			new TicketNote { Note = "Note 2 for person 1 ticket 2", PersonId = (Seed_Persons.DataPerson.Count) - 1, TicketId = (Seed_Tickets.DataTicket.Count) - 1 },
			new TicketNote { Note = "Note One for person 2 ticket 2", PersonId = (Seed_Persons.DataPerson.Count), TicketId = (Seed_Tickets.DataTicket.Count) - 1 },
			new TicketNote { Note = "Note Two for person 2 ticket 1", PersonId = (Seed_Persons.DataPerson.Count), TicketId = (Seed_Tickets.DataTicket.Count) - 2 },
		};
}

[Collection("Context Collection")]
public class TicketNotesTests
{
	readonly ApplicationFixture appFixture;

	/// <summary>Initialises a new instance of the <see cref="TicketNotesTests"/> class.</summary>
	/// <param name="fixture">Class fixture.</param>
	public TicketNotesTests(ApplicationFixture fixture)
	{
		appFixture = fixture;
	}

	#region POST/Add

	/// <summary>Test for invalid POST/Create Ticket Note.</summary>
	/// <remarks>Person Id not found.</remarks>
	[Fact]
	public async Task Add_InValid_PersonId_NotFound()
	{
		// Arrange
		const int unknownPersonId = 99;
		const int knownTicketId = 1;
		TicketNote newTicketNote = new() { Note = "An brand new ticket note", PersonId = unknownPersonId, TicketId = knownTicketId };

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage responsePerson = await client.GetAsync($"/person/{unknownPersonId}");
		HttpResponseMessage responseTicket = await client.GetAsync($"/ticket/{knownTicketId}");
		HttpResponseMessage response = await client.PostAsJsonAsync<TicketNote>("/ticket/note", newTicketNote);

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, responsePerson.StatusCode);
		Assert.Equal(HttpStatusCode.OK, responseTicket.StatusCode);
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for invalid POST/Create Ticket Note.</summary>
	/// <remarks>Ticket Id not found.</remarks>
	[Fact]
	public async Task Add_InValid_TicketId_NotFound()
	{
		// Arrange
		const int knownPersonId = 2;
		const int unknownTicketId = 99;
		TicketNote newTicketNote = new() { Note = "An brand new ticket note", PersonId = knownPersonId, TicketId = unknownTicketId };

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage responsePerson = await client.GetAsync($"/person/{knownPersonId}");
		HttpResponseMessage responseTicket = await client.GetAsync($"/ticket/{unknownTicketId}");
		HttpResponseMessage response = await client.PostAsJsonAsync<TicketNote>("/ticket/note", newTicketNote);

		// Assert
		Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
		Assert.Equal(HttpStatusCode.NotFound, responseTicket.StatusCode);
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for valid POST/Create Ticket Note.</summary>
	[Fact]
	public async Task Add_Valid_Created()
	{
		// Arrange
		const int knownPersonId = 2;
		const int knownTicketId = 1;
		TicketNote newTicketNote = new() { Note = "An brand new ticket note", PersonId = knownPersonId, TicketId = knownTicketId };

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage responsePerson = await client.GetAsync($"/person/{knownPersonId}");
		HttpResponseMessage responseTicket = await client.GetAsync($"/ticket/{knownTicketId}");
		HttpResponseMessage response = await client.PostAsJsonAsync<TicketNote>("/ticket/note", newTicketNote);

		// Assert
		Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
		Assert.Equal(HttpStatusCode.OK, responseTicket.StatusCode);
		Assert.Equal(HttpStatusCode.Created, response.StatusCode);
	}

	#endregion POST/Add

	#region DELETE/Delete

	/// <summary>Test for invalid DELETE/Delete ticket note.</summary>
	/// <remarks>Person is not admin.</remarks>
	[Fact]
	public async Task Delete_InValid_PersonNotAdmin_Forbidden()
	{
		// Arrange
		const int knownTicketNoteIdPersonNotAdmin = 3;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.DeleteAsync($"/ticket/note/{knownTicketNoteIdPersonNotAdmin}");

		// Assert
		// TODO: Update this test to check for a Forbidden status code, when the API implements authorisation!
		////Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

		Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
	}

	/// <summary>Test for DELETE/Delete ticket note.</summary>
	/// <remarks>Ticket Note Id not found.</remarks>
	[Fact]
	public async Task Delete_InValid_TicketNoteId_NotFound()
	{
		// Arrange
		const int unknownTicketNoteId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.DeleteAsync($"/ticket/note/{unknownTicketNoteId}");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for valid DELETE/Delete ticket note.</summary>
	[Fact]
	public async Task Delete_Valid_TicketNoteId_NoContent()
	{
		// Arrange
		const int knownTicketNoteId = 1; // From seed, this ticket note has a person who IsAdmin

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.DeleteAsync($"/ticket/note/{knownTicketNoteId}");

		// Assert
		Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
	}

	#endregion DELETE/Delete

	#region GET/Read

	/// <summary>Test for valid GET/Get all ticket notes.</summary>
	[Fact]
	public async Task GetAll_Valid_Ok()
	{
		// Arrange

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync("/ticket/note");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<List<TicketNote>>(await JsonSerializer.DeserializeAsync<List<TicketNote>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	/// <summary>Test for invalid GET/Get ticket note by id.</summary>
	/// <remarks>Ticket note Id not found.</remarks>
	[Fact]
	public async Task GetById_InValid_TicketNoteId_NotFound()
	{
		// Arrange
		const int unknownTicketNoteId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/ticket/note/{unknownTicketNoteId}");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for valid GET/Get ticket note by id.</summary>
	[Fact]
	public async Task GetById_Valid_Ok()
	{
		// Arrange
		const int knownTicketNoteId = 2;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/ticket/note/{knownTicketNoteId}");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<TicketNote>(await JsonSerializer.DeserializeAsync<TicketNote>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	/// <summary>Test for invalid GET/Get ticket note by person id.</summary>
	/// <remarks>Person Id not found.</remarks>
	[Fact]
	public async Task GetByPersonId_InValid_PersonId_NotFound()
	{
		// Arrange
		const int unknownPersonId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/person/{unknownPersonId}/note");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for valid GET/Get ticket note by person id.</summary>
	[Fact]
	public async Task GetByPersonId_Valid_Ok()
	{
		// Arrange
		const int knownPersonId = 2;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/person/{knownPersonId}/note");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<List<TicketNote>>(await JsonSerializer.DeserializeAsync<List<TicketNote>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	/// <summary>Test for invalid GET/Get ticket note by ticket id.</summary>
	/// <remarks>Ticket Id not found.</remarks>
	[Fact]
	public async Task GetByTicketId_InValid_TicketId_NotFound()
	{
		// Arrange
		const int unknownTicketId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/ticket/{unknownTicketId}/note");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for valid GET/Get ticket note by ticket id.</summary>
	[Fact]
	public async Task GetByTicketId_Valid_Ok()
	{
		// Arrange
		const int knownTicketId = 2;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/ticket/{knownTicketId}/note");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<List<TicketNote>>(await JsonSerializer.DeserializeAsync<List<TicketNote>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	#endregion GET/Read

	#region PUT/Update

	/// <summary>Test for invalid PUT/Update ticket note.</summary>
	/// <remarks>Person Id not found.</remarks>
	[Fact]
	public async Task Update_InValid_PersonId_NotFound()
	{
		// Arrange
		const int knownTicketNoteId = 2;
		const int unknownPersonId = 99;
		const int knownTicketId = 2;
		TicketNote updateTicketNote = new() { Note = "Updated note", PersonId = unknownPersonId, TicketId = knownTicketId };

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage responsePerson = await client.GetAsync($"/person/{unknownPersonId}");
		HttpResponseMessage responseTicket = await client.GetAsync($"/ticket/{knownTicketId}");
		HttpResponseMessage response = await client.PutAsJsonAsync<TicketNote>($"/ticket/note/{knownTicketNoteId}", updateTicketNote);

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, responsePerson.StatusCode);
		Assert.Equal(HttpStatusCode.OK, responseTicket.StatusCode);
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for invalid PUT/Update ticket note.</summary>
	/// <remarks>Ticket Id not found.</remarks>
	[Fact]
	public async Task Update_InValid_TicketId_NotFound()
	{
		// Arrange
		const int knownTicketNoteId = 2;
		const int knownPersonId = 2;
		const int unknownTicketId = 99;
		TicketNote updateTicketNote = new() { Note = "Updated note", PersonId = knownPersonId, TicketId = unknownTicketId };

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage responsePerson = await client.GetAsync($"/person/{knownPersonId}");
		HttpResponseMessage responseTicket = await client.GetAsync($"/ticket/{unknownTicketId}");
		HttpResponseMessage response = await client.PutAsJsonAsync<TicketNote>($"/ticket/note/{knownTicketNoteId}", updateTicketNote);

		// Assert
		Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
		Assert.Equal(HttpStatusCode.NotFound, responseTicket.StatusCode);
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for PUT/Update ticket note.</summary>
	/// <remarks>Ticket Note Id not found.</remarks>
	[Fact]
	public async Task Update_InValid_TicketNoteId_NotFound()
	{
		// Arrange
		int unknownTicketNoteId = Seed_TicketNotes.DataTicketNote.Count + 99;
		TicketNote updateTicketNote = new() { Note = "Updated note", PersonId = 0, TicketId = 0 };
		StringContent? putContent = new(JsonSerializer.Serialize(updateTicketNote), Encoding.UTF8, "application/json");

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.PutAsync($"/ticket/note/{unknownTicketNoteId}", putContent);

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for valid PUT/Update ticket note.</summary>
	[Fact]
	public async Task Update_Valid_Ok()
	{
		// Arrange
		const int knownTicketNoteId = 2;
		const int knownPersonId = 2;
		const int knownTicketId = 2;
		TicketNote updateTicketNote = new() { Note = "Updated note", PersonId = knownPersonId, TicketId = knownTicketId };

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage responsePerson = await client.GetAsync($"/person/{knownPersonId}");
		HttpResponseMessage responseTicket = await client.GetAsync($"/ticket/{knownTicketId}");
		HttpResponseMessage response = await client.PutAsJsonAsync<TicketNote>($"/ticket/note/{knownTicketNoteId}", updateTicketNote);
		TicketNote? responseGet = await client.GetFromJsonAsync<TicketNote>($"/ticket/note/{knownTicketNoteId}");

		// Assert
		Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
		Assert.Equal(HttpStatusCode.OK, responseTicket.StatusCode);
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		TicketNote? model = Assert.IsAssignableFrom<TicketNote>(responseGet);
		Assert.Equal(updateTicketNote.Note, model.Note);
		Assert.Equal(knownTicketNoteId, model.Id);
		Assert.Equal(updateTicketNote.TicketId, model.TicketId);
		Assert.Equal(updateTicketNote.PersonId, model.PersonId);
	}

	#endregion PUT/Update
}