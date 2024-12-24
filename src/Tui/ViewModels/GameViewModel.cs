using System.Data.Common;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using TypeType.Lib;
using TypeType.Lib.Data;
using TypeType.Tui.Services;

namespace TypeType.Tui.ViewModels;

public partial class GameViewModel : ViewModelBase
{
    [ObservableProperty]
    private char _lastKeyPressed = default!;
    [ObservableProperty]
    private string _challengeText;
    private readonly ILogger<GameViewModel> logger;
    private readonly DbContext db;
    private readonly GameState state;

    public GameViewModel(INavigationService navigationService, ILogger<GameViewModel> logger, DbContext db)
    {
        this.logger = logger;
        this.db = db;
        this.state = new GameState();

        LoadCommand = new(Load);
    }

    public RelayCommand LoadCommand { get; }
    private void Load()
    {
        var quote = db.GetRandom();
        ChallengeText = quote.Text;
        state.Init(quote.Text);
    }

    partial void OnLastKeyPressedChanging(char value)
    {
        Debug.WriteLine($"Key pressed: {value}");

        if (state.Current == value)
            _lastKeyPressed = value;
    }
}
