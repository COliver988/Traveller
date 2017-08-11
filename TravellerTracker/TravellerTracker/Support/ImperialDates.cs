namespace Traveller.Support
{
    public class ImperialDates
    {
        public ImperialDates(int day, int year)
        {
            if (day.Equals(null) )
            {
                day = 1;
                year = 1105;
            }
            Day = day; 
            Year = year;
        }

        public int Year { get; set; }
        public int Day { get; set; }

        public void addDays(int daysToAdd)
        {
            Day += daysToAdd;
            if ( Day > 365 )
            {
                Year++;
                Day -= 365;
            }
        }
    }
}
