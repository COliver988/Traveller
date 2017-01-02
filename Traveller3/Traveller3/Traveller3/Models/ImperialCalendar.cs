using Newtonsoft.Json;

namespace Traveller3.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ImperialCalendar
    {
        public ImperialCalendar ( int day, int year)
        {
            if (day > 356 || day < 0) day = 160;
            Day = day;
            Year = year;
        }

        [JsonProperty]
        public int Day { get; set; }
        [JsonProperty]
        public int Year { get; set; }

        public string Date {  get { return string.Format("Imperial Date: {0:000}-{1}", Day, Year);  } }

        // add days to the date; check for year end
        public void AddDays (int days)
        {
            Day += days;
            if (Day > 365)
            {
                Day -= 365;
                Year++;
            }
        }
    }
}
