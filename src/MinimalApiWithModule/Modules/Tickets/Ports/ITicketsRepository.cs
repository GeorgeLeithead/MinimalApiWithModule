namespace MinimalApiWithModule.Modules.Tickets.Ports;

/// <summary>Tickets repository interface.</summary>
public interface ITicketsRepository
{
	/// <summary>POST/Add ticket.</summary>
	/// <param name="ticket">Ticket POCO.</param>
	/// <returns>List of Tickets.</returns>
	Task<Ticket?> Create(Ticket ticket);

	/// <summary>DELETE/Delete a ticket.</summary>
	/// <param name="ticket">Ticket POCO.</param>
	/// <returns>Number of records affected.</returns>
	Task<int> Delete(Ticket ticket);

	/// <summary>Does the ticket exist in the repository</summary>
	/// <param name="id">Ticket identifier.</param>
	/// <returns>true if exists; otherwise false.</returns>
	Task<bool> ExistsAsync(int id);

	/// <summary>GET/Get Tickets.</summary>
	/// <returns>List of all Tickets.</returns>
	Task<List<Ticket>?> Read();

	/// <summary>GET/Get ticket by unique identifier.</summary>
	/// <param name="id">Ticket identifier.</param>
	/// <returns>A ticket POCO.</returns>
	Ticket? ReadById(int id);

	/// <summary>GET/Get ticket asynchronously by unique identifier.</summary>
	/// <param name="id">Ticket identifier.</param>
	/// <returns>A ticket POCO.</returns>
	Task<Ticket?> ReadByIdAsync(int id);

	/// <summary>GET/Get tickets by person identifier.</summary>
	/// <param name="personId">Person identifier.</param>
	/// <returns>List of person tickets.</returns>
	Task<List<Ticket>?> ReadByPersonIdAsync(int personId);

	/// <summary>PUT/Update ticket.</summary>
	/// <param name="ticket">Ticket POCO.</param>
	/// <returns>Number of records affected.</returns>
	Task<int> Update(Ticket ticket);
}