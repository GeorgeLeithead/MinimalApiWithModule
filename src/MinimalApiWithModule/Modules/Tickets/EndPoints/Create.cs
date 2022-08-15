namespace MinimalApiWithModule.Modules.Tickets.EndPoints;

/// <summary>Create ticket endpoint.</summary>
public static class Create
{
	/// <summary>POST/Create ticket.</summary>
	/// <param name="ticket">Ticket to add.</param>
	/// <param name="ticketRepository">Ticket repository.</param>
	/// <param name="personsRepository">Person repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 201 Created.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> Handler(Ticket ticket, ITicketsRepository ticketRepository, IPersonsRepository personsRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Tickets.Create.Handler] Create ticket @{LogTime}", DateTimeOffset.UtcNow);
		Person? newTicketPerson = await personsRepository.ReadByIdAsync(ticket.PersonId);
		if (newTicketPerson is null)
		{
			logger.LogError("[Modules.Tickets.Create.Handler] Person not found @{LogTime}", DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		Ticket? newTicket = await ticketRepository.Create(ticket);
		if (newTicket == null)
		{
			logger.LogError("[Modules.Tickets.Create.Handler] Ticket not added @{LogTime}", DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		logger.LogInformation("[Modules.Tickets.Create.Handler] Created Ticket with id:={id} @{LogTime}", newTicket.Id, DateTimeOffset.UtcNow);
		return Results.Created("/ticket/" + newTicket.Id, newTicket);
	}
}