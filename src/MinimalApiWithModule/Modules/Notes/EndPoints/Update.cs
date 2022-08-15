namespace MinimalApiWithModule.Modules.Notes.EndPoints;

/// <summary>PUT/Update ticket note endpoint.</summary>
public static class Update
{
	/// <summary>PUT/Update a ticket note.</summary>
	/// <param name="id">Ticket note identifier.</param>
	/// <param name="ticketNote">Ticket Note object.</param>
	/// <param name="ticketNoteRepository">Ticket Notes repository.</param>
	/// <param name="ticketRepository">Ticket repository.</param>
	/// <param name="personRepository">Person Repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 200 Ok.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> Handler(int id, TicketNote ticketNote, INotesRepository ticketNoteRepository, ITicketsRepository ticketRepository, IPersonsRepository personRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Notes.UpdateTicketNote.Handler] Update Ticket Note for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		TicketNote? thisTicketNote = await ticketNoteRepository.ReadByIdAsync(id);
		if (thisTicketNote == null)
		{
			logger.LogError("[Modules.Notes.UpdateTicketNote.Handler] Ticket Note not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		Ticket? thisTicket = await ticketRepository.ReadByIdAsync(ticketNote.TicketId);
		if (thisTicket is null)
		{
			logger.LogError("[Modules.Notes.UpdateTicketNote.Handler] Ticket not found for id:={id} @{LogTime}", ticketNote.TicketId, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		Person? thisPerson = await personRepository.ReadByIdAsync(ticketNote.PersonId);
		if (thisPerson is null)
		{
			logger.LogError("[Modules.Notes.UpdateTicketNote.Handler] Person not found for id:={id} @{LogTime}", ticketNote.PersonId, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		try
		{
			thisTicketNote.Note = ticketNote.Note;
			thisTicketNote.PersonId = ticketNote.PersonId;
			thisTicketNote.TicketId = ticketNote.TicketId;
			await ticketNoteRepository.Update(thisTicketNote);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			logger.LogError("[Modules.Notes.UpdateTicketNote.Handler] Error Updating Ticket Note for id:={id} @{LogTime}. Error:= {ex}", id, DateTimeOffset.UtcNow, ex);
			if (!await ticketRepository.ExistsAsync(id))
			{
				logger.LogError("[Modules.Notes.UpdateTicketNote.Handler] Ticket Note not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
				return Results.NotFound();
			}

			throw;
		}

		logger.LogInformation("[Modules.Notes.UpdateTicketNote.Handler] Updated Ticket Note for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		return Results.Ok(thisTicketNote);
	}
}
