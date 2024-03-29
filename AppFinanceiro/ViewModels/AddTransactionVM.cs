﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Models.Models;
using Repositories.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace AppFinanceiro.ViewModels;

public partial class AddTransactionVM : ObservableObject, IQueryAttributable
{
    private ITransactionRepository _repository;

    [ObservableProperty]
    string _transactionType;
    [ObservableProperty]
    Transaction _transaction;
    [ObservableProperty]
    string[] _categoryColors;
    [ObservableProperty]
    List<TitleValue> _colorListView;
    public ObservableCollection<TitleValue> Items { get; set; } = new ObservableCollection<TitleValue>();
    [ObservableProperty]
    bool _isLoading;
    [ObservableProperty]
    private TitleValue _currentItem;
    [ObservableProperty]
    bool _isDisplayPicker;

    [ObservableProperty]
    bool _checkPaymentType;
    [ObservableProperty]
    DateTime _creationDate;
    [ObservableProperty]
    string _name;
    [ObservableProperty]
    string _description;
    [ObservableProperty]
    string _amout;

    [ObservableProperty]
    string _titleColor = "Default", _pickertColor = "Default";

    public ICommand SaveTransactionCommand
        => new Command(SaveTransaction);

   
    public AddTransactionVM(ITransactionRepository repository)
    {
        _repository = repository;
        CreationDate = DateTime.UtcNow;
        WeakReferenceMessenger.Default.Register<TransactionMessage>(this, (e, msg) =>
        {
            if (msg.Message.Equals("CurrentItem"))
            {
                PickertColor = CurrentItem.BoxColor;
            }
        });
    }
    private async void SaveTransaction()
    {
        try
        {
            if(string.IsNullOrEmpty(Amout) && string.IsNullOrEmpty(Name))
            {
                await App.Current.MainPage.DisplayAlert("Por favor,", "Preencha a categoria e o valor.", "Cancelar","Ok");

                return;
            }

            Transaction = new Transaction();
            Transaction.PaymentType = new PaymentType();
            Transaction.TitleValue = new TitleValue();
            Transaction.Amout = Convert.ToDecimal(Amout);
            Transaction.PaymentType.Check = CheckPaymentType;
            Transaction.PaymentType.Type = TransactionType;
            Transaction.CreatedDate = CreationDate;
            Transaction.Name = Name;
            Transaction.Description = Description;
            Transaction.TitleValue = CurrentItem;

            _repository.Post(Transaction);
            WeakReferenceMessenger.Default.Send<TransactionMessage>(new TransactionMessage { Message = "Update" });
            await Shell.Current.GoToAsync("..");
            CleanForm();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    private void CleanForm()
    {
        Transaction = new Transaction();
        Amout = string.Empty;
        CheckPaymentType = false;
        TransactionType = string.Empty;
        CreationDate = DateTime.UtcNow;
        Name = string.Empty;
        Description = string.Empty;
        TitleColor = "Default";
        PickertColor = "Default";
        CurrentItem = new();
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var type = query["name"];
        TransactionType = type.ToString();
        LoadData();


        var newThread = new System.Threading.Thread(() =>
        {
            Application.Current.Dispatcher.Dispatch(() =>
            {
                WeakReferenceMessenger.Default.Send<TransactionMessage>(new TransactionMessage { Message = "CloseTabMenuNavigation" });
            });
        });
        newThread.Start();
    }
    private void LoadData()
    {

        if (TransactionType == Models.Models.Enuns.TransactionType.Income.ToString())
        {
            TitleColor = "Green";
            PickertColor = "#48b050";
            return;
        }
        PickertColor = "#f44236";
        TitleColor = "Red";
    }
    [RelayCommand]
    public async void OpenPicker()
    {
        IsLoading = true;
        // wait till api response is return

        //await Task.Delay(1000);

        Items.Clear();
        TitleValue titleValue = new();
        foreach (var value in titleValue.GetColorPikers())
        {
            Items.Add(value);
        }
        IsLoading = false;
        IsDisplayPicker = true;
    }
}
