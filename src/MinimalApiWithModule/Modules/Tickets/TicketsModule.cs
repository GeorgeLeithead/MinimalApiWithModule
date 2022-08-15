namespace MinimalApiWithModule.Modules.Tickets;

/// <summary>Tickets module.</summary>
public class TicketsModule : IModule
{
	/// <summary>Register a module.</summary>
	/// <param name="services">Service collection.</param>
	/// <returns>Service collection.</returns>
	public IServiceCollection RegisterModule(IServiceCollection services)
	{
		services.AddScoped<ITicketsRepository, TicketsRepository>();
		return services;
	}

	/// <summary>Map endpoints.</summary>
	/// <param name="endpoints">Endpoint route builder.</param>
	/// <returns>Endpoint route builder.</returns>
	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/ticket/{id}", EndPoints.Read.HandlerById)
			.Produces<ITicket>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("GetTicketById")
			.WithTags("Ticket")
			.AllowAnonymous();
		endpoints.MapGet("/person/{personId}/ticket", EndPoints.Read.HandlerByPersonId)
			.Produces<ITicket>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("GetTicketByPersonId")
			.WithTags("Ticket")
			.AllowAnonymous();
		endpoints.MapGet("/ticket", EndPoints.Read.HandlerAll)
			.Produces<IList<ITicket>>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("GetTicketAll")
			.WithTags("Ticket")
			.AllowAnonymous();
		endpoints.MapPut("/ticket/{id}", EndPoints.Update.Handler)
			.Accepts<ITicket>("application/json")
			.Produces<ITicket>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("UpdateTicket")
			.WithTags("Ticket")
			.AllowAnonymous();
		endpoints.MapPost("/ticket", EndPoints.Create.Handler)
			.Accepts<ITicket>("application/json")
			.Produces<ITicket>(StatusCodes.Status201Created)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("AddTicket")
			.WithTags("Ticket")
			.AllowAnonymous();
		endpoints.MapDelete("/ticket/{id}", EndPoints.Delete.Handler)
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("DeleteTicket")
			.WithTags("Ticket")
			.AllowAnonymous();

		return endpoints;
	}
}
