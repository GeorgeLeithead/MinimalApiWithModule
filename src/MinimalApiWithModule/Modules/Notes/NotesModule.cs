namespace MinimalApiWithModule.Modules.Notes;

/// <summary>Notes module.</summary>
public class NotesModule : IModule
{
	/// <summary>Register a module.</summary>
	/// <param name="services">Service collection.</param>
	/// <returns>Service collection.</returns>
	public IServiceCollection RegisterModule(IServiceCollection services)
	{
		services.AddScoped<INotesRepository, NotesRepository>();
		return services;
	}

	/// <summary>Map endpoints.</summary>
	/// <param name="endpoints">Endpoint route builder.</param>
	/// <returns>Endpoint route builder.</returns>
	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/ticket/note/{id}", EndPoints.Read.HandlerById)
			.Produces<ITicketNote>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("GetNotesById")
			.WithTags("TicketNotes")
			.AllowAnonymous();
		endpoints.MapGet("/ticket/note", EndPoints.Read.HandlerAll)
			.Produces<IList<ITicketNote>>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("GetNotesAll")
			.WithTags("TicketNotes")
			.AllowAnonymous();
		endpoints.MapGet("/ticket/{ticketId}/note", EndPoints.Read.HandlerByTicketId)
			.Produces<IList<ITicketNote>>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("GetNotesByTicketId")
			.WithTags("TicketNotes")
			.AllowAnonymous();
		endpoints.MapGet("/person/{personId}/note", EndPoints.Read.HandlerByPersonId)
			.Produces<IList<ITicketNote>>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("GetNotesByPersonId")
			.WithTags("TicketNotes")
			.AllowAnonymous();
		endpoints.MapPut("/ticket/note/{id}", EndPoints.Update.Handler)
			.Accepts<ITicketNote>("application/json")
			.Produces<ITicketNote>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("UpdateTicketNote")
			.WithTags("TicketNotes")
			.AllowAnonymous();
		endpoints.MapPost("/ticket/note", EndPoints.Create.Handler)
			.Accepts<ITicketNote>("application/json")
			.Produces<ITicketNote>(StatusCodes.Status201Created)
			.Produces(StatusCodes.Status404NotFound)
			.WithName("AddNote")
			.WithTags("TicketNotes")
			.AllowAnonymous();
		endpoints.MapDelete("/ticket/note/{id}", EndPoints.Delete.Handler)
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status403Forbidden)
			.WithName("DeleteTicketNote")
			.WithTags("TicketNotes")
			.AllowAnonymous();

		return endpoints;
	}
}
