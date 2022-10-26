using System.Numerics;
using SdlGames.Engine.Event;
using SdlGames.Engine.Math;

namespace SdlGames.Engine.Internal.Interfaces;

public interface IWindow
{
    public delegate void WindowEventHandler(EventType type);

    void SetTitle(string title);
    string GetTitle();

    void SetSize(int width, int height);
    Vector2 GetSize();

    void SetPosition(int x, int y);
    Vector2 GetPosition();
    
    void SetVsync(bool enabled);
    bool GetVsync();

    void SetFullscreen(bool enabled);
    bool GetFullscreen();

    void PollEvents(WindowEventHandler handler);

    void Clear(Color color);

    void Present();
}