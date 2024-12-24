using CommunityToolkit.Mvvm.ComponentModel;
using TypeType.Tui.ViewModels;

namespace TypeType.Tui.Services;

// Navigation interface
public interface INavigationService
{
    public Task Navigate<TViewModel>() where TViewModel : ViewModelBase;
}
