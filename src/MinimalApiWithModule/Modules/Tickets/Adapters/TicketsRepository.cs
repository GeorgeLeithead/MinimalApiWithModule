namespace MinimalApiWithModule.Modules.Tickets.Adapters;

/// <summary>Tickets repository.</summary>
public class TicketsRepository : ITicketsRepository
{
	readonly ApplicationContext db;

	/// <summary>Initialises a new instance of the <see cref="TicketsRepository"/> class.</summary>
	/// <param name="context">Application Context.</param>
	public TicketsRepository(ApplicationContext context)
	{
		db = context;
	}

	/// <summary>POST/Add ticket.</summary>
	/// <param name="ticket">Ticket POCO.</param>
	/// <returns>A ticket POCO.</returns>
	public async Task<Ticket?> Create(Ticket ticket)
	{
		db.Tickets.Add(ticket);
		await db.SaveChangesAsync();
		return await ReadByIdAsync(ticket.Id);
	}

	/// <summary>DELETE/Delete a ticket.</summary>
	/// <param name="ticket">Ticket POCO.</param>
	/// <returns>Number of records affected.</returns>
	public async Task<int> Delete(Ticket ticket)
	{
		db.Tickets.Remove(ticket);
		return await db.SaveChangesAsync();
	}

	/// <summary>Does the ticket exist in the repository</summary>
	/// <param name="id">Ticket identifier.</param>
	/// <returns>true if exists; otherwise false.</returns>
	public Task<bool> ExistsAsync(int id)
	{
		return db.Tickets.AnyAsync(e => e.Id == id);
	}

	/// <summary>GET/Get Tickets.</summary>
	/// <returns>List of all Tickets.</returns>
	public async Task<List<Ticket>?> Read()
	{
		return await db.Tickets.ToListAsync();
	}

	/// <summary>GET/Get ticket by unique identifier.</summary>
	/// <param name="id">Ticket identifier.</param>
	/// <returns>A ticket POCO.</returns>
	public Ticket? ReadById(int id)
	{
		return db.Tickets.FirstOrDefault(e => e.Id == id);
	}

	/// <summary>GET/Get ticket asynchronously by unique identifier.</summary>
	/// <param name="id">Ticket identifier.</param>
	/// <returns>A ticket POCO.</returns>
	public Task<Ticket?> ReadByIdAsync(int id)
	{
		return db.Tickets.FirstOrDefaultAsync(e => e.Id == id);
	}

	/// <summary>GET/Get tickets by person identifier.</summary>
	/// <param name="personId">Person identifier.</param>
	/// <returns>List of person tickets.</returns>
	public async Task<List<Ticket>?> ReadByPersonIdAsync(int personId)
	{
		return await db.Tickets.Where(e => e.PersonId == personId).ToListAsync();
	}

	/// <summary>PUT/Update ticket.</summary>
	/// <param name="ticket">Ticket POCO.</param>
	/// <returns>Number of records affected.</returns>
	public async Task<int> Update(Ticket ticket)
	{
		db.Tickets.Update(ticket);
		return await db.SaveChangesAsync();
	}
}