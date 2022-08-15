namespace MinimalApiWithModule.Modules.Persons.EndPoints;

/// <summary>Delete person endpoint.</summary>
public static class Delete
{
	/// <summary>DELETE/Delete a person.</summary>
	/// <param name="id">Person identifier.</param>
	/// <param name="personRepository">Person Repository.</param>
	/// <param name="logger">Logger.</param>
	/// <returns>Status 204 No Content.</returns>
	/// <returns>Status 404 Not Found.</returns>
	public static async Task<IResult> Handler(int id, IPersonsRepository personRepository, ILogger logger)
	{
		logger.LogInformation("[Modules.Persons.Delete.Handler] Delete Person for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		Person? thisPerson = await personRepository.ReadByIdAsync(id);
		if (thisPerson == null)
		{
			logger.LogError("[Modules.Persons.Delete.Handler] Person not found for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
			return Results.NotFound();
		}

		await personRepository.Delete(thisPerson);
		logger.LogInformation("[Modules.Persons.Delete.Handler] Deleted Person for id:={id} @{LogTime}", id, DateTimeOffset.UtcNow);
		return Results.NoContent();
	}
}
