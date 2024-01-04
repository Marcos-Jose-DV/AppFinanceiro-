

using CommunityToolkit.Mvvm.ComponentModel;
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

    private async void SaveTransaction()
    {
        try
        {
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
            WeakReferenceMessenger.Default.Send<string>("Update");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public AddTransactionVM(ITransactionRepository repository)
    {
        _repository = repository;
        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            if (msg.Equals("CurrentItem"))
            {
                PickertColor = CurrentItem.BoxColor;
            }
        });
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
                WeakReferenceMessenger.Default.Send<string>("CloseTabMenuNavigation");
            });
        });
        newThread.Start();
    }
    private void LoadData()
    {
        try
        {
            if (TransactionType == null) { }
            TitleColor = "Green";
            if (TransactionType == Models.Models.Enuns.TransactionType.Income.ToString())
            {
                PickertColor = "#48b050";
                return;
            }
            PickertColor = "#f44236";
            TitleColor = "Red";
        }
        finally
        {
            
        }
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
