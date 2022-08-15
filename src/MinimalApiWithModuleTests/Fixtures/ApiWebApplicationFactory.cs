namespace MinimalApiWithModuleTests.Fixtures;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinimalApiWithModule;

/// <summary>API Web application factory.</summary>
public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
	readonly string environment;

	/// <summary>Initialises a new instance of the <see cref="ApiWebApplicationFactory"/> class.</summary>
	/// <param name="environment">Instance environment.</param>
	public ApiWebApplicationFactory(string environment = "Development")
	{
		this.environment = environment;
	}

	/// <summary>Create application host.</summary>
	/// <param name="builder">Host builder</param>
	/// <remarks>Remove the existing application DBContextOptions and replace with the test version.</remarks>
	/// <returns>Created host.</returns>
	protected override IHost CreateHost(IHostBuilder builder)
	{
		InMemoryDatabaseRoot root = new();
		builder.UseEnvironment(environment);

		// Add mock/test services to the builder here
		builder.ConfigureServices(services =>
		{
			ServiceDescriptor? context = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(DbContextOptions<ApplicationContext>));
			if (context != null)
			{
				services.Remove(context);
			}

			services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("Testing", root));
		});

		return base.CreateHost(builder);
	}
}