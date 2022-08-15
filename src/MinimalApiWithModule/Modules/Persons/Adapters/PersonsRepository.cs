namespace MinimalApiWithModule.Modules.Persons.Adapters;

/// <summary>Persons repository.</summary>
public class PersonsRepository : IPersonsRepository
{
	readonly ApplicationContext db;

	/// <summary>Initialises a new instance of the <see cref="PersonsRepository"/> class.</summary>
	/// <param name="context">Application Context.</param>
	public PersonsRepository(ApplicationContext context)
	{
		db = context;
	}

	/// <summary>POST/Create person.</summary>
	/// <param name="person">Person POCO.</param>
	/// <returns>A person POCO.</returns>
	public async Task<Person?> Create(Person person)
	{
		db.Persons.Add(person);
		await db.SaveChangesAsync();
		return await ReadByIdAsync(person.Id);
	}

	/// <summary>DELETE/Delete a person.</summary>
	/// <param name="person">Person POCO.</param>
	/// <returns>Number of records affected.</returns>
	public async Task<int> Delete(Person person)
	{
		db.Persons.Remove(person);
		return await db.SaveChangesAsync();
	}

	/// <summary>GET/Read person by unique identifier.</summary>
	/// <param name="id">Person identifier.</param>
	/// <returns>A person POCO.</returns>
	public Person? ReadById(int id)
	{
		return db.Persons.FirstOrDefault(p => p.Id == id);
	}

	/// <summary>GET/Read person asynchronously by unique identifier.</summary>
	/// <param name="id">Person identifier.</param>
	/// <returns>A person POCO.</returns>
	public Task<Person?> ReadByIdAsync(int id)
	{
		return db.Persons.FirstOrDefaultAsync(p => p.Id == id);
	}

	/// <summary>GET/Read persons.</summary>
	/// <returns>List of all persons.</returns>
	public async Task<List<Person>?> Read()
	{
		return await db.Persons.ToListAsync();
	}

	/// <summary>PUT/Update person.</summary>
	/// <param name="person">Person POCO.</param>
	/// <returns>Number of records affected.</returns>
	public async Task<int> Update(Person person)
	{
		db.Persons.Update(person);
		return await db.SaveChangesAsync();
	}

	/// <summary>Does the person exist in the repository</summary>
	/// <param name="id">Person identifier.</param>
	/// <returns>true if exists; otherwise false.</returns>
	public Task<bool> ExistsAsync(int id)
	{
		return db.Persons.AnyAsync(p => p.Id == id);
	}
}