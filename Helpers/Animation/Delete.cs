

namespace Helpers.Animation;

public class Delete
{
    private static Color _borderOriginalBackgroundColor;
    private static Image _imageOriginal = new Image();
    public async static Task DeletionAnimation(Border border, bool IsDeleteAnimation)
    {
        var img = (Image)border.Content;

        if (IsDeleteAnimation)
        {
            _borderOriginalBackgroundColor = border.BackgroundColor;
            _imageOriginal.Source = img.Source;

            await border.RotateYTo(90, 500);

            border.BackgroundColor = Colors.Red;

            img.Source = ImageSource.FromFile("delete_icon.png");

            await border.RotateYTo(180, 500);
        }
        else
        {
            await border.RotateYTo(90, 500);

            border.BackgroundColor = _borderOriginalBackgroundColor;
            img.Source = _imageOriginal.Source;

            await border.RotateYTo(0, 500);
        }
    }
}
