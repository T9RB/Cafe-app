using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;

namespace Cafe.Views;

public partial class OrderInfoView : Window
{
    public OrderInfoView()
    {
        InitializeComponent();
        DataContext = new OrderInfoViewModel();
    }
}