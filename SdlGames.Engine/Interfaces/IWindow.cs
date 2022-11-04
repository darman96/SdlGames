using System.Numerics;
using SdlGames.Engine.Event;

namespace SdlGames.Engine.Interfaces;

public interface IWindow
{
    internal delegate void WindowEventHandler(EventType type);

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

    internal void PollEvents(WindowEventHandler handler);
}