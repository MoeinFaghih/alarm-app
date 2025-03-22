namespace AlarmApp.Models
{
    public class Alarm
    {
        public Guid id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Type { get; set; }
        public double SetValue { get; set; }
        public double? ResetValue { get; set; }
        public bool IsOpen => EndDate == null && ResetValue == null;
    }
}
