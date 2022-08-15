namespace MinimalApiWithModule.Models;

/// <summary>Ticket POC configuration.</summary>
public static class TicketConfig
{
	/// <summary>Configure Ticket POCO.</summary>
	/// <param name="modelBuilder">Model builder.</param>
	public static void Configure(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Ticket>(
			entity =>
			{
				entity
					.HasKey(t => t.Id);
				entity
					.Property(t => t.Id)
					.ValueGeneratedOnAdd()
					.IsRequired();
				entity
					.HasMany<TicketNote>(p => p.TicketNotes)
					.WithOne()
					.HasForeignKey("TicketId")
					.IsRequired();
			});
	}
}