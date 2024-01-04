using System.Globalization;


namespace AppFinanceiro.Libraries.Converters;

public class TransactionPortfolioValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;


        decimal amout = (decimal)value;
        if (amout == 0)
        {
            return "R$ 0,00";
        }

        if (parameter == null) return amout.ToString("C2", new CultureInfo("pt-BR"));

        if (parameter.Equals("Expense"))
        {
            return $"- {amout.ToString("C2", new CultureInfo("pt-BR"))}";
        }

        return amout.ToString("C2", new CultureInfo("pt-BR"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
