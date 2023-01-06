namespace Application.Helpers;
public static class DateTimeExtensions
{
    public static string GetCurrentAge(this DateTime dateInThePast)
    {
        var currDate = DateTime.UtcNow;

        return currDate.Year - dateInThePast.Year <= 0 ?
            currDate.Month - dateInThePast.Month <= 0 ?
            currDate.Day - dateInThePast.Day <= 0 ?
            currDate.Hour - dateInThePast.Hour <= 0 ?
            currDate.Minute - dateInThePast.Minute <= 0 ?
            currDate.Second - dateInThePast.Second <= 0 ?
            "0 seconds"
            : $"{currDate.Second - dateInThePast.Second} seconds"
            : $"{currDate.Minute - dateInThePast.Minute} minutes"
            : $"{currDate.Hour - dateInThePast.Hour} hours"
            : $"{currDate.Day - dateInThePast.Day} days"
            : $"{currDate.Month - dateInThePast.Month} months"
            : $"{currDate.Year - dateInThePast.Year} years";
    }

}

