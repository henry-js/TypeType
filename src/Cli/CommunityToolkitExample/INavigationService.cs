
namespace CommunityToolkitExample;

// Navigation interface
public interface INavigationService
{
    public Task Navigate<TViewModel>() where TViewModel : ViewModel;
}
