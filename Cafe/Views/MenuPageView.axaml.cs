using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;
using Cafe.Models.Models;

namespace Cafe.Views;

public partial class MenuPageView : UserControl
{
    public MenuPageView()
    {
        InitializeComponent();
    }
    
    public void RemoveDish(Dish dish)
    {
        (DataContext as MenuPageViewModel).RemoveDishImpl(dish);
    }
}