namespace MinimalApiWithModule.Modules.Notes.EndPoints;

/// <summary>Delete ticket note endpoint.</summary>
public static class Delete
{
	/// <summary>DELETE/Delete a ticket note.</summary>
	/// <param name="id">Ticket Note identifier.</param>
	/// <param name="ticketNoteRepository">Ticket Note Repository.</param>
	/// <param name="personRepository">Person repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 204 No Content.</returns>
	/// <returns>Status 404 Not Found.</returns>
	/// <returns>Status 403 Forbidden.</returns>
	/// <remarks>Can only delete note if Person IsAdmin.</remarks>
	public static async Task<IResult> Handler(int id, INotesRepository ticketNoteRepository, IPersonsRepository personRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Notes.Delete.Handler] Delete Ticket Note for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		TicketNote? thisTicketNote = await ticketNoteRepository.ReadByIdAsync(id);
		if (thisTicketNote == null)
		{
			logger.LogError("[Modules.Notes.Delete.Handler] Ticket Note not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		Person? deletePerson = await personRepository.ReadByIdAsync(thisTicketNote.PersonId);
		if (deletePerson is not null && !deletePerson.IsAdmin)
		{
			logger.LogError("[Modules.Notes.Delete.Handler] Person not admin for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.Forbid();
		}

		await ticketNoteRepository.Delete(thisTicketNote);
		logger.LogInformation("[Modules.Notes.Delete.Handler] Deleted Ticket Note for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		return Results.NoContent();
	}
}
