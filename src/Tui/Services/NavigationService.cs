using Microsoft.Extensions.DependencyInjection;
using Terminal.Gui;
using TypeType.Tui.ViewModels;
using TypeType.Tui.Views;

namespace TypeType.Tui.Services;

public class NavigationService : INavigationService
{
    private readonly Dictionary<Type, Type> viewFinder = [];
    private readonly IServiceProvider services;

    public NavigationService(IServiceProvider services)
    {
        viewFinder.Add(typeof(GameViewModel), typeof(GameView));
        viewFinder.Add(typeof(LoginViewModel), typeof(LoginView));
        this.services = services;
    }

    public async Task Navigate<TViewModel>() where TViewModel : ViewModelBase
    {
        var windowType = viewFinder[typeof(TViewModel)];
        var window = services.GetRequiredService(windowType) as Toplevel;
        Application.Run(window);
    }
}
