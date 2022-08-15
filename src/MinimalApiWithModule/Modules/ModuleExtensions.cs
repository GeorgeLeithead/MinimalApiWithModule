namespace MinimalApiWithModule.Modules;

/// <summary>Automatic module registration extensions class.</summary>
public static class ModuleExtensions
{
	static readonly List<IModule> registeredModules = new(); // this could also be added into the DI container

	/// <summary>Register a module.</summary>
	/// <param name="services">Service collection.</param>
	/// <returns>Service collection.</returns>
	public static IServiceCollection RegisterModules(this IServiceCollection services)
	{
		IEnumerable<IModule> modules = DiscoverModules();
		foreach (IModule? module in modules)
		{
			module.RegisterModule(services);
			registeredModules.Add(module);
		}

		return services;
	}

	/// <summary>Map endpoints.</summary>
	/// <param name="app">Web Application</param>
	/// <returns>Web application with mapped endpoints.</returns>
	public static WebApplication MapEndpoints(this WebApplication app)
	{
		foreach (IModule? module in registeredModules)
		{
			module.MapEndpoints(app);
		}
		return app;
	}

	/// <summary>Discover domain driven modules.</summary>
	/// <returns>Enumerable list of discovered modules.</returns>
	static IEnumerable<IModule> DiscoverModules()
	{
		return typeof(IModule).Assembly
			.GetTypes()
			.Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
			.Select(Activator.CreateInstance)
			.Cast<IModule>();
	}
}
