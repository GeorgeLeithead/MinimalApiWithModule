namespace MinimalApiWithModule;

/// <summary>Definition for the EF application context.</summary>
public class ApplicationContext : DbContext
{
	/// <summary>Initialises a new instance of the <see cref="ApplicationContext"/> class.</summary>
	/// <param name="options">Options to be used by a DB context.</param>
	public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
	{
	}

	/// <summary>Used to query and save instances of <see cref="Person"/>.</summary>
	public virtual DbSet<Person> Persons { get; set; } = null!;

	/// <summary>Used to query and save instances of <see cref="TicketNote"/>.</summary>
	public virtual DbSet<TicketNote> TicketNotes { get; set; } = null!;

	/// <summary>Used to query and save instances of <see cref="Ticket"/>.</summary>
	public virtual DbSet<Ticket> Tickets { get; set; } = null!;

	/// <summary>Saves all changes made in this context to the database.</summary>
	/// <remarks>Record data changes for all entities (except for added entities).</remarks>
	/// <returns>The number of state entries written to the database.</returns>
	public override int SaveChanges()
	{
		//this.EnsureAutoHistory();
		return base.SaveChanges();
	}

	/// <summary>Saves all changes made in this context to the database.</summary>
	/// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
	/// <remarks>Record data changes for all entities (except for added entities).</remarks>
	/// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		//this.EnsureAutoHistory();
		return base.SaveChangesAsync(cancellationToken);
	}

	/// <summary>Further configure the model.</summary>
	/// <param name="modelBuilder">Model the defines the shape of entities, their relationships and how they map to the database.</param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// enable auto history functionality.
		//modelBuilder.EnableAutoHistory();
		PersonConfig.Configure(modelBuilder);
		TicketConfig.Configure(modelBuilder);
		TicketNoteConfig.Configure(modelBuilder);
	}
}