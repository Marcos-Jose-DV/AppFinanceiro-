﻿using CommunityToolkit.Mvvm.ComponentModel;
using Repositories.Repositories;
using System.Windows.Input;
using Models.Models.Enuns;
using AppFinanceiro.Services;
using Models.Models;
using CommunityToolkit.Mvvm.Messaging;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore;
using System.Xml.Linq;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace AppFinanceiro.ViewModels;

public partial class HomeVM : ObservableObject
{
    private readonly ITransactionRepository _repository;
    private readonly INavigationService _navigationService;
    private string[] _img = { "olho_aberto.png", "olho_fechado.png" };



    [ObservableProperty] List<Transaction> _transactions;
    [ObservableProperty] decimal _total, _incomeTotal, _expenseTotal, _saldo;

    [ObservableProperty]
    bool _gridOptionTransaction,
        _moneyIsVisivle,
        _labelMoneyIsVisible,
        _containsTransactionVerticalStackLayout,
        _emptyIncomeVerticalStackLayout,
        _containsDespesaVerticalStackLayout,
        _emptyExpenseVerticalStackLayout;

    [ObservableProperty] string _buttonViewMoney;


    [ObservableProperty] ScrollView _onScrollViewScrolled = new();

    public ICommand MoneyIsVisibleCommand
        => new Command(MoneyVisivle);
    public ICommand AddTransactionCommand
        => new Command<string>(AddTransaction);

    public ObservableCollection<ISeries> SeriesExpense { get; set; }
    public IEnumerable<ISeries> SeriesIncome { get; set; }

    public HomeVM(ITransactionRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _navigationService = navigationService;
        WeakReferenceMessenger.Default.Register<TransactionMessage>(this, (e, msg) =>
        {
            if (msg.Message.Equals("Update"))
            {
                SavePreferences();
            }
            if (msg.Message.Equals("Remove"))
            {
                Transaction transaction = msg.Transaction;
                _repository.Delete(transaction);
                SeedData();
            }

            SeedData();
        });

        SavePreferences();
    }
    private void MoneyVisivle()
    {
        MoneyIsVisivle = !MoneyIsVisivle;
        LabelMoneyIsVisible = !LabelMoneyIsVisible;
        ButtonViewMoney = ButtonViewMoney.Equals(_img[0]) ? _img[1] : _img[0];
        Preferences.Set("MoneyIsVisible", MoneyIsVisivle);
        Preferences.Set("LabelMoneyIsVisible", LabelMoneyIsVisible);
        Preferences.Set("ButtonViewMoney", ButtonViewMoney);
    }
    private void SavePreferences()
    {
        MoneyIsVisivle = Preferences.Get("MoneyIsVisible", false);
        LabelMoneyIsVisible = Preferences.Get("LabelMoneyIsVisible", true);
        ButtonViewMoney = Preferences.Get("ButtonViewMoney", _img[0]);
        SeedData();
    }
    private void SeedData()
    {
        //_repository.DeleteAll();
        Transactions = _repository.Get();

        var expenseAmounts = Transactions
       .Where(item => item.PaymentType.Type == TransactionType.Expense.ToString())
       .Select(item => item.Amout)
       .ToArray();

        var name = Transactions
       .Where(item => item.PaymentType.Type == TransactionType.Expense.ToString())
       .Select(item => item.Name)
       .ToArray();

        int _index = 0;

        SeriesExpense = null;
        SeriesExpense = new ObservableCollection<ISeries>(expenseAmounts.AsPieSeries((value, series) =>
        {
            series.MaxRadialColumnWidth = 40;
            series.Name = name[_index++ % name.Length];
        }));


        expenseAmounts = Transactions
      .Where(item => item.PaymentType.Type == TransactionType.Income.ToString())
      .Select(item => item.Amout)
      .ToArray();

        name = Transactions
      .Where(item => item.PaymentType.Type == TransactionType.Income.ToString())
      .Select(item => item.Name)
      .ToArray();

        _index = 0;

        SeriesIncome = null;
        SeriesIncome = expenseAmounts.AsPieSeries((value, series) =>
        {
            series.MaxRadialColumnWidth = 40;
            series.Name = name[_index++ % name.Length];
        });

        CheckData();
        ConvertData();
    }
    private void CheckData()
    {
        bool income = Transactions
            .Where(x => x.PaymentType?.Type == TransactionType.Income
            .ToString())
            .ToList().Count != 0;
        bool expense = Transactions
            .Where(x => x.PaymentType?.Type == TransactionType.Expense
            .ToString())
            .ToList().Count != 0;

        ContainsTransactionVerticalStackLayout = income || expense == true;
        EmptyIncomeVerticalStackLayout = !income;
        EmptyExpenseVerticalStackLayout = !expense;
    }
    private void ConvertData()
    {
        IncomeTotal = Transactions
            .Where(x => x.PaymentType?.Type == TransactionType.Income.ToString())
            .Sum(x => x.Amout);

        ExpenseTotal = Transactions
           .Where(x => x.PaymentType?.Type == TransactionType.Expense.ToString())
           .Sum(x => x.Amout);

        Saldo = IncomeTotal - ExpenseTotal;
    }
    private async void AddTransaction(string type)
    {
        try
        {
            if (type == null)
            {
                return;
            }
            await _navigationService.GoToAsync($"AddTransactionPage?name={type}");

            var newThread = new System.Threading.Thread(() =>
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    Task.Delay(100);
                    WeakReferenceMessenger.Default.Send<TransactionMessage>(new TransactionMessage { Message = "CloseTabMenuNavigation" });
                });
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine(ex.StackTrace);
        }
    }
}
