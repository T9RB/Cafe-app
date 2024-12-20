using System;
using System.IO;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using Cafe.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using Excel = Microsoft.Office.Interop.Excel;

namespace Cafe.ViewModels;

public class WaiterMainViewModel : ViewModelBase
{
    
    private PageViewModelBase _CurrentPage;
    public PageViewModelBase CurrentPage
    {
        get { return _CurrentPage; }
        private set { this.RaiseAndSetIfChanged(ref _CurrentPage, value); }
    }
    
    private readonly PageViewModelBase[] Pages = 
    { 
        new OrdersWaiterPageViewModel(),
        new NewOrderPageViewModel(),
        new ProfilePageViewModel(),
    };
    
    public ICommand OpenNewOrderPage { get; }
    public ICommand OpenOrdersPage { get; }
    public ICommand OpenProfilePage { get; }
    public ReactiveCommand<Window, Unit> CreateReport { get; }
    public ReactiveCommand<Window, Unit> ExitProfile { get; }
    public WaiterMainViewModel()
    {
        _CurrentPage = Pages[0];

        var canOpenNewOrderPage = this.WhenAnyValue((x => x.CurrentPage.OpenNewOrderWaiterPage));
        OpenNewOrderPage = ReactiveCommand.Create(OpenNewOrderPageImpl, canOpenNewOrderPage);
        var canOpenOrdersWaiterPage = this.WhenAnyValue((x => x.CurrentPage.OpenOrdersWaiterPage));
        OpenOrdersPage = ReactiveCommand.Create(OpenOrdersWaiterPageImpl, canOpenOrdersWaiterPage);
        var canOpenProfilePage = this.WhenAnyValue(x => x.CurrentPage.OpenProfilePage);
        OpenProfilePage = ReactiveCommand.Create(OpenProfilePageImpl, canOpenProfilePage);

        CreateReport = ReactiveCommand.Create<Window>(CreateReportImpl);
        ExitProfile = ReactiveCommand.Create<Window>(ExitProfileImpl);
    }

    private void ExitProfileImpl(Window obj)
    {
        AuthorizationView av = new AuthorizationView();
        AuthorizationViewModel.AuthUser = null;
        av.Show();
        obj.Close();
    }
    
    private void OpenOrdersWaiterPageImpl()
    {
        CurrentPage = Pages[0];
    }

    private void OpenNewOrderPageImpl()
    {
        CurrentPage = Pages[1];
    }
    
    private void OpenProfilePageImpl()
    {
        CurrentPage = Pages[2];
    }
    
    private void CreateReportImpl(Window obj)
    {
        
    }
}