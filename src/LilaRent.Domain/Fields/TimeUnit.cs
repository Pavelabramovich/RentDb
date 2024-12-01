namespace LilaRent.Domain.Fields;


public enum TimeUnit
{
    Minutes = 1,
    Hours = 60,
    Days = 24 * 60,
}


// Should be replaced with property extensions in future.
public static class TimeUnitExtension
{
    public static int GetMinutesCount(this TimeUnit @this)
    {
        return (int)@this;
    }


    public static int GetRentTimeMultiplier(this TimeUnit timeUnit) => timeUnit switch
    {
        TimeUnit.Minutes => 10,
        TimeUnit.Hours => 1,
        TimeUnit.Days => 1,

        _ => throw new NotSupportedException($"{timeUnit} value of {typeof(TimeUnit).Name} is not supported.")
    };
}
