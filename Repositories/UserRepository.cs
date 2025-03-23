using System.Data;
using AlarmApp.Models;
using Dapper;
using Npgsql;

namespace AlarmApp.Repositories
{
	public class UserRepository
	{
		private readonly string _connectionString;
		private static Dictionary<string, (int Attempts, DateTime LockoutEnd)> _failedLogins = new();

		public UserRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<User?> GetUserByEmailAsync(string email)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			return await connection.QueryFirstOrDefaultAsync<User>(
				"SELECT * FROM users WHERE email = @Email", new { Email = email });
		}
	}
}
