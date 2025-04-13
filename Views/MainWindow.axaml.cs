using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia;
using Avalonia.Markup.Xaml;

namespace AvaloniaCustomTitlebar.Views;

public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();

        // Validate resources exist
        if (this.FindResource("restore_regular") is not Geometry 
            || this.FindResource("maximize_regular") is not Geometry)
        {
            throw new XamlLoadException("Missing icon resources");
        }

        // Subscribe to WindowState changes using Avalonia's property system
        WindowStateProperty.Changed.AddClassHandler<Window>(
            (sender, e) => UpdateMaximizeIcon(e));
    }

    // Update maximize icon based on window state
    private void UpdateMaximizeIcon(AvaloniaPropertyChangedEventArgs e)
    {   
        // If changed state is WindowState then update maximize icon
        if (e.Property == Window.WindowStateProperty && e.GetNewValue<WindowState>() is WindowState newState)
        {   
            if (MaximizeIcon != null)
            {
                // Set icon based on current window state
                MaximizeIcon.Data = newState == WindowState.Maximized
                    ? (Geometry)this.FindResource("restore_regular")!
                    : (Geometry)this.FindResource("maximize_regular")!;
            }
        }
    }

    // Handle window drag when the title bar is clicked
     private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }

    // Minimizes the window
    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    // Maximize the window
    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Maximized)
        {
            this.WindowState = WindowState.Normal;
        }
        else
        {
            this.WindowState = WindowState.Maximized;
        }
    }

    // Close the window
    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
