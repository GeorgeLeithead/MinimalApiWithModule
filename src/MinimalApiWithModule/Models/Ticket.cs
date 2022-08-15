namespace MinimalApiWithModule.Models;

#nullable disable

/// <summary>Ticket POCO</summary>
public partial class Ticket : ITicket
{
	/// <summary>Initialises a new instance of the <see cref="Ticket"/> class.</summary>
	/// <remarks>Public constructor with required data</remarks>
	/// <param name="ticketPerson">Person object</param>
	public Ticket(Person ticketPerson)
	{
		if (ticketPerson == null)
		{
			throw new ArgumentNullException(nameof(ticketPerson));
		}

		ticketPerson.Tickets.Add(this);
		TicketNotes = new HashSet<TicketNote>();
		Init();
	}

	/// <summary>Initialises a new instance of the <see cref="Ticket"/> class.</summary>
	public Ticket()
	{
		TicketNotes = new HashSet<TicketNote>();
		Init();
	}

	/*************************************************************************
	 * Properties
	 *************************************************************************/

	/// <summary>Gets or sets the Content of a ticket.</summary>
	[DisplayName("Content of ticket")]
	[Description("The ticket contents")]
	public string Content { get; set; }

	/// <summary>Gets the Unique Ticket Identifier</summary>
	[Key]
	[Required]
	[DisplayName("Ticket Id")]
	[Description("Unique ticket identifier")]
	public int Id { get; set; }

	/// <summary>Gets or sets the person for the ticket.</summary>
	[DisplayName("Person for ticket")]
	public int PersonId { get; set; }

	/*************************************************************************
	 * Navigation properties
	 *************************************************************************/
	/// <summary>One ticket has zero or more ticket notes.</summary>
	[DisplayName("Ticket identifier")]
	[Description("One ticket has zero or more ticket notes.")]
	public virtual ICollection<TicketNote> TicketNotes { get; private set; }

	/// <summary>Static create function (for use in LINQ queries, etc.)</summary>
	/// <param name="thisPerson">Person object</param>
	public static Ticket Create(Person thisPerson)
	{
		return new Ticket(thisPerson);
	}

	/// <summary>Replaces default constructor, since it's protected. Caller assumes responsibility for setting all required values before saving.</summary>
	public static Ticket CreateTicketUnsafe()
	{
		return new Ticket();
	}

	static void Init()
	{
		// Intentionally left empty.
	}
}