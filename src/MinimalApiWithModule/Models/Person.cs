namespace MinimalApiWithModule.Models;

/// <summary>Person POCO.</summary>
public partial class Person : IPerson
{
	static void Init()
	{
		// intentionally left empty
	}

	/// <summary>Default constructor</summary>
	public Person()
	{
		Tickets = new HashSet<Ticket>();
		TicketNotes = new HashSet<TicketNote>();
		Init();
	}

	/// <summary>Gets or sets the Forename of person.</summary>
	[DisplayName("Person Forename")]
	[Description("The persons forename (first name)")]
	public string? Forename { get; set; }

	/// <summary>Gets the Unique Person Identifier.</summary>
	[Key]
	[Required]
	[DisplayName("Person ID")]
	[Description("Unique person identifier")]
	public int Id { get; }

	/// <summary>Gets or sets a value indicating whether the person is an admin.</summary>
	[DisplayName("Is Admin?")]
	[Description("True if the person is an administrator; otherwise false")]
	public bool IsAdmin { get; set; }

	/// <summary>Gets or sets the Surname of person.</summary>
	[DisplayName("Person Surname")]
	[Description("The persons surname (family name)")]
	public string? Surname { get; set; }

	/*************************************************************************
	 * Navigation properties
	 *************************************************************************/

	/// <summary>One person has zero or more ticket notes.</summary>
	[Description("One person has zero or more ticket notes")]
	[Display(Name = "Ticket note person identifier")]
	public virtual ICollection<TicketNote> TicketNotes { get; private set; }

	/// <summary>One person has zero or more tickets.</summary>
	[Description("One person has zero or more tickets")]
	[Display(Name = "Ticket person Identifier")]
	public virtual ICollection<Ticket> Tickets { get; private set; }
}