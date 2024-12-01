using System.Globalization;


namespace LilaRent.MobileApp.Converters;


public class FuncMultiConverter<TTarget, TParam> : IMultiValueConverter
{
    private readonly Func<object[], TParam?, CultureInfo, TTarget>? _convert;
    private readonly Func<TTarget, TParam?, CultureInfo, object[]>? _convertBack;


    public FuncMultiConverter
    (
        Func<object[], TParam?, CultureInfo, TTarget>? convert = null, 
        Func<TTarget, TParam?, CultureInfo, object[]>? convertBack = null) 
    {
        if (convert is null && convertBack is null)
            throw new ArgumentException("Convert or convertBack must be not null.");

        _convert = convert;
        _convertBack = convertBack;
    }

    public FuncMultiConverter
    (
        Func<object[], TParam?, TTarget>? convert = null, 
        Func<TTarget, TParam?, object[]>? convertBack = null)
    {
        if (convert is null && convertBack is null)
            throw new ArgumentException("Convert or convertBack must be not null.");

        if (convert is not null)
            _convert = (values, param, info) => convert(values, param);

        if (convertBack is not null)
            _convertBack = (value, param, info) => convertBack(value, param);
    }

    public FuncMultiConverter
    (
        Func<object[], TTarget>? convert = null, 
        Func<TTarget, object[]>? convertBack = null)
    {
        if (convert is null && convertBack is null)
            throw new ArgumentException("Convert or convertBack must be not null.");

        if (convert is not null)
            _convert = (values, param, info) => convert(values);

        if (convertBack is not null)
            _convertBack = (value, param, info) => convertBack(value);
    }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (_convert is null)
            throw new NotImplementedException();

        if (parameter is not null and not TParam)
            throw new ArgumentException("Invalid parameter type", nameof(parameter));

        return _convert(values, (TParam?)parameter, culture)!;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        if (_convertBack is null)
            throw new NotImplementedException();

        if (value is not TTarget)
            throw new ArgumentException("Invalid value type", nameof(value));

        if (parameter is not null and not TParam)
            throw new ArgumentException("Invalid parameter type", nameof(parameter));


        return _convertBack((TTarget)value, (TParam?)parameter, culture)!;
    }
}


public class FuncMultiConverter<TSource1, TSource2, TTarget> : FuncMultiConverter<TTarget, object?>
{
    static object[] ToObjects(ValueTuple<TSource1 , TSource2> values) => new object[] { values.Item1!, values.Item2! };

    public FuncMultiConverter
    (
        Func<(TSource1, TSource2), TTarget>? convert = null,
        Func<TTarget, (TSource1, TSource2)>? convertBack = null
    )
    : base(
        convert is null 
            ? null
            : (object[] values) => convert(((TSource1)values[0], (TSource2)values[1])), 
        
        convertBack is null
            ? null
            : (TTarget value) => ToObjects(convertBack(value)))
    { }

    public FuncMultiConverter
    (
        Func<TSource1, TSource2, TTarget>? convert = null,
        Func<TTarget, (TSource1, TSource2)>? convertBack = null
    )
    : this(
        convert is null
            ? null
            : ((TSource1, TSource2) values) => convert(values.Item1, values.Item2),

        convertBack) 
    { }
}


public class FuncMultiConverter<TSource1, TSource2, TSource3, TTarget> : FuncMultiConverter<TTarget, object?>
{
    static object[] ToObjects(ValueTuple<TSource1?, TSource2?, TSource3?> values) 
        => new object[] { values.Item1!, values.Item2!, values.Item3! };

    public FuncMultiConverter
    (
        Func<(TSource1?, TSource2?, TSource3?), TTarget>? convert = null,
        Func<TTarget, (TSource1?, TSource2?, TSource3?)>? convertBack = null
    )
    : base(
        convert is null
            ? null
            : (object[] values) => convert(((TSource1?)values[0], (TSource2?)values[1], (TSource3?)values[2])),

        convertBack is null
            ? null
            : (TTarget value) => ToObjects(convertBack(value)))
    { }

    public FuncMultiConverter
    (
        Func<TSource1?, TSource2?, TSource3?, TTarget>? convert = null,
        Func<TTarget, (TSource1?, TSource2?, TSource3?)>? convertBack = null
    )
    : this(
        convert is null
            ? null
            : ((TSource1?, TSource2?, TSource3?) values) => convert(values.Item1, values.Item2, values.Item3),

        convertBack)
    { }
}


public class FuncMultiConverter<TSource1, TSource2, TSource3, TSource4, TTarget> : FuncMultiConverter<TTarget, object?>
{
    static object[] ToObjects(ValueTuple<TSource1, TSource2, TSource3, TSource4> values) 
        => new object[] { values.Item1!, values.Item2!, values.Item3!, values.Item4! };

    public FuncMultiConverter
    (
        Func<(TSource1, TSource2, TSource3, TSource4), TTarget>? convert = null,
        Func<TTarget, (TSource1, TSource2, TSource3, TSource4)>? convertBack = null
    )
    : base(
        convert is null
            ? null
            : (object[] values) => convert(((TSource1)values[0], (TSource2)values[1], (TSource3)values[2], (TSource4)values[3])),

        convertBack is null
            ? null
            : (TTarget value) => ToObjects(convertBack(value)))
    { }

    public FuncMultiConverter
    (
        Func<TSource1, TSource2, TSource3, TSource4, TTarget>? convert = null,
        Func<TTarget, (TSource1, TSource2, TSource3, TSource4)>? convertBack = null
    )
    : this(
        convert is null
            ? null
            : ((TSource1, TSource2, TSource3, TSource4) values) => convert(values.Item1, values.Item2, values.Item3, values.Item4),

        convertBack)
    { }
}
