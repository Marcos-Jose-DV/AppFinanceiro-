using AppFinanceiro.ViewModels;
using Models.Models;

namespace AppFinanceiro.Views;

public partial class AddTransactionPage : ContentPage
{
	public AddTransactionPage(AddTransactionVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
		//Loaded += AddTransactionPageLoad;
	}

    //private void AddTransactionPageLoad(object? sender, EventArgs e)
    //{
    //    EntryFocus.Focus();
    //}
}