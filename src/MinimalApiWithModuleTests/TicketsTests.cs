namespace MinimalApiWithModuleTests;

using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using MinimalApiWithModule.Models;
using MinimalApiWithModuleTests.Fixtures;
using Xunit;

/// <summary>Seed the Tickets table.</summary>
public static class Seed_Tickets
{
	/// <summary>Ticket test data.</summary>
	public static List<Ticket> DataTicket =>
		new()
		{
			new Ticket { Content = "Ticket 1", PersonId = (Seed_Persons.DataPerson.Count - 1) },
			new Ticket { Content = "Ticket 2", PersonId = (Seed_Persons.DataPerson.Count) },
			new Ticket { Content = "Ticket 3", PersonId = (Seed_Persons.DataPerson.Count - 1) },
		};
}

[Collection("Context Collection")]
/// <summary>Tests for Ticket.</summary>
public class TicketsTests
{
	readonly ApplicationFixture appFixture;

	/// <summary>Initialises a new instance of the <see cref="TicketsTests"/> class.</summary>
	/// <param name="fixture">Class fixture.</param>
	public TicketsTests(ApplicationFixture fixture)
	{
		appFixture = fixture;
	}

	#region POST/Create

	/// <summary>Test for valid POST/Create Ticket.</summary>
	[Fact]
	public async Task Add_Valid_Created()
	{
		// Arrange
		const int knownPersonId = 2;
		Ticket newTicket = new() { Content = "New Ticket", PersonId = knownPersonId };

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage responsePerson = await client.GetAsync($"/person/{knownPersonId}");
		HttpResponseMessage response = await client.PostAsJsonAsync<Ticket>("/ticket", newTicket);

		// Assert
		Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
		Assert.Equal(HttpStatusCode.Created, response.StatusCode);
	}

	#endregion

	#region DELETE/Delete

	/// <summary>Test for valid DELETE/Delete ticket.</summary>
	[Fact]
	public async Task Delete_Valid_NoContent()
	{
		// Arrange
		const int knownTicketId = 1;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.DeleteAsync($"/ticket/{knownTicketId}");

		// Assert
		Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
	}

	/// <summary>Test for invalid DELETE/Delete ticket.</summary>
	/// <remarks>Ticket Id not found.</remarks>
	[Fact]
	public async Task Delete_InValid_NotFound()
	{
		// Arrange
		const int unknownTicketId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.DeleteAsync($"/ticket/{unknownTicketId}");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	#endregion

	#region Get/Read

	/// <summary>Test for valid GET/Get all tickets.</summary>
	[Fact]
	public async Task GetAll_Valid_Ok()
	{
		// Arrange

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync("/ticket");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<List<Ticket>>(await JsonSerializer.DeserializeAsync<List<Ticket>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	/// <summary>Test for valid GET/Get ticket by id.</summary>
	[Fact]
	public async Task GetById_Valid_Ok()
	{
		// Arrange
		const int knownTicketId = 2;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/ticket/{knownTicketId}");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<Ticket>(await JsonSerializer.DeserializeAsync<Ticket>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	/// <summary>Test for invalid GET/Get ticket by id.</summary>
	/// <remarks>Ticket Id not found.</remarks>
	[Fact]
	public async Task GetById_InValid_NotFound()
	{
		// Arrange
		const int unknownTicketId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/ticket/{unknownTicketId}");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for valid GET/Get ticket by person id.</summary>
	[Fact]
	public async Task GetByPersonId_Valid_Ok()
	{
		// Arrange
		const int knownPersonId = 2;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/person/{knownPersonId}/ticket");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<List<Ticket>>(await JsonSerializer.DeserializeAsync<List<Ticket>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	/// <summary>Test for invalid GET/Get ticket by person id.</summary>
	/// <remarks>Person Id not found.</remarks>
	[Fact]
	public async Task GetByPersonId_InValid_NotFound()
	{
		// Arrange
		const int unknownPersonId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/person/{unknownPersonId}/ticket");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	#endregion

	#region PUT/Update

	/// <summary>Test for valid PUT/Update ticket.</summary>
	[Fact]
	public async Task Update_Valid_Ok()
	{
		// Arrange
		const int knownTicketId = 2;
		const int knownPersonId = 2;
		Ticket updateTicket = new() { Content = "Updated ticket", PersonId = knownPersonId };
		StringContent updateTicketJson = new(JsonSerializer.Serialize(updateTicket), Encoding.UTF8, "application/json");

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage responsePerson = await client.GetAsync($"/person/{knownPersonId}");
		HttpResponseMessage response = await client.PutAsync($"/ticket/{knownTicketId}", updateTicketJson);
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, responsePerson.StatusCode);
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Ticket? updatedTicket = Assert.IsAssignableFrom<Ticket>(await JsonSerializer.DeserializeAsync<Ticket>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
		Assert.Equal(updateTicket.Content, updatedTicket.Content);
		Assert.Equal(updateTicket.PersonId, updatedTicket.PersonId);
	}

	/// <summary>Test for invalid PUT/Update ticket.</summary>
	/// <remarks>Ticket Id not found.</remarks>
	[Fact]
	public async Task Update_InValid_TicketId_NotFound()
	{
		// Arrange
		const int unknownTicketId = 99;
		const int knownPersonId = 2;
		Ticket updateTicket = new() { Content = "Updated ticket", PersonId = knownPersonId };
		StringContent updateTicketJson = new(JsonSerializer.Serialize(updateTicket), Encoding.UTF8, "application/json");

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.PutAsync($"/ticket/{unknownTicketId}", updateTicketJson);

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	/// <summary>Test for invalid PUT/Update ticket.</summary>
	/// <remarks>Person Id not found.</remarks>
	[Fact]
	public async Task Update_InValid_PersonId_NotFound()
	{
		// Arrange
		const int knownTicketId = 2;
		const int unknownPersonId = 99;
		Ticket updateTicket = new() { Content = "Updated ticket", PersonId = unknownPersonId };
		StringContent updateTicketJson = new(JsonSerializer.Serialize(updateTicket), Encoding.UTF8, "application/json");

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.PutAsync($"/ticket/{knownTicketId}", updateTicketJson);

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	#endregion
}