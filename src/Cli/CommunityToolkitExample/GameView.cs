﻿using CommunityToolkit.Mvvm.Messaging;
using Terminal.Gui;

namespace CommunityToolkitExample;

public partial class GameView : IRecipient<Message<LoginActions>>
{
    public GameView(GameViewModel viewModel)
    {
        WeakReferenceMessenger.Default.Register(this);
        Title = $"Community Toolkit MVVM Example - {Application.QuitKey} to Exit";
        ViewModel = viewModel;
        InitializeComponent();
        usernameInput.TextChanged += (_, _) =>
                                     {
                                         ViewModel.Username = usernameInput.Text;
                                     };
        passwordInput.TextChanged += (_, _) =>
                                     {
                                         ViewModel.Password = passwordInput.Text;
                                     };
        loginButton.Accepting += (_, _) =>
                              {
                                  if (!ViewModel.CanLogin) { return; }
                                  ViewModel.NavigateToGameViewCommand.Execute(null);
                              };

        clearButton.Accepting += (_, _) =>
                              {
                                  ViewModel.ClearCommand.Execute(null);
                              };

        Initialized += async (_, _) => { await ViewModel.Initialized(); };
    }

    public GameViewModel ViewModel { get; set; }

    public void Receive(Message<LoginActions> message)
    {
        switch (message.Value)
        {
            case LoginActions.Clear:
                {
                    loginProgressLabel.Text = ViewModel.LoginProgressMessage;
                    validationLabel.Text = ViewModel.ValidationMessage;
                    validationLabel.ColorScheme = ViewModel.ValidationColorScheme;
                    break;
                }
            case LoginActions.LoginProgress:
                {
                    loginProgressLabel.Text = ViewModel.LoginProgressMessage;
                    break;
                }
            case LoginActions.Validation:
                {
                    validationLabel.Text = ViewModel.ValidationMessage;
                    validationLabel.ColorScheme = ViewModel.ValidationColorScheme;
                    break;
                }
        }
        SetText();
        // BUGBUG: This should not be needed:
    }

    private void SetText()
    {
        usernameInput.Text = ViewModel.Username;
        usernameLengthLabel.Text = ViewModel.UsernameLengthMessage;
        passwordInput.Text = ViewModel.Password;
        passwordLengthLabel.Text = ViewModel.PasswordLengthMessage;
    }
}
