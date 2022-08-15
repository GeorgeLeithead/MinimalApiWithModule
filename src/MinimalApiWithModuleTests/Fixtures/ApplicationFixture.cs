namespace MinimalApiWithModuleTests.Fixtures;

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiWithModule;
using MinimalApiWithModuleTests;

/// <summary>Fixture class.</summary>
/// <remarks><see cref="https://xunit.net/docs/shared-context#class-fixture"/>Ensure that the fixture instance will be created before any of the tests have run, and once all the tests have finished, it will clean up the fixture object by calling Dispose, if present.</remarks>
public class ApplicationFixture : IDisposable
{
	bool isDisposed;

	/// <summary>Initialises a new instance of the <see cref="ApplicationFixture"/> class.</summary>
	public ApplicationFixture()
	{
		Task<ApiWebApplicationFactory> result = CreateTestApiWebApplicationFactory();
		Application = result.Result;
	}

	public ApiWebApplicationFactory Application { get; private set; }

	/// <summary>Create test API Web Application Factory.</summary>
	/// <remarks>Ensures that:<br />
	///  - the database for the context does not exist (clean down).<br />
	///  - the database for the context exists and ensures the database schema is compatible.<br />
	///  - Seeds and saves the database.
	///  </remarks>
	/// <returns>Created factory.</returns>
	public static async Task<ApiWebApplicationFactory> CreateTestApiWebApplicationFactory()
	{
		ApiWebApplicationFactory application = new("Development");
		using (IServiceScope scope = application.Services.CreateScope())
		{
			IServiceProvider provider = scope.ServiceProvider;
			using ApplicationContext notesDbContext = provider.GetRequiredService<ApplicationContext>();
			await notesDbContext.Database.EnsureDeletedAsync();
			await notesDbContext.Database.EnsureCreatedAsync();
			await notesDbContext.AddRangeAsync(Seed_Persons.DataPerson);
			await notesDbContext.AddRangeAsync(Seed_Tickets.DataTicket);
			await notesDbContext.AddRangeAsync(Seed_TicketNotes.DataTicketNote);
			await notesDbContext.SaveChangesAsync();
		}

		// Act
		return application;
	}

	/// <inheritdoc />
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool isDisposing)
	{
		if (isDisposed)
		{
			return;
		}

		isDisposed = true;
	}
}