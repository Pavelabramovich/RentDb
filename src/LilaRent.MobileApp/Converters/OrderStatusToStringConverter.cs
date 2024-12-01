using LilaRent.MobileApp.Entities;
using System.Globalization;


namespace LilaRent.MobileApp.Converters;


class OrderStatusToStringConverter : IValueConverter
{
    public static string Convert(OrderStatus orderStatus) => orderStatus switch
    {
        OrderStatus.Past => "Past",
        OrderStatus.Current => "Current",
        OrderStatus.Upcoming => "Upcoming",

        _ => throw new ArgumentException("Unknown order status.")
    };


    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException
            .ThrowIfNull(value, nameof(value));

        if (value is not OrderStatus orderStatus)
            throw new ArgumentException("Value must be OrderStatus");

        return Convert(orderStatus);
    }


    public static OrderStatus ConvertBack(string orderStatus) => orderStatus switch
    {
        "Past" => OrderStatus.Upcoming,
        "Current" => OrderStatus.Current,
        "Upcoming" => OrderStatus.Upcoming,

        _ => throw new ArgumentException("Unknown order status")
    };

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException
            .ThrowIfNull(value, nameof(value));

        if (value is not string orderStatus)
            throw new ArgumentException("Value must be string");

        return ConvertBack(orderStatus);
    }
}