using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller3.Models
{
    public class ImperialCalendar
    {
        public ImperialCalendar ( int vday, int vyear)
        {
            if (vday > 356 || vday < 0) vday = 160;
            day = vday;
            year = vyear;
        }
        private int day;
        private int year;

        public string Date {  get { return string.Format("Imperial Date: {day:000}-{year}");  } }

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
