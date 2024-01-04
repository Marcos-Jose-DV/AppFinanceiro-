using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFinanceiro.Libraries.Converters;

public class TransactionTypeColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;
        string query = (string)value;
        return query switch
        {
            "Income" => Color.FromArgb("#48b050"),
            "Expense" => Color.FromArgb("#f44236"),
            _ => Colors.Black,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
