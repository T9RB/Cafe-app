using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;

namespace Cafe.Views;

public partial class NewOrderPageView : UserControl
{
    public NewOrderPageView()
    {
        InitializeComponent();
        DataContext = new NewOrderPageViewModel();
    }
}