using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Converters;


public class FuncConverter<TSource, TTarget, TParam> : IValueConverter
{
    private readonly Func<TSource, TParam, CultureInfo, TTarget>? _convert;
    private readonly Func<TTarget, TParam, CultureInfo, TSource>? _convertBack;


    public FuncConverter(
        Func<TSource, TParam, CultureInfo, TTarget>? convert = null, 
        Func<TTarget, TParam, CultureInfo, TSource>? convertBack = null)
    {
        if (convert is null && convertBack is null)
            throw new ArgumentException("Convert or convertBack must be not null.");

        _convert = convert;
        _convertBack = convertBack;
    }


    public FuncConverter(
        Func<TSource, TParam, TTarget>? convert = null, 
        Func<TTarget, TParam, TSource>? convertBack = null)
    {
        if (convert is null && convertBack is null)
            throw new ArgumentException("Convert or convertBack must be not null.");

        if (convert is not null)
            _convert = (value, param, info) => convert(value, param);

        if (convertBack is not null)
            _convertBack = (value, param, info) => convertBack(value, param);
    }


    public FuncConverter(
        Func<TSource, TTarget>? convert = null, 
        Func<TTarget, TSource>? convertBack = null)
    {
        if (convert is null && convertBack is null)
            throw new ArgumentException("Convert or convertBack must be not null.");

        if (convert is not null)
            _convert = (value, param, info) => convert(value);

        if (convertBack is not null)
            _convertBack = (value, param, info) => convertBack(value);
    }


    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (_convert is null)
            throw new NotImplementedException();

        if (value is not TSource)
            throw new ArgumentException("Invalid value type", nameof(value));

        if (parameter is not null and not TParam)
            throw new ArgumentException("Invalid parameter type", nameof(parameter));

        return _convert((TSource)value, (TParam?)parameter!, culture)!;
    }


    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (_convertBack is null)
            throw new NotImplementedException();

        if (value is not TTarget)
            throw new ArgumentException("Invalid value type", nameof(value));

        if (parameter is not null and not TParam)
            throw new ArgumentException("Invalid parameter type", nameof(parameter));


        return _convertBack((TTarget)value, (TParam?)parameter!, culture)!;
    }
}


public class FuncConverter<TSource, TTarget> : FuncConverter<TSource, TTarget, object>
{

    public FuncConverter(
        Func<TSource?, TTarget>? convert = null, 
        Func<TTarget?, TSource>? convertBack = null
    )
        : base(convert, convertBack)
    { }
}


public class FuncConverter<TSource> : FuncConverter<TSource, object, object>
{

    public FuncConverter(
        Func<TSource?, object>? convert = null, 
        Func<object?, TSource>? convertBack = null
    )
        : base(convert, convertBack)
    { }
}


public class FuncConverter : FuncConverter<object, object, object>
{
    public FuncConverter(
        Func<object?, object>? convert = null, 
        Func<object?, object>? convertBack = null
    )
        : base(convert, convertBack)
    { }
}