namespace MinimalApiWithModule.Modules.Persons.Ports;

/// <summary>Persons repository interface.</summary>
public interface IPersonsRepository
{
	/// <summary>POST/Create person.</summary>
	/// <param name="person">Person POCO.</param>
	/// <returns>List of persons.</returns>
	Task<Person?> Create(Person person);

	/// <summary>DELETE/Delete a person.</summary>
	/// <param name="person">Person POCO.</param>
	/// <returns>Number of records affected.</returns>
	Task<int> Delete(Person person);

	/// <summary>GET/Read person by unique identifier.</summary>
	/// <param name="id">Person identifier.</param>
	/// <returns>A person POCO.</returns>
	Person? ReadById(int id);

	/// <summary>GET/Read person asynchronously by unique identifier.</summary>
	/// <param name="id">Person identifier.</param>
	/// <returns>A person POCO.</returns>
	Task<Person?> ReadByIdAsync(int id);

	/// <summary>GET/Read persons.</summary>
	/// <returns>List of all persons.</returns>
	Task<List<Person>?> Read();

	/// <summary>Does the person exist in the repository</summary>
	/// <param name="id">Person identifier.</param>
	/// <returns>true if exists; otherwise false.</returns>
	Task<bool> ExistsAsync(int id);

	/// <summary>PUT/Update person.</summary>
	/// <param name="person">Person POCO.</param>
	/// <returns>Number of records affected.</returns>
	Task<int> Update(Person person);
}