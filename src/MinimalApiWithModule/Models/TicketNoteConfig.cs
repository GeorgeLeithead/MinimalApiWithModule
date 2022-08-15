namespace MinimalApiWithModule.Models;

/// <summary>Ticket note POC configuration.</summary>
public class TicketNoteConfig
{
	/// <summary>Configure Ticket POCO.</summary>
	/// <param name="modelBuilder">Model builder.</param>
	public static void Configure(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<TicketNote>(
			entity =>
			{
				entity
					.HasKey(t => t.Id);
				entity
					.Property(t => t.Id)
					.ValueGeneratedOnAdd()
					.IsRequired();
				entity
					.Property(t => t.Note)
					.HasMaxLength(254);
			});
	}
}