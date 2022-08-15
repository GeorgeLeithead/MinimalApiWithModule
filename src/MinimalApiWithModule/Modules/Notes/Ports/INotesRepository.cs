namespace MinimalApiWithModule.Modules.Notes.Ports;

/// <summary>Notes repository interface.</summary>
public interface INotesRepository
{
	/// <summary>POST/Create note.</summary>
	/// <param name="note">Note POCO.</param>
	/// <returns>List of notes.</returns>
	Task<TicketNote?> Create(TicketNote note);

	/// <summary>DELETE/Delete a note.</summary>
	/// <param name="note">Note POCO.</param>
	/// <returns>Number of records affected.</returns>
	Task<int> Delete(TicketNote note);

	/// <summary>GET/Read note by unique identifier.</summary>
	/// <param name="id">Note identifier.</param>
	/// <returns>A note POCO.</returns>
	TicketNote? ReadById(int id);

	/// <summary>GET/Read note asynchronously by unique identifier.</summary>
	/// <param name="id">Note identifier.</param>
	/// <returns>A note POCO.</returns>
	Task<TicketNote?> ReadByIdAsync(int id);

	/// <summary>GET/Read notes.</summary>
	/// <returns>List of all notes.</returns>
	Task<List<TicketNote>?> Read();

	/// <summary>GET/Read notes for ticket identifier.</summary>
	/// <param name="ticketId">Ticket identifier.</param>
	Task<List<TicketNote>?> ReadByTicketId(int ticketId);

	/// <summary>GET/Read notes for person identifier.</summary>
	/// <param name="personId">Person identifier.</param>
	Task<List<TicketNote>?> ReadByPersonId(int personId);

	/// <summary>Does the note exist in the repository</summary>
	/// <param name="id">Note identifier.</param>
	/// <returns>true if exists; otherwise false.</returns>
	Task<bool> ExistsAsync(int id);

	/// <summary>PUT/Update note.</summary>
	/// <param name="note">Note POCO.</param>
	/// <returns>Number of records affected.</returns>
	Task<int> Update(TicketNote note);
}