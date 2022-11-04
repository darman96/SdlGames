using static SDL2.SDL;
using System.Numerics;
using SdlGames.Engine.Event;
using SdlGames.Engine.Interfaces;

namespace SdlGames.Engine.Internal.Sdl;

internal class SdlWindow : IWindow
{
    public IntPtr Handle => this.windowHandle;
    
    private readonly IntPtr windowHandle;

    public SdlWindow(string title, int width, int height)
    {
        this.windowHandle = SDL_CreateWindow(
            title,
            SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,
            width, height,
            SDL_WindowFlags.SDL_WINDOW_VULKAN);
        
        if (this.windowHandle == IntPtr.Zero)
            throw new Exception("Failed to create window");
    }

    public void SetTitle(string title)
    {
        SDL_SetWindowTitle(this.windowHandle, title);
    }

    public string GetTitle()
    {
        return SDL_GetWindowTitle(this.windowHandle);
    }

    public void SetSize(int width, int height)
    {
        SDL_SetWindowSize(this.windowHandle, width, height);
    }

    public Vector2 GetSize()
    {
        SDL_GetWindowSize(this.windowHandle, out var width, out var height);
        return new Vector2(width, height);
    }

    public void SetPosition(int x, int y)
    {
        SDL_SetWindowPosition(this.windowHandle, x, y);
    }

    public Vector2 GetPosition()
    {
        SDL_GetWindowPosition(this.windowHandle, out var x, out var y);
        return new Vector2(x, y);
    }

    public void SetVsync(bool enabled)
    {
        SDL_GL_SetSwapInterval(enabled ? 1 : 0);
    }

    public bool GetVsync()
    {
        return SDL_GL_GetSwapInterval() == 1;
    }

    public void SetFullscreen(bool enabled)
    {
        SDL_SetWindowFullscreen(this.windowHandle, enabled 
            ? SDL_GetWindowFlags(this.windowHandle) & (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN 
            : SDL_GetWindowFlags(this.windowHandle) & ~(uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
    }

    public bool GetFullscreen()
    {
        return ((SDL_WindowFlags)SDL_GetWindowFlags(this.windowHandle))
            .HasFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
    }

    void IWindow.PollEvents(IWindow.WindowEventHandler handler)
    {
        while (SDL_PollEvent(out var sdlEvent) != 0)
        {
            switch (sdlEvent.type)
            {
                case SDL_EventType.SDL_QUIT:
                    handler(EventType.Quit);
                    break;
                case SDL_EventType.SDL_FIRSTEVENT:
                case SDL_EventType.SDL_APP_TERMINATING:
                case SDL_EventType.SDL_APP_LOWMEMORY:
                case SDL_EventType.SDL_APP_WILLENTERBACKGROUND:
                case SDL_EventType.SDL_APP_DIDENTERBACKGROUND:
                case SDL_EventType.SDL_APP_WILLENTERFOREGROUND:
                case SDL_EventType.SDL_APP_DIDENTERFOREGROUND:
                case SDL_EventType.SDL_LOCALECHANGED:
                case SDL_EventType.SDL_DISPLAYEVENT:
                case SDL_EventType.SDL_WINDOWEVENT:
                case SDL_EventType.SDL_SYSWMEVENT:
                case SDL_EventType.SDL_KEYDOWN:
                case SDL_EventType.SDL_KEYUP:
                case SDL_EventType.SDL_TEXTEDITING:
                case SDL_EventType.SDL_TEXTINPUT:
                case SDL_EventType.SDL_KEYMAPCHANGED:
                case SDL_EventType.SDL_TEXTEDITING_EXT:
                case SDL_EventType.SDL_MOUSEMOTION:
                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                case SDL_EventType.SDL_MOUSEWHEEL:
                case SDL_EventType.SDL_JOYAXISMOTION:
                case SDL_EventType.SDL_JOYBALLMOTION:
                case SDL_EventType.SDL_JOYHATMOTION:
                case SDL_EventType.SDL_JOYBUTTONDOWN:
                case SDL_EventType.SDL_JOYBUTTONUP:
                case SDL_EventType.SDL_JOYDEVICEADDED:
                case SDL_EventType.SDL_JOYDEVICEREMOVED:
                case SDL_EventType.SDL_CONTROLLERAXISMOTION:
                case SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                case SDL_EventType.SDL_CONTROLLERBUTTONUP:
                case SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                case SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                case SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED:
                case SDL_EventType.SDL_CONTROLLERTOUCHPADDOWN:
                case SDL_EventType.SDL_CONTROLLERTOUCHPADMOTION:
                case SDL_EventType.SDL_CONTROLLERTOUCHPADUP:
                case SDL_EventType.SDL_CONTROLLERSENSORUPDATE:
                case SDL_EventType.SDL_FINGERDOWN:
                case SDL_EventType.SDL_FINGERUP:
                case SDL_EventType.SDL_FINGERMOTION:
                case SDL_EventType.SDL_DOLLARGESTURE:
                case SDL_EventType.SDL_DOLLARRECORD:
                case SDL_EventType.SDL_MULTIGESTURE:
                case SDL_EventType.SDL_CLIPBOARDUPDATE:
                case SDL_EventType.SDL_DROPFILE:
                case SDL_EventType.SDL_DROPTEXT:
                case SDL_EventType.SDL_DROPBEGIN:
                case SDL_EventType.SDL_DROPCOMPLETE:
                case SDL_EventType.SDL_AUDIODEVICEADDED:
                case SDL_EventType.SDL_AUDIODEVICEREMOVED:
                case SDL_EventType.SDL_SENSORUPDATE:
                case SDL_EventType.SDL_RENDER_TARGETS_RESET:
                case SDL_EventType.SDL_RENDER_DEVICE_RESET:
                case SDL_EventType.SDL_POLLSENTINEL:
                case SDL_EventType.SDL_USEREVENT:
                case SDL_EventType.SDL_LASTEVENT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}