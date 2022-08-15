namespace MinimalApiWithModule.Modules.Tickets.EndPoints;

/// <summary>Delete ticket endpoint.</summary>
public static class Delete
{
	/// <summary>DELETE/Delete a ticket.</summary>
	/// <param name="id">Ticket identifier.</param>
	/// <param name="ticketRepository">Ticket Repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 204 No Content.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> Handler(int id, ITicketsRepository ticketRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Tickets.Delete.Handler] Delete Ticket for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		Ticket? thisTicket = await ticketRepository.ReadByIdAsync(id);
		if (thisTicket == null)
		{
			logger.LogError("[Modules.Tickets.Delete.Handler] Ticket not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		await ticketRepository.Delete(thisTicket);
		logger.LogInformation("[Modules.Tickets.Delete.Handler] Deleted Ticket for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		return Results.NoContent();
	}
}
