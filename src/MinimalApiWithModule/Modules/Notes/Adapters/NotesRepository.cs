namespace MinimalApiWithModule.Modules.Notes.Adapters;

/// <summary>Notes repository.</summary>
public class NotesRepository : INotesRepository
{
	readonly ApplicationContext db;

	/// <summary>Initialises a new instance of the <see cref="NotesRepository"/> class.</summary>
	/// <param name="context">Application Context.</param>
	public NotesRepository(ApplicationContext context)
	{
		db = context;
	}

	/// <summary>POST/Create note.</summary>
	/// <param name="note">Note POCO.</param>
	/// <returns>List of notes.</returns>
	public async Task<TicketNote?> Create(TicketNote note)
	{
		db.TicketNotes.Add(note);
		await db.SaveChangesAsync();
		return await ReadByIdAsync(note.Id);
	}

	/// <summary>DELETE/Delete a note.</summary>
	/// <param name="note">Note POCO.</param>
	/// <returns>Number of records affected.</returns>
	public async Task<int> Delete(TicketNote note)
	{
		db.TicketNotes.Remove(note);
		return await db.SaveChangesAsync();
	}

	/// <summary>GET/Read note by unique identifier.</summary>
	/// <param name="id">Note identifier.</param>
	/// <returns>A note POCO.</returns>
	public TicketNote? ReadById(int id)
	{
		return db.TicketNotes.FirstOrDefault(e => e.Id == id);
	}

	/// <summary>GET/Read note asynchronously by unique identifier.</summary>
	/// <param name="id">Note identifier.</param>
	/// <returns>A note POCO.</returns>
	public async Task<TicketNote?> ReadByIdAsync(int id)
	{
		return await db.TicketNotes.FirstOrDefaultAsync(e => e.Id == id);
	}

	/// <summary>GET/Read notes.</summary>
	/// <returns>List of all notes.</returns>
	public async Task<List<TicketNote>?> Read()
	{
		return await db.TicketNotes.ToListAsync();
	}

	/// <summary>GET/Read notes for ticket identifier.</summary>
	/// <param name="ticketId">Ticket identifier.</param>
	public async Task<List<TicketNote>?> ReadByTicketId(int ticketId)
	{
		return await db.TicketNotes.Where(e => e.TicketId == ticketId).ToListAsync();
	}

	/// <summary>GET/Read notes for person identifier.</summary>
	/// <param name="personId">Person identifier.</param>
	/// <returns></returns>
	public async Task<List<TicketNote>?> ReadByPersonId(int personId)
	{
		return await db.TicketNotes.Where(e => e.PersonId == personId).ToListAsync();
	}

	/// <summary>Does the note exist in the repository</summary>
	/// <param name="id">Note identifier.</param>
	/// <returns>true if exists; otherwise false.</returns>
	public Task<bool> ExistsAsync(int id)
	{
		return db.TicketNotes.AnyAsync(e => e.Id == id);
	}

	/// <summary>PUT/Update note.</summary>
	/// <param name="note">Note POCO.</param>
	/// <returns>Number of records affected.</returns>
	public async Task<int> Update(TicketNote note)
	{
		db.TicketNotes.Update(note);
		return await db.SaveChangesAsync();

	}
}
