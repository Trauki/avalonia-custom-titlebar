using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaCustomTitlebar.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Avalonia Custom Titlebar";
}