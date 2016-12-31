using Newtonsoft.Json;

namespace Traveller3.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ImperialCalendar
    {
        public ImperialCalendar ( int vday, int vyear)
        {
            if (vday > 356 || vday < 0) vday = 160;
            day = vday;
            year = vyear;
        }

        [JsonProperty]
        public int day;
        [JsonProperty]
        public int year;

        public string Date {  get { return string.Format("Imperial Date: {0:000}-{1}", day, year);  } }

        // add days to the date; check for year end
        public void AddDays (int days)
        {
            day += days;
            if (day > 365)
            {
                day -= 365;
                year++;
            }
        }
    }
}
