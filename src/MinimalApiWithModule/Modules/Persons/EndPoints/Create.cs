namespace MinimalApiWithModule.Modules.Persons.EndPoints;

/// <summary>Create person endpoint.</summary>
public static class Create
{
	/// <summary>POST/Create person.</summary>
	/// <param name="person">Person to create.</param>
	/// <param name="personRepository">Person Repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 201 Created.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> Handler(Person person, IPersonsRepository personRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Persons.Create.Handler] Create person @{LogTime}", DateTimeOffset.UtcNow);
		Person? newPerson = await personRepository.Create(person);
		if (newPerson == null)
		{
			logger.LogError("[Modules.Persons.Create.Handler] Person not created @{LogTime}", DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		logger.LogInformation("[Modules.Persons.Create.Handler] Created Person with id:={id} @{LogTime}", newPerson.Id, DateTimeOffset.UtcNow);
		return Results.Created("/person/" + newPerson.Id, newPerson);
	}
}
