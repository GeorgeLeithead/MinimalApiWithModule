namespace MinimalApiWithModule.Modules.Notes.EndPoints;

/// <summary>Create note endpoint.</summary>
public static class Create
{
	/// <summary>POST/Create note.</summary>
	/// <param name="note">Note to create.</param>
	/// <param name="noteRepository">Note repository.</param>
	/// <param name="ticketsRepository">Ticket repository.</param>
	/// <param name="personsRepository">Person repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 201 Created.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> Handler(TicketNote note, INotesRepository noteRepository, ITicketsRepository ticketsRepository, IPersonsRepository personsRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Notes.Create.Handler] Create note @{LogTime}", DateTimeOffset.UtcNow);
		Ticket? newNoteTicket = await ticketsRepository.ReadByIdAsync(note.TicketId);
		if (newNoteTicket == null)
		{
			logger.LogInformation("[Modules.Notes.Create.Handler] Ticket not found @{LogTime}", DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		Person? newNotePerson = await personsRepository.ReadByIdAsync(note.PersonId);
		if (newNotePerson == null)
		{
			logger.LogInformation("[Modules.Notes.Create.Handler] Person not found @{LogTime}", DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		TicketNote? newNote = await noteRepository.Create(note);
		if (newNote == null)
		{
			logger.LogError("[Modules.Notes.Create.Handler] Note not created @{LogTime}", DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		logger.LogInformation("[Modules.Notes.Create.Handler] Created note with id:={id} @{LogTime}", newNote.Id, DateTimeOffset.UtcNow);
		return Results.Created("/ticket/note/" + newNote.Id, newNote);
	}
}