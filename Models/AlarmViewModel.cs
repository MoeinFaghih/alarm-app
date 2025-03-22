using System.Collections.Generic;

namespace AlarmApp.Models
{
	public class AlarmViewModel
	{
		public IList<Alarm> Alarms { get; set; } = new List<Alarm>();
		public int OpenCount { get; set; }
		public int CloseCount { get; set; }
		public int[] DistributionByType { get; set; } = new int[4];
	}
}
