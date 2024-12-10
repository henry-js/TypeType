using Terminal.Gui;

public partial class GameView : Window
{
    private Label titleLabel;

    private void InitializeComponent()
    {
        titleLabel = new Label();
        titleLabel.Text = "Game View";
        Add(titleLabel);
    }
}
