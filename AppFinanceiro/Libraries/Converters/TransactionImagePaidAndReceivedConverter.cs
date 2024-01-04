using System.Globalization;

namespace AppFinanceiro.Libraries.Converters;

class TransactionImagePaidAndReceivedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;
        string title = (string)value;
        return title switch
        {
            "Income" => "income_icon.png",
            "Expense" => "expense_icon.png",
            _ => ""
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
