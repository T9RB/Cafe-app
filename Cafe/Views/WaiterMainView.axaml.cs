using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;

namespace Cafe.Views;

public partial class WaiterMainView : Window
{
    public WaiterMainView()
    {
        InitializeComponent();
        DataContext = new WaiterMainViewModel();
    }
}