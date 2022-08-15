
namespace MinimalApiWithModuleTests;

using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using MinimalApiWithModule.Models;
using MinimalApiWithModuleTests.Fixtures;
using Xunit;

/// <summary>Seed the Persons table.</summary>
public static class Seed_Persons
{
	/// <summary>Person test data.</summary>
	public static List<Person> DataPerson =>
		new()
		{
			new Person { Forename = "Able", Surname = "Baker", IsAdmin = true },
			new Person { Forename = "Charlie", Surname = "Daniels", IsAdmin = false }
		};
}

[Collection("Context Collection")]
/// <summary>Tests for Person.</summary>
public class PersonsTests
{
	readonly ApplicationFixture appFixture;

	/// <summary>Initialises a new instance of the <see cref="PersonsTests"/> class.</summary>
	/// <param name="fixture">Class fixture.</param>
	public PersonsTests(ApplicationFixture fixture)
	{
		appFixture = fixture;
	}

	#region POST/Add

	/// <summary>Test for valid POST/Create Person.</summary>
	[Fact]
	public async Task Add_Valid_WhenCalled_Ok()
	{
		// Arrange
		StringContent addPerson = new(JsonSerializer.Serialize(Seed_Persons.DataPerson[0]), Encoding.UTF8, "application/json");

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.PostAsJsonAsync("/person", addPerson);

		// Assert
		Assert.Equal(HttpStatusCode.Created, response.StatusCode);
	}

	#endregion

	#region DELETE/Delete

	/// <summary>Test for valid DELETE/Delete person.</summary>
	[Fact]
	public async Task Delete_Valid_NoContent()
	{
		// Arrange
		const int knownPersonId = 1;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.DeleteAsync($"/person/{knownPersonId}");

		// Assert
		Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
	}

	/// <summary>Test for invalid DELETE/Delete person.</summary>
	/// <remarks>Person Id not found.</remarks>
	[Fact]
	public async Task Delete_InValid_NotFound()
	{
		// Arrange
		const int unknownPersonId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.DeleteAsync($"/person/{unknownPersonId}");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	#endregion

	#region GET/Read

	/// <summary>Test for valid GET/Get all persons.</summary>
	[Fact]
	public async Task GetAll_Valid_Ok()
	{
		// Arrange

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync("/person");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<List<Person>>(await JsonSerializer.DeserializeAsync<List<Person>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	/// <summary>Test for valid GET/Get person by id.</summary>
	[Fact]
	public async Task GetById_Valid_Ok()
	{
		// Arrange
		const int knownPersonId = 2;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/person/{knownPersonId}");
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsAssignableFrom<Person>(await JsonSerializer.DeserializeAsync<Person>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
	}

	/// <summary>Test for invalid GET/Get person by id.</summary>
	/// <remarks>Person Id not found.</remarks>
	[Fact]
	public async Task GetById_InValid_NotFound()
	{
		// Arrange
		const int unknownPersonId = 99;

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.GetAsync($"/person/{unknownPersonId}");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	#endregion

	#region PUT/Update

	/// <summary>Test for valid PUT/Update person.</summary>
	[Fact]
	public async Task Update_Valid_Ok()
	{
		// Arrange
		const int knownPersonId = 2;
		Person updatePerson = new() { Forename = "Tiger", Surname = "Woods", IsAdmin = false };
		StringContent updatePersonJson = new(JsonSerializer.Serialize(updatePerson), Encoding.UTF8, "application/json");

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.PutAsync($"/person/{knownPersonId}", updatePersonJson);
		Stream stream = await response.Content.ReadAsStreamAsync();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Person? updatedPerson = Assert.IsAssignableFrom<Person>(await JsonSerializer.DeserializeAsync<Person>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
		Assert.Equal(updatePerson.Forename, updatedPerson.Forename);
		Assert.Equal(updatePerson.Surname, updatedPerson.Surname);
		Assert.Equal(updatePerson.IsAdmin, updatedPerson.IsAdmin);
	}

	/// <summary>Test for invalid PUT/Update person.</summary>
	/// <remarks>Person Id not found.</remarks>
	[Fact]
	public async Task Update_InValid_PersonId_NotFound()
	{
		// Arrange
		const int unknownPersonId = 99;
		Person updatePerson = new() { Forename = "Tiger", Surname = "Woods", IsAdmin = false };
		StringContent updatePersonJson = new(JsonSerializer.Serialize(updatePerson), Encoding.UTF8, "application/json");

		// Act
		HttpClient client = appFixture.Application.CreateClient();
		HttpResponseMessage response = await client.PutAsync($"/person/{unknownPersonId}", updatePersonJson);

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	#endregion
}