﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using Cafe.Views;
using Cafe.Models.Models;
using ReactiveUI;
using Excel = Microsoft.Office.Interop.Excel;

namespace Cafe.ViewModels;

public class AdminMainViewModel : ViewModelBase
{
    private PageViewModelBase _CurrentPage;

    private ObservableCollection<Order> _orders;

    public PageViewModelBase CurrentPage
    {
        get { return _CurrentPage; }
        private set { this.RaiseAndSetIfChanged(ref _CurrentPage, value); }
    }

    public ObservableCollection<Order> Orders
    {
        get => _orders;
        set => this.RaiseAndSetIfChanged(ref _orders, value);
    }
    
    private readonly PageViewModelBase[] Pages = 
    { 
        new MenuPageViewModel(),
        new EmployeesPageViewModel(),
        new ProfilePageViewModel(),
        new OrdersPageViewModel()
    };
    
    public ICommand OpenMenuDishPage { get; }
    public ICommand OpenEmployeesPage { get; }
    public ICommand OpenProfilePage { get; }
    public ICommand OpenOrdersPage { get; }
    
    public ReactiveCommand<Window, Unit> ExitProfile { get; }
    public ReactiveCommand<Window, Unit> CreateReport { get; }

    public AdminMainViewModel()
    {
        _CurrentPage = Pages[0];
        ExitProfile = ReactiveCommand.Create<Window>(ExitProfileImpl);
        CreateReport = ReactiveCommand.Create<Window>(CreateReportImpl);

        Orders = new ObservableCollection<Order>(Helper.GetContext().Orders.ToList());
        
        var canOpenMenuDish = this.WhenAnyValue(x => x.CurrentPage.OpenMenuDishesPage);
        OpenMenuDishPage = ReactiveCommand.Create(OpenMenuDishPageImpl, canOpenMenuDish);
        
        var canOpenEmployeesPage = this.WhenAnyValue(x => x.CurrentPage.OpenEmployeesPage);
        OpenEmployeesPage = ReactiveCommand.Create(OpenEmployeespageImpl, canOpenEmployeesPage);
        
        var canOpenProfilePage = this.WhenAnyValue(x => x.CurrentPage.OpenProfilePage);
        OpenProfilePage = ReactiveCommand.Create(OpenProfilePageImpl, canOpenProfilePage);

        var canOpenOrdersPage = this.WhenAnyValue(x => x.CurrentPage.OpenOrdersPage);
        OpenOrdersPage = ReactiveCommand.Create(OpenOrdersPageImpl, canOpenOrdersPage);
    }

    private void CreateReportImpl(Window obj)
    {
        using(ExcelHelper helper = new ExcelHelper())
        {
            if (helper.Open(filePath: Path.Combine(Environment.CurrentDirectory, @"C:\Users\Public\Documents\Отчёт о заказах.xlsx")))
            {
                int i = 0;
                var allOrders = Orders.ToList().OrderBy(p => p.DateAndTime).ToList();
                var application = new Excel.Application();
                string[] month = new string[12] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", 
                                                            "Август", "Сентябрь", "Окрябрь", "Ноябрь", "Декабрь" };
                
                application.SheetsInNewWorkbook = month.Length;

                Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                
                for (int j = 0; j < month.Length; ++j)
                {
                    int counter = 0;
                    int startRowIndex = 1;
                    
                    Excel.Worksheet worksheet = application.Worksheets.Item[j + 1];
                    worksheet.Name = month[j];

                    worksheet.Cells[1][startRowIndex] = "Номер";
                    worksheet.Cells[2][startRowIndex] = "Дата";
                    worksheet.Cells[3][startRowIndex] = "Цена";

                    startRowIndex++;

                    while (allOrders.Count > i)
                    {
                        if (allOrders[i].DateAndTime.Month == j + 1)
                        {
                            worksheet.Cells[1][startRowIndex] = allOrders[i].IdOrder;
                            worksheet.Cells[2][startRowIndex] = allOrders[i].DateAndTime.ToString("yy-MM-dd");
                            worksheet.Cells[3][startRowIndex] = allOrders[i].Price;
                            counter++;
                        }
                        else
                        {
                            break;
                        }
                        i++;
                        startRowIndex++;
                    }
                    
                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex],
                        worksheet.Cells[2][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "Итого:";

                    worksheet.Cells[3][startRowIndex].Formula =
                        $"=SUM(C{startRowIndex - counter}:" + $"C{startRowIndex - 1}";

                    worksheet.Columns.AutoFit();
                    helper.Save();
                }
                application.Visible = true;
            }
        }
    }

    private void ExitProfileImpl(Window obj)
    {
        AuthorizationViewModel.AuthUser = null;
        AuthorizationView av = new AuthorizationView();
        av.Show();
        obj.Close();
    }

    private void OpenOrdersPageImpl()
    {
        CurrentPage = Pages[3];
    }

    private void OpenProfilePageImpl()
    {
        CurrentPage = Pages[2];
    }

    private void OpenEmployeespageImpl()
    {
        CurrentPage = Pages[1];
    }

    private void OpenMenuDishPageImpl()
    {
        CurrentPage = Pages[0];
    }
}