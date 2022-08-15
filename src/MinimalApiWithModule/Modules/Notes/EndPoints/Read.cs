namespace MinimalApiWithModule.Modules.Notes.EndPoints;

/// <summary>Read ticket notes endpoint.</summary>
public static class Read
{
	/// <summary>GET/Read all ticket notes.</summary>
	/// <param name="noteRepository">Note Repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 200 Ok.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> HandlerAll(INotesRepository noteRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Notes.Read.HandlerAll] Query All Notes @{LogTime}", DateTimeOffset.UtcNow);
		List<TicketNote>? notes = await noteRepository.Read();
		if (notes == null)
		{
			logger.LogError("[Modules.Notes.Read.HandlerAll] Notes not found @{LogTime}", DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		logger.LogInformation("[Modules.Notes.Read.HandlerAll] Queried All Notes @{LogTime}", DateTimeOffset.UtcNow);
		return Results.Ok(notes);
	}

	/// <summary>GET/Read note by unique identifier.</summary>
	/// <param name="id">Note identifier.</param>
	/// <param name="noteRepository">Note Repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 200 Ok.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> HandlerById(int id, INotesRepository noteRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Notes.Read.HandlerById] Query Note for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		TicketNote? note = await noteRepository.ReadByIdAsync(id);
		if (note == null)
		{
			logger.LogError("[Modules.Notes.Read.HandlerById] Note not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		logger.LogInformation("[Modules.Notes.Read.HandlerById] Queried Note for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		return Results.Ok(note);
	}

	/// <summary>GET/Read notes by person identifier.</summary>
	/// <param name="personId">Person identifier.</param>
	/// <param name="noteRepository">Note repository.</param>
	/// <param name="personsRepository">Person repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 200 Ok.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> HandlerByPersonId(int personId, INotesRepository noteRepository, IPersonsRepository personsRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Notes.Read.HandlerByPersonId] Query Notes for personId:={personId} @{LogTime}", personId, DateTimeOffset.UtcNow);
		Person? person = await personsRepository.ReadByIdAsync(personId);
		if (person is null)
		{
			logger.LogError("[Modules.Notes.Read.HandlerByPersonId] Person not found for Id:={personId} @{LogTime}", personId, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		List<TicketNote>? notes = await noteRepository.ReadByPersonId(personId);
		if (notes == null)
		{
			logger.LogError("[Modules.Notes.Read.HandlerByPersonId] Notes not found for personId:={personId} @{LogTime}", personId, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}
		logger.LogInformation("[Modules.Notes.Read.HandlerByPersonId] Queried Notes for personId:={personId} @{LogTime}", personId, DateTimeOffset.UtcNow);
		return Results.Ok(notes);
	}

	/// <summary>GET/Read note by ticket identifier.</summary>
	/// <param name="ticketId">Ticket identifier.</param>
	/// <param name="noteRepository">Note repository.</param>
	/// <param name="ticketRepository">Ticket repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 200 Ok.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> HandlerByTicketId(int ticketId, INotesRepository noteRepository, ITicketsRepository ticketRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Notes.Read.HandlerByTicketId] Query Notes for ticketId:={ticketId} @{LogTime}", ticketId, DateTimeOffset.UtcNow);
		Ticket? ticket = await ticketRepository.ReadByIdAsync(ticketId);
		if (ticket is null)
		{
			logger.LogError("[Modules.Notes.Read.HandlerByTicketId] Ticket not found for Id:={ticketId} @{LogTime}", ticketId, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		List<TicketNote>? notes = await noteRepository.ReadByTicketId(ticketId);
		if (notes == null)
		{
			logger.LogError("[Modules.Notes.Read.HandlerByTicketId] Notes not found for ticketId:={ticketId} @{LogTime}", ticketId, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		logger.LogInformation("[Modules.Notes.Read.HandlerByTicketId] Queried Notes for ticketId:={ticketId} @{LogTime}", ticketId, DateTimeOffset.UtcNow);
		return Results.Ok(notes);
	}
}