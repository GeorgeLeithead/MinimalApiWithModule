namespace MinimalApiWithModule.Models;

/// <summary>Ticket model interface.</summary>
public interface ITicket
{
	/// <summary>Gets or sets the Content of a ticket.</summary>
	string Content { get; set; }

	/// <summary>Gets the Unique Ticket Identifier</summary>
	int Id { get; set; }

	/// <summary>Gets the collection of notes for the ticket.</summary>
	ICollection<TicketNote> TicketNotes { get; }

	/// <summary>Gets or sets the person for the ticket.</summary>
	int PersonId { get; set; }
}