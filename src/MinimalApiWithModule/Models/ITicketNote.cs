namespace MinimalApiWithModule.Models;

/// <summary>Ticket note POCO interface.</summary>
public interface ITicketNote
{
	/// <summary>Gets or the note identifier.</summary>
	int Id { get; set; }

	/// <summary>Gets or sets the note for a ticket.</summary>
	string Note { get; set; }

	/// <summary>Gets or sets the person for the ticket note.</summary>
	int PersonId { get; set; }

	///<summary>Gets or sets the ticket for the ticket note.</summary>
	int TicketId { get; set; }
}