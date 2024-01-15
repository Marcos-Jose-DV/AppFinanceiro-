using CommunityToolkit.Mvvm.Messaging;
using MauiPopup;
using MauiPopup.Views;
using Models.Models;
using System.Collections;

namespace AppFinanceiro.CustomPickerControl;

public partial class PickerControlView : BasePopupPage
{
    public PickerControlView(IEnumerable itemSource, DataTemplate itemTemplate, double pickerControlHeight = 200)
    {
        InitializeComponent();

        clPickerView.ItemsSource = itemSource;
        clPickerView.ItemTemplate = itemTemplate;
        grv.HeightRequest = pickerControlHeight;
    }

    private void ClPickerView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentItem = e.CurrentSelection.FirstOrDefault();
        PopupAction.ClosePopup(currentItem);

        WeakReferenceMessenger.Default.Send<TransactionMessage>(new TransactionMessage { Message = "CurrentItem" });
    }
}