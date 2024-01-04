using AppFinanceiro.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using Helpers.Animation;

namespace AppFinanceiro.Views;

public partial class HomePage : ContentPage
{
    public HomePage(HomeVM vm)
    {
        InitializeComponent();
        GridOptionTransaction.IsVisible = false;
        GridOptionTransaction2.IsVisible = false;
        BindingContext = vm;

        TabMenu tabMenu = new(ShowButton, ButtonMenuShow, ButtonDespesa, ButtonDadosDespesa, ButtonReceita,
            ButtonDadosReceita, ButtonDespesa2, ButtonDadosDespesa2, ButtonReceita2, ButtonDadosReceita2);

        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            if (msg.Equals("CloseTabMenuNavigation"))
            {
                CloseMenuToNavigation();
            }
        });
    }
    private async void OnCollectionViewScrolled(object sender, ScrolledEventArgs e)
    {
        if (e.ScrollY > 0)
        {
            await ShowButton.TranslateTo(0, 500, 500);
        }
        else
        {
            await ShowButton.TranslateTo(0, 0, 500, Easing.CubicOut);
        }
    }
    private void MenuAnimetion(object sender, EventArgs e)
    {
        GridOptionTransaction.IsVisible = true;
        GridOptionTransaction2.IsVisible = true;
        ButtonDespesa.IsVisible = true;
        ButtonDadosDespesa.IsVisible = true;
        ButtonReceita.IsVisible = true;
        ButtonDadosReceita.IsVisible = true;
        TabMenu.MenuTabAnimation();
    }
    private async void CloseMenuAnimationAsync(object sender, EventArgs e)
    {
        try
        {
            bool closeMenuAnimetionResult = await TabMenu.CloseMenuAnimetion();

            if (closeMenuAnimetionResult)
            {
                ButtonDespesa.IsVisible = false;
                ButtonDadosDespesa.IsVisible = false;
                ButtonReceita.IsVisible = false;
                ButtonDadosReceita.IsVisible = false;
                GridOptionTransaction2.IsVisible = false;
            }

            bool FinalAnimationResult = await TabMenu.FinalAnimation();

            if (FinalAnimationResult)
            {
                GridOptionTransaction.IsVisible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    private void CloseMenuToNavigation()
    {
        try
        {
            ButtonDespesa.IsVisible = false;
            ButtonDadosDespesa.IsVisible = false;
            ButtonReceita.IsVisible = false;
            ButtonDadosReceita.IsVisible = false;
            GridOptionTransaction2.IsVisible = false;
            GridOptionTransaction.IsVisible = false;

            TabMenu.CloseMenuNavigation();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    private async void TapGestureRecognizerTapped_ToDelete(object sender, TappedEventArgs e)
    {
        await Delete.DeletionAnimation((Border)sender, true);
        bool result = await DisplayAlert("Excluir!", "Tem certeza que deseja excluir?", "Sim", "Não");

        if (!result)
        {
            await Delete.DeletionAnimation((Border)sender, false);
            return;
        }
    }
}

