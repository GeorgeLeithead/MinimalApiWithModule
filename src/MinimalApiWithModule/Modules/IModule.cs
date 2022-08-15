namespace MinimalApiWithModule.Modules;

/// <summary>Interface for the automating the process of registering modules.</summary>
public interface IModule
{
	/// <summary>Register a module.</summary>
	/// <param name="services">Service collection.</param>
	/// <returns>Service collection.</returns>
	IServiceCollection RegisterModule(IServiceCollection services);

	/// <summary>Map endpoints.</summary>
	/// <param name="endpoints">Endpoint route builder.</param>
	/// <returns>Endpoint route builder.</returns>
	IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}
