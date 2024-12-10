using Microsoft.Extensions.DependencyInjection;

namespace CommunityToolkitExample;

public class NavigationService : INavigationService
{
    Dictionary<Type, Type> viewModelToViewMappings = [];
    private readonly Func<Type, IView> viewFinder;

    public NavigationService(Func<Type, IView> viewFinder)
    {
        viewModelToViewMappings.Add(typeof(GameViewModel), typeof(GameView));
        this.viewFinder = viewFinder;
    }

    public async Task Navigate<TViewModel>() where TViewModel : ViewModel
    {
        var viewType = viewModelToViewMappings[typeof(TViewModel)];

        var view = viewFinder?.Invoke(viewType);
        await Task.Run(() => view?.SetFocus());
    }
}

public class ViewModel
{
}
