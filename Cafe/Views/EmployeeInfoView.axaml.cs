using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;

namespace Cafe.Views;

public partial class EmployeeInfoView : Window
{
    public EmployeeInfoView()
    {
        InitializeComponent();
        DataContext = new EmployeeInfoViewModel();
    }
}