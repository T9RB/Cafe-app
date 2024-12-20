using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;

namespace Cafe.Views;

public partial class AdminMainView : Window
{
    public AdminMainView()
    {
        InitializeComponent();
        DataContext = new AdminMainViewModel();
    }
}