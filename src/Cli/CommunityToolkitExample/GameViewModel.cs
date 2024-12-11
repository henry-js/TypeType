using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Terminal.Gui;

namespace CommunityToolkitExample;

public partial class GameViewModel : ViewModelBase
{
    private const string DEFAULT_LOGIN_PROGRESS_MESSAGE = "Press 'Login' to log in.";
    private const string INVALID_LOGIN_MESSAGE = "Please enter a valid user name and password.";
    private const string LOGGING_IN_PROGRESS_MESSAGE = "Logging in...";
    private const string VALID_LOGIN_MESSAGE = "The input is valid!";
    private readonly INavigationService navigationService;
    [ObservableProperty]
    private bool _canLogin;

    [ObservableProperty]
    private string _loginProgressMessage;

    private string _password;

    [ObservableProperty]
    private string _passwordLengthMessage;

    private string _username;

    [ObservableProperty]
    private string _usernameLengthMessage;

    [ObservableProperty]
    private ColorScheme? _validationColorScheme;

    [ObservableProperty]
    private string _validationMessage;
    private INavigationService _navigation;

    public GameViewModel(INavigationService navigationService)
    {
        _loginProgressMessage = string.Empty;
        _password = string.Empty;
        _passwordLengthMessage = string.Empty;
        _username = string.Empty;
        _usernameLengthMessage = string.Empty;
        _validationMessage = string.Empty;

        Username = string.Empty;
        Password = string.Empty;


        NavigationService = navigationService;
        NavigateToGameViewCommand = new(NavigationService.Navigate<GameViewModel>);
        ClearCommand = new(Clear);
        LoginCommand = new(Execute);

        // TODO: Use Factory Pattern to resolve: https://blog.stephencleary.com/2013/01/async-oop-2-constructors.html
        Clear();


        async void Execute() { await Login(); }
        return;
    }

    public AsyncRelayCommand ClearCommand { get; }

    public RelayCommand LoginCommand { get; }
    public AsyncRelayCommand NavigateToGameViewCommand { get; }


    public string Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            PasswordLengthMessage = $"_Password ({_password.Length} characters):";
            ValidateLogin();
        }
    }

    public string Username
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value);
            UsernameLengthMessage = $"_Username ({_username.Length} characters):";
            ValidateLogin();
        }
    }

    public INavigationService NavigationService
    {
        get => _navigation;
        set
        {
            SetProperty(ref _navigation, value);

        }
    }
    public async Task Initialized()
    {
        await Clear();
    }

    private async Task Clear()
    {
        Username = string.Empty;
        Password = string.Empty;
        await Task.Run(() => SendMessage(LoginActions.Clear, DEFAULT_LOGIN_PROGRESS_MESSAGE));
    }

    private async Task Login()
    {
        // await SendMessage(LoginActions.LoginProgress, LOGGING_IN_PROGRESS_MESSAGE);
        // await Task.Delay(TimeSpan.FromSeconds(3));
        // var clearTask = Clear();
    }

    private async Task SendMessage(LoginActions loginAction, string message = "")
    {
        switch (loginAction)
        {
            case LoginActions.Clear:
                LoginProgressMessage = message;
                ValidationMessage = INVALID_LOGIN_MESSAGE;
                ValidationColorScheme = Colors.ColorSchemes["Error"];
                break;
            case LoginActions.LoginProgress:
                LoginProgressMessage = message;
                break;
            case LoginActions.Validation:
                ValidationMessage = CanLogin ? VALID_LOGIN_MESSAGE : INVALID_LOGIN_MESSAGE;
                ValidationColorScheme = CanLogin ? Colors.ColorSchemes["Base"] : Colors.ColorSchemes["Error"];
                break;
        }
        await Task.Run(() => WeakReferenceMessenger.Default.Send(new Message<LoginActions> { Value = loginAction }));
    }

    private async void ValidateLogin()
    {
        CanLogin = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        await SendMessage(LoginActions.Validation);
    }
}
