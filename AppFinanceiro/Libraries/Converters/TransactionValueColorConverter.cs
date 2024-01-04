using Models.Models;
using Models.Models.Enuns;
using System.Globalization;

namespace AppFinanceiro.Libraries.Converters;

internal class TransactionValueColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;

        Transaction transaction = (Transaction)value;

        if (transaction.Amout == 0)
        {
            return Color.FromArgb("#000000");
        }

        if (transaction.PaymentType.Type == "Income")
        {
            return Color.FromArgb("#48b050");
        }

        return Color.FromArgb("#f44236");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
