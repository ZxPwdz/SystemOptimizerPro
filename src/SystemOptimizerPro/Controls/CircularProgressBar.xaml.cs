using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SystemOptimizerPro.Controls;

public partial class CircularProgressBar : UserControl
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(CircularProgressBar),
            new PropertyMetadata(0.0, OnValueChanged));

    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(double), typeof(CircularProgressBar),
            new PropertyMetadata(100.0));

    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(CircularProgressBar),
            new PropertyMetadata(8.0, OnStrokeThicknessChanged));

    public static readonly DependencyProperty ProgressColorProperty =
        DependencyProperty.Register(nameof(ProgressColor), typeof(Brush), typeof(CircularProgressBar),
            new PropertyMetadata(Brushes.Blue, OnProgressColorChanged));

    public static readonly DependencyProperty ShowPercentageProperty =
        DependencyProperty.Register(nameof(ShowPercentage), typeof(bool), typeof(CircularProgressBar),
            new PropertyMetadata(true, OnShowPercentageChanged));

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    public Brush ProgressColor
    {
        get => (Brush)GetValue(ProgressColorProperty);
        set => SetValue(ProgressColorProperty, value);
    }

    public bool ShowPercentage
    {
        get => (bool)GetValue(ShowPercentageProperty);
        set => SetValue(ShowPercentageProperty, value);
    }

    public CircularProgressBar()
    {
        InitializeComponent();
        UpdateArc();
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((CircularProgressBar)d).UpdateArc();
    }

    private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CircularProgressBar)d;
        control.BackgroundCircle.StrokeThickness = (double)e.NewValue;
        control.ProgressPath.StrokeThickness = (double)e.NewValue;
    }

    private static void OnProgressColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CircularProgressBar)d;
        control.ProgressPath.Stroke = (Brush)e.NewValue;
    }

    private static void OnShowPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CircularProgressBar)d;
        control.ValueText.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateArc()
    {
        double clampedValue = Math.Clamp(Value, 0, 100);
        double angle = clampedValue / 100.0 * 360.0;

        // Update text
        ValueText.Text = $"{(int)clampedValue}%";

        if (angle <= 0)
        {
            ProgressPath.Visibility = Visibility.Collapsed;
            return;
        }

        ProgressPath.Visibility = Visibility.Visible;

        double radians = (angle - 90) * Math.PI / 180.0;
        double radius = 45;
        double centerX = 50;
        double centerY = 50;

        double endX = centerX + radius * Math.Cos(radians);
        double endY = centerY + radius * Math.Sin(radians);

        // Avoid rendering issues at exactly 360 degrees
        if (angle >= 359.9)
        {
            endX = 49.99;
            endY = 5;
        }

        PathFigure.StartPoint = new Point(50, 5);
        ArcSegment.Point = new Point(endX, endY);
        ArcSegment.IsLargeArc = angle > 180;
        ArcSegment.Size = new Size(radius, radius);
    }
}
