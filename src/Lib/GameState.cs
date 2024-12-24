
namespace TypeType.Lib;

public class GameState
{
    private int _pos = 0;
    private readonly string _text;

    public void Init(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);
    }

    public char Current => _text[_pos];
}
