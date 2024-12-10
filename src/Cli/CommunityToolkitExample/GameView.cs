using Terminal.Gui;

namespace CommunityToolkitExample;

public partial class GameView : IView
{
    public bool SetFocus()
    {
        return this.SetFocus();
    }
}

public interface IView
{
    bool SetFocus();
}
