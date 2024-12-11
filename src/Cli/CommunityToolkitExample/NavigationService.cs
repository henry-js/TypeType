using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Gui;

namespace CommunityToolkitExample;

public class NavigationService : INavigationService
{
    private readonly Func<Type, Window> viewFinder;

    public NavigationService(Func<Type, Window> viewFinder)
    {
        this.viewFinder = viewFinder;
    }

    public async Task Navigate<TViewModel>() where TViewModel : ViewModelBase
    {
        var view = viewFinder?.Invoke(typeof(TViewModel));
        await Task.Run(() => Application.Run(view));
    }
}

public abstract class ViewModelBase : ObservableObject
{
}
