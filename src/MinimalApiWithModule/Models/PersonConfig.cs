namespace MinimalApiWithModule.Models;

/// <summary>Person POCO configuration.</summary>
public static class PersonConfig
{
	/// <summary>Configure Person POCO.</summary>
	/// <param name="modelBuilder">Model Builder.</param>
	public static void Configure(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Person>(
			entity =>
			{
				entity
					.HasKey(e => e.Id);
				entity
					.Property(e => e.Id)
					.ValueGeneratedOnAdd()
					.IsRequired();
				entity
					.HasMany<Ticket>(p => p.Tickets)
					.WithOne()
					.HasForeignKey("PersonId")
					.IsRequired();
				entity
					.HasMany<TicketNote>(p => p.TicketNotes)
					.WithOne()
					.HasForeignKey("PersonId")
					.IsRequired();
			});
	}
}