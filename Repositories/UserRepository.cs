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
			if (_failedLogins.TryGetValue(email, out var entry) && entry.LockoutEnd > DateTime.UtcNow)
			{
				throw new Exception("Too many failed attempts. Try again later.");
			}


			using var connection = new NpgsqlConnection(_connectionString);
			return await connection.QueryFirstOrDefaultAsync<User>(
				"SELECT * FROM users WHERE email = @Email", new { Email = email });
		}

		public void RegisterFailedAttempt(string email)
		{
			if (!_failedLogins.ContainsKey(email))
				_failedLogins[email] = (1, DateTime.UtcNow);
			else
			{
				var entry = _failedLogins[email];
				_failedLogins[email] = (entry.Attempts + 1, entry.Attempts >= 3 ? DateTime.UtcNow.AddMinutes(5) : entry.LockoutEnd);
			}
		}

		public void resetLockOut(string email){
			if(_failedLogins.ContainsKey(email))
				_failedLogins.Remove(email) ;
		}
	}
}
