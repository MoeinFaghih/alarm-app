using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using AlarmApp.Models;

namespace AlarmApp.Repositories
{
	public class AlarmRepository
	{
		private readonly string _connectionString;

		public AlarmRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		private IDbConnection Connection => new NpgsqlConnection(_connectionString);

		public List<Alarm> GetAllAlarms()
		{
			try
			{
				using (var db = Connection)
				{
					return db.Query<Alarm>("select * from alarm").ToList();
				}
			}
			catch (NpgsqlException ex)
			{
				return new List<Alarm>();
			}
		}
	}
}
