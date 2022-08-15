namespace MinimalApiWithModule.Models;

#nullable disable

/// <summary>Ticket note POCO.</summary>
public partial class TicketNote : ITicketNote
{
	/// <summary>Initialises a new instance of the <see cref="TicketNote"/> class.</summary>
	/// <remarks>Public constructor with required data</remarks>
	/// <param name="thisTicket">Ticket object.</param>
	/// <param name="thisPerson">person object.</param>
	public TicketNote(Ticket thisTicket, Person thisPerson)
	{
		if (thisTicket == null)
		{
			throw new ArgumentNullException(nameof(thisTicket));
		}

		thisTicket.TicketNotes.Add(this);

		if (thisPerson == null)
		{
			throw new ArgumentNullException(nameof(thisPerson));
		}

		thisPerson.TicketNotes.Add(this);
		Init();
	}

	/// <summary>Initialises a new instance of the <see cref="TicketNote"/> class.</summary>
	public TicketNote() => Init();

	/// <summary>Gets or the note identifier.</summary>
	[Key]
	[Required]
	[DisplayName("Note Id")]
	[Description("Note unique identifier")]
	public int Id { get; set; }

	/// <summary>Gets or sets the note for a ticket.</summary>
	[DisplayName("Note")]
	[Description("Note for a ticket.")]
	[MaxLength(254, ErrorMessage = "A note cannot be longer than 254 characters")]
	[StringLength(254, ErrorMessage = "A note cannot be longer than 254 characters")]
	[MinLength(1, ErrorMessage = "A note cannot be shorter than 1 character")]
	[Required]
	public string Note { get; set; }

	/// <summary>Gets or sets the person for the ticket note.</summary>
	[DisplayName("Person for ticket note")]
	public int PersonId { get; set; }

	///<summary>Gets or sets the ticket for the ticket note.</summary>
	[DisplayName("Ticket for ticket note")]
	public int TicketId { get; set; }

	/// <summary>
	/// Static create function (for use in LINQ queries, etc.)
	/// </summary>
	/// <param name="_ticket0"></param>
	/// <param name="_person1"></param>
	public static TicketNote Create(Ticket _ticket0, Person _person1)
	{
		return new TicketNote(_ticket0, _person1);
	}

	/// <summary>Replaces default constructor, since it's protected. Caller assumes responsibility for setting all required values before saving.</summary>
	public static TicketNote CreateTicketNoteUnsafe()
	{
		return new TicketNote();
	}

	static void Init()
	{
		// Intentionally left empty
	}
}