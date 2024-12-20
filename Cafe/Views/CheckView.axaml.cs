using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;

namespace Cafe.Views;

public partial class CheckView : Window
{
    public CheckView()
    {
        InitializeComponent();
        DataContext = new CheckViewModel();
    }
}