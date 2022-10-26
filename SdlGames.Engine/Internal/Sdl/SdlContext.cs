using System.Numerics;
using SDL2;
using SdlGames.Engine.Event;
using SdlGames.Engine.Graphics;
using SdlGames.Engine.Internal.Interfaces;
using SdlGames.Engine.Math;
using static SDL2.SDL;

namespace SdlGames.Engine.Internal.Sdl;

internal class SdlContext : IWindow, IRenderer
{
    public IntPtr WindowHandle { get; private set; }
    public IntPtr RendererHandle { get; private set; }

    public SdlContext(string title, int width, int height)
    {
        this.WindowHandle = SDL_CreateWindow(
            title,
            SDL_WINDOWPOS_UNDEFINED,
            SDL_WINDOWPOS_UNDEFINED,
            width,
            height,
            SDL_WindowFlags.SDL_WINDOW_VULKAN);

        this.RendererHandle = SDL_CreateRenderer(
            this.WindowHandle,
            0,
            SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
    }

    public void SetTitle(string title)
    {
        SDL_SetWindowTitle(this.WindowHandle, title);
    }

    public string GetTitle()
    {
        return SDL_GetWindowTitle(this.WindowHandle);
    }

    public void SetSize(int width, int height)
    {
        SDL_SetWindowSize(this.WindowHandle, width, height);
    }

    public Vector2 GetSize()
    {
        SDL_GetWindowSize(this.WindowHandle, out var width, out var height);
        return new Vector2(width, height);
    }

    public void SetPosition(int x, int y)
    {
        SDL_SetWindowPosition(this.WindowHandle, x ,y);
    }

    public Vector2 GetPosition()
    {
        SDL_GetWindowPosition(this.WindowHandle, out var x, out var y);
        return new Vector2(x, y);
    }

    public void SetVsync(bool enabled)
    {
        SDL_RenderSetVSync(this.RendererHandle, enabled ? 1 : 0);
    }

    public bool GetVsync()
    {
        return SDL_GetHintBoolean(SDL_HINT_RENDER_VSYNC, SDL_bool.SDL_FALSE) == SDL_bool.SDL_TRUE;
    }

    public void SetFullscreen(bool enabled)
    {
        throw new NotImplementedException();
    }

    public bool GetFullscreen()
    {
        throw new NotImplementedException();
    }

    public void PollEvents(IWindow.WindowEventHandler handler)
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

    public void Clear(Color color)
    {
        SDL_SetRenderDrawColor(this.RendererHandle, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
        SDL_RenderClear(this.RendererHandle);
    }

    public void Present()
    {
        SDL_RenderPresent(this.RendererHandle);
    }


    Texture IRenderer.CreateTexture(IntPtr bufferHandle, int bufferSize)
    {
        var rwOps = SDL_RWFromConstMem(bufferHandle, bufferSize);
        var textureHandle = SDL_image.IMG_LoadTexture_RW(this.RendererHandle, rwOps, 0);
        SDL_QueryTexture(textureHandle, out _, out _, out var width, out var height);
        
        return new Texture(textureHandle, new Vector2(width, height));
    }

    public void DrawTexture(Vector2 position, Texture texture)
    {
        var rect = new SDL_FRect
        {
            x = position.X,
            y = position.Y,
            w = texture.Size.X,
            h = texture.Size.Y
        };
        
        SDL_RenderCopyF(this.RendererHandle, texture.Handle, IntPtr.Zero, ref rect);
    }

    public void DrawSprite(Vector2 position, Texture texture, Rect sourceRect)
    {
        var rect = new SDL_FRect
        {
            x = position.X,
            y = position.Y,
            w = sourceRect.Width,
            h = sourceRect.Height
        };
        
        var sdlSourceRect = new SDL_Rect
        {
            x = (int)sourceRect.X,
            y = (int)sourceRect.Y,
            w = (int)sourceRect.Width,
            h = (int)sourceRect.Height
        };
        
        SDL_RenderCopyF(this.RendererHandle, texture.Handle, ref sdlSourceRect, ref rect);
    }
}