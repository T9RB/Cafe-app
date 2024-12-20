using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;
using Cafe.Models.Models;

namespace Cafe.Views;

public partial class OrdersPageView : UserControl
{
    public OrdersPageView()
    {
        InitializeComponent();
    }

    public void InfoOrder(Order order)
    {
        (DataContext as OrdersPageViewModel).InfoOrderImpl(order);
    }
}