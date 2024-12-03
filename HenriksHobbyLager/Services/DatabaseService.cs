/* using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;

namespace HenriksHobbyLager.services;

public abstract class DatabaseService<T> where T : class
{
	protected string DatabaseType { get; }

	protected DatabaseService(IRepository<T> repository)
	{
		DatabaseType = repository switch
		{
			SQLiteRepository<T> => "SQLite",
			MongoRepository<T> => "MongoDB",
			_ => throw new InvalidOperationException("Okänd databas")
		};
	}
}

 */