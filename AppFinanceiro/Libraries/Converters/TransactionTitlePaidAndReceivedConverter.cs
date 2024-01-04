
using System.Globalization;

namespace AppFinanceiro.Libraries.Converters;

public class TransactionTitlePaidAndReceivedConverter: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;
        string title = (string)value;
        return title switch
        {
            "Income" => "Recebido",
            "Expense" => "Pago",
            _ => ""
        };
    }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
