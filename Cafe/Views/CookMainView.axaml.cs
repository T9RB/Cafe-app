using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;

namespace Cafe.Views;

public partial class CookMainView : Window
{
    public CookMainView()
    {
        InitializeComponent();
        DataContext = new CookMainViewModel();
    }
}