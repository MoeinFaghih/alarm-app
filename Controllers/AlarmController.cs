using Microsoft.AspNetCore.Mvc;
using AlarmApp.Models;

using AlarmApp.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AlarmApp.Controllers
{
	[Authorize]
    public class AlarmController : Controller
    {
		private readonly AlarmRepository _alarmRepository;

		public AlarmController (AlarmRepository alarmRepository)
		{
			_alarmRepository = alarmRepository;
		}

        public IActionResult Index()
        {
			var output = new AlarmViewModel();

			//Random rand = new Random();
			//Alarm[] arr = new Alarm[10];

			/*for (int i = 0; i < 10; i++)
			{
				arr[i] = new Alarm
				{
					id = i + 1, // Assign a unique id
					StartDate = DateTime.Now.AddDays(-rand.Next(1, 100)), // Random start date in the past 1-100 days
					//EndaDate = rand.Next(0, 2) == 0 ? null : (DateTime?)DateTime.Now.AddDays(rand.Next(1, 100)), // Random EndDate (null or a future date)
					Type = rand.Next(0, 4), // Random alarm type between 1 and 4
					SetValue = rand.Next() * 100, // Random SetValue between 0 and 100
					//ResetValue = rand.Next(0, 2) == 0 ? null : (double?)rand.NextDouble() * 100 // Random ResetValue (null or a random value)
				};

				if(rand.Next(0, 2) == 0)
				{
					arr[i].EndaDate = (DateTime?)DateTime.Now.AddDays(rand.Next(1, 100));
					arr[i].ResetValue = (double?)rand.Next() * 100;
				}
			}

			foreach(Alarm alarm in arr)
			{
				output.Alarms.Add(alarm);

				if (alarm.EndaDate == null && alarm.ResetValue == null)
					output.OpenCount++;
				else
					output.CloseCount++;

				output.DistributionByType[alarm.Type]++;
			}*/

			output.Alarms = _alarmRepository.GetAllAlarms();

			foreach (var alarm in output.Alarms)
			{
				if (alarm.EndDate == null && alarm.ResetValue == null)
					output.OpenCount++;
				else
					output.CloseCount++;

				output.DistributionByType[alarm.Type]++;
			}


			return View(output);
        }
    }
}
