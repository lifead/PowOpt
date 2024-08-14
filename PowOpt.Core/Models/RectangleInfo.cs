using ReactiveUI;

namespace PowOpt.Core.Models;

public class RectangleInfo : ReactiveObject
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public string FragmentName { get; set; }
    public int ZIndex { get; set; }

    private string _color;
    public string Color
    {
        get => _color;
        set => this.RaiseAndSetIfChanged(ref _color, value);
    }

    public RectangleInfo(double x, double y, double width, double height, string fragmentName, int zIndex)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        FragmentName = fragmentName;
        ZIndex = zIndex;
        Color = "Transparent"; // Начальный цвет
    }
}
