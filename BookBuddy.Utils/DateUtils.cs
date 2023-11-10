namespace BookBuddy.Utils {
    public class DateUtils {
        public static int GetDaysBetween(DateTime startDate, DateTime endDate) {
            TimeSpan timeSpan = endDate.Date - startDate.Date;
            return timeSpan.Days;
        }
    }
}