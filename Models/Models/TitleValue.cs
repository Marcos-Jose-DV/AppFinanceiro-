namespace Models.Models;

public class TitleValue
{

    public string Title { get; set; }
    public string BoxColor { get; set; }
    public List<TitleValue> GetColorPikers()
    {
        var colors = new List<TitleValue>
        {
            new TitleValue { BoxColor = "#48b050", Title = "Green" },
            new TitleValue { BoxColor = "#f44236", Title = "Red" },
            new TitleValue { BoxColor = "#5e412f", Title = "Brown" },
            new TitleValue { BoxColor = "#fcebb6", Title = "Beige" },
            new TitleValue { BoxColor = "#78c0a8", Title = "Teal" },
            new TitleValue { BoxColor = "#f07818", Title = "Orange" },
            new TitleValue { BoxColor = "#f0a830", Title = "Goldenrod" },
            new TitleValue { BoxColor = "#f75e50", Title = "Coral" },
            new TitleValue { BoxColor = "#eac761", Title = "Mustard" },
            new TitleValue { BoxColor = "#3e3e3e", Title = "Dark Gray" },
            new TitleValue { BoxColor = "#6b6b6b", Title = "Gray" },
            new TitleValue { BoxColor = "#9e9e9e", Title = "Silver" },
            new TitleValue { BoxColor = "#4a90e2", Title = "Dodger Blue" },
            new TitleValue { BoxColor = "#a020f0", Title = "Purple" },
            new TitleValue { BoxColor = "#20b2aa", Title = "Light Sea Green" },
            new TitleValue { BoxColor = "#db7093", Title = "Pale Violet Red" },
            new TitleValue { BoxColor = "#4682b4", Title = "Steel Blue" },
            new TitleValue { BoxColor = "#00ff00", Title = "Lime" },
            new TitleValue { BoxColor = "#ff6347", Title = "Tomato" },
            new TitleValue { BoxColor = "#808080", Title = "Gray" },
            new TitleValue { BoxColor = "#556b2f", Title = "Dark Olive Green" },
            new TitleValue { BoxColor = "#800000", Title = "Maroon" }

        };
        return colors;
    }
}
