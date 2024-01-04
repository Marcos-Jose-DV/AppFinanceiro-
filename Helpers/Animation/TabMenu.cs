namespace Helpers.Animation;

public class TabMenu
{
    public TabMenu(Button showButton, Button buttonMenuShow, VerticalStackLayout buttonDespesa,
        VerticalStackLayout buttonDadosDespesa, VerticalStackLayout buttonReceita, VerticalStackLayout
        buttonDadosReceita, VerticalStackLayout buttonDespesa2, VerticalStackLayout buttonDadosDespesa2, VerticalStackLayout buttonReceita2, VerticalStackLayout buttonDadosReceita2)
    {
        ShowButton = showButton;
        ButtonMenuShow = buttonMenuShow;
        ButtonDespesa = buttonDespesa;
        ButtonDadosDespesa = buttonDadosDespesa;
        ButtonReceita = buttonReceita;
        ButtonDadosReceita = buttonDadosReceita;
        ButtonDespesa2 = buttonDespesa2;
        ButtonDadosDespesa2 = buttonDadosDespesa2;
        ButtonReceita2 = buttonReceita2;
        ButtonDadosReceita2 = buttonDadosReceita2;
    }

    public static Button ShowButton { get; set; }
    public static Button ButtonMenuShow { get; set; }
    public static VerticalStackLayout ButtonDespesa { get; set; }
    public static VerticalStackLayout ButtonDadosDespesa { get; set; }
    public static VerticalStackLayout ButtonReceita { get; set; }
    public static VerticalStackLayout ButtonDadosReceita { get; set; }
    public static VerticalStackLayout ButtonDespesa2 { get; set; }
    public static VerticalStackLayout ButtonDadosDespesa2 { get; set; }
    public static VerticalStackLayout ButtonReceita2 { get; set; }
    public static VerticalStackLayout ButtonDadosReceita2 { get; set; }
    public async static void MenuTabAnimation()
    {
        await Task.WhenAll
        (
             ShowButton.RelRotateTo(45, 250, Easing.CubicOut),
             ButtonMenuShow.RelRotateTo(45, 250, Easing.CubicOut)
        );

        await Task.WhenAll
        (
             ButtonDespesa.TranslateTo(50, -110, 500, Easing.CubicOut),
             ButtonDadosDespesa.TranslateTo(110, -30, 500, Easing.CubicOut),

             ButtonReceita.TranslateTo(-50, -110, 500, Easing.CubicOut),
             ButtonDadosReceita.TranslateTo(-110, -30, 500, Easing.CubicOut),

             ButtonDespesa2.TranslateTo(50, -110, 500, Easing.CubicOut),
             ButtonDadosDespesa2.TranslateTo(110, -30, 500, Easing.CubicOut),

             ButtonReceita2.TranslateTo(-50, -110, 500, Easing.CubicOut),
             ButtonDadosReceita2.TranslateTo(-110, -30, 500, Easing.CubicOut)
        );
    }
    public static async Task<bool> CloseMenuAnimetion()
    {
        await Task.WhenAll
        (
             ButtonDespesa.TranslateTo(0, 0, 150, Easing.CubicOut),
             ButtonDadosDespesa.TranslateTo(0, 0, 150, Easing.CubicOut),

             ButtonReceita.TranslateTo(0, 0, 150, Easing.CubicOut),
             ButtonDadosReceita.TranslateTo(0, 0, 150, Easing.CubicOut),

             ButtonDespesa2.TranslateTo(0, 0, 150, Easing.CubicOut),
             ButtonDadosDespesa2.TranslateTo(0, 0, 150, Easing.CubicOut),

             ButtonReceita2.TranslateTo(0, 0, 150, Easing.CubicOut),
             ButtonDadosReceita2.TranslateTo(0, 0, 150, Easing.CubicOut)
        );
        return true;
    }
    public static async Task<bool> FinalAnimation()
    {
        await Task.WhenAll
        (
            ShowButton.RelRotateTo(-45, 150, Easing.CubicOut),
            ButtonMenuShow.RelRotateTo(-45, 150, Easing.CubicOut)
        );

        return true;
    }
    public async static void CloseMenuNavigation()
    {
        await Task.WhenAll
        (
            ButtonDespesa.TranslateTo(0, 0, 0),
            ButtonDadosDespesa.TranslateTo(0, 0, 0),
            ButtonReceita.TranslateTo(0, 0, 0),
            ButtonDadosReceita.TranslateTo(0, 0, 0),
            ButtonDespesa2.TranslateTo(0, 0, 0),
            ButtonDadosDespesa2.TranslateTo(0, 0, 0),
            ButtonReceita2.TranslateTo(0, 0, 0),
            ButtonDadosReceita2.TranslateTo(0, 0, 0),
            ShowButton.RelRotateTo(-45, 150),
            ButtonMenuShow.RelRotateTo(-45, 150)
        );
    }
}
