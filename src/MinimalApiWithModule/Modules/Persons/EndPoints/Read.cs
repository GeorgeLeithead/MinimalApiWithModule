namespace MinimalApiWithModule.Modules.Persons.EndPoints;

/// <summary>Read persons endpoint.</summary>
public static class Read
{
	/// <summary>GET/Read all persons.</summary>
	/// <param name="personRepository">Person Repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 200 Ok.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> HandlerAll(IPersonsRepository personRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Persons.Read.HandlerAll] Query All Persons @{LogTime}", DateTimeOffset.UtcNow);
		List<Person>? persons = await personRepository.Read();
		if (persons == null)
		{
			logger.LogError("[Modules.Persons.Read.HandlerAll] Persons not found @{LogTime}", DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		logger.LogInformation("[Modules.Persons.Read.HandlerAll] Queried All Persons @{LogTime}", DateTimeOffset.UtcNow);
		return Results.Ok(persons);
	}

	/// <summary>GET/Read person by unique identifier.</summary>
	/// <param name="id">Person identifier.</param>
	/// <param name="personRepository">Person Repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 200 Ok.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> HandlerById(int id, IPersonsRepository personRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Persons.Read.HandlerById] Query Person for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		Person? person = await personRepository.ReadByIdAsync(id);
		if (person == null)
		{
			logger.LogError("[Modules.Persons.Read.HandlerById] Person not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		logger.LogInformation("[Modules.Persons.Read.HandlerById] Queried Person for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		return Results.Ok(person);
	}
}