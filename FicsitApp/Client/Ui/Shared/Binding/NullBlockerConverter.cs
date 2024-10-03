using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Client.Ui.Shared.Binding;

public class NullBlockerConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value ?? 0;
    }
}