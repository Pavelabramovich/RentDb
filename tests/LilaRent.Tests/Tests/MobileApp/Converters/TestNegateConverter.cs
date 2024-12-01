using LilaRent.MobileApp.Converters;
using FluentAssertions;
using System.Globalization;


namespace LilaRent.Tests.MobileApp.Converters;


public class Test_DoubleNegateConverter
{
    public static IEnumerable<object[]> TestValues => new object[][]
    {
        [1, -1],
        [2, -2],
        [-10, 10],
        [0, 0],
        [5d, -5],
        [double.NegativeInfinity, double.PositiveInfinity],
    };


    [Theory]
    [MemberData(nameof(TestValues))]
    public void Typed_convert_negate_value(double value, double expected)
    {
        double result = DoubleNegateConverter.Convert(value);


        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TestValues))]
    public void Default_convert_negate_value(double value, double expected)
    {
        var converter = new DoubleNegateConverter();


        object result = converter.Convert(value, typeof(double), null, CultureInfo.CurrentCulture);


        result.Should().Be(expected);
    }

    [Fact]
    public void Default_convert_throw_ArgumentNullException()
    {
        var converter = new DoubleNegateConverter();


        var convertAction = () => converter.Convert(null, typeof(double), null, CultureInfo.CurrentCulture);


        convertAction.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData("str")]
    [InlineData(new int[] { 1, 2, 3 })]
    [InlineData(typeof(DateTime))]
    public void Default_convert_throw_ArgumentException_on_not_double_arguments(object notDouble)
    {
        var converter = new DoubleNegateConverter();


        var action = () => converter.Convert(notDouble, typeof(double), null, CultureInfo.CurrentCulture);


        action.Should().Throw<ArgumentException>();
    }


    [Theory]
    [MemberData(nameof(TestValues))]
    public void Default_convertBack_negate_value(double value, double expected)
    {
        var converter = new DoubleNegateConverter();


        object result = converter.ConvertBack(value, typeof(double), null, CultureInfo.CurrentCulture);


        result.Should().Be(expected);
    }
}