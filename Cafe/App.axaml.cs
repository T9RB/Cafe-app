using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Cafe.ViewModels;
using Cafe.Views;

namespace Cafe;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new AuthorizationView()
            {
                DataContext = new AuthorizationViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}