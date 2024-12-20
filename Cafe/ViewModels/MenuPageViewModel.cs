using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using Cafe.Data.Context;
using Cafe.Models.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Cafe.ViewModels;

public class MenuPageViewModel : PageViewModelBase
{
    private readonly MyDbContext _db = new MyDbContext();
    
    private bool _openMenuDishesPage;

    private ObservableCollection<Dish> _dishes;
    
    private string _name;
    private double _price;

    public ObservableCollection<Dish> Dishes
    {
        get => _dishes;
        set => this.RaiseAndSetIfChanged(ref _dishes, value);
    }
    
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    public double Price
    {
        get => _price;
        set => this.RaiseAndSetIfChanged(ref _price, value);
    }
    
    public override bool OpenMenuDishesPage
    {
        get => _openMenuDishesPage;
        protected set => this.RaiseAndSetIfChanged(ref _openMenuDishesPage, value);
    }
    
    public override bool OpenEmployeesPage 
    { 
        get => true;
        protected set => throw new NotSupportedException(); 
    }
    
    public override bool OpenProfilePage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    public override bool OpenOrdersPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public override bool OpenOrdersCookPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public override bool OpenNewOrderWaiterPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public override bool OpenOrdersWaiterPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public ReactiveCommand<Unit, Unit> AddDish { get; }
    
    public MenuPageViewModel()
    {
        AddDish = ReactiveCommand.Create(AddDishImpl);
        Dishes = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
    }

    public void RemoveDishImpl(Dish selectDish)
    {
        var dish = Dishes.Where(x => x.IdDish == selectDish.IdDish).FirstOrDefault();
        if (dish != null)
        {
            Dishes.Remove(selectDish);
            _db.Dishes.Remove(dish);
            _db.SaveChanges();
            MessageBoxManager.GetMessageBoxStandard("Успех", "Блюдо удалено", ButtonEnum.Ok, Icon.Success).ShowAsync();   
        }
    }
    
    private void AddDishImpl()
    {
        var dish = Helper.GetContext().Dishes.FirstOrDefault(x=> x.Name == Name);

        if (dish == null)
        {
            Dish newDish = new Dish();

            newDish.Name = Name;
            newDish.Price = Convert.ToDouble(Price);
            
            Dishes.Add(newDish);
            Helper.GetContext().Dishes.AddAsync(newDish);
            Helper.GetContext().SaveChanges();
            
            MessageBoxManager.GetMessageBoxStandard("Успех", "Блюдо добавлено", ButtonEnum.Ok, Icon.Success).ShowAsync();
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Блюдо с таким названием уже существует", ButtonEnum.Ok, Icon.Error).ShowAsync();
        }
    }
}