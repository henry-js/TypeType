using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Terminal.Gui;
using TypeType.Tui.Services;
using TypeType.Tui.ViewModels;

namespace TypeType.Tui.ViewModels;

public partial class GameViewModel : ViewModelBase
{

    public GameViewModel(INavigationService navigationService, ILogger<GameViewModel> logger)
    {
        this.logger = logger;
        this.state = new GameState();
    }
    [ObservableProperty]
    private Key _lastKeyPressed = default!;
    private readonly ILogger<GameViewModel> logger;
    private readonly GameState state;

    public RelayCommand LoadCommand { get; internal set; }

    partial void OnLastKeyPressedChanging(Key value)
    {
        if (value.IsAlt || value.IsCtrl || value.IsShift) return;

        Debug.WriteLine($"Key pressed: {value}");
    }
}

internal class GameState
{
    public GameState()
    {
    }
}
