using System.Globalization;
using AppFinanceiro.ViewModels;
using Models.Models;
using Models.Models.Enuns;

namespace AppFinanceiro.Libraries.Converters;

internal class CarteiraValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;

       Transaction transaction = (Transaction)value;

        if (transaction.Amout == 0)
        {
            return "R$ 0,00";
        }

        if (transaction.TransactionType == TransactionType.Income)
        {
            return transaction.Amout.ToString("C2", new CultureInfo("pt-BR"));
        }

        return $"- {transaction.Amout.ToString("C2", new CultureInfo("pt-BR"))}";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
