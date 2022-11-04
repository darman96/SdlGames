using System.Drawing;
using SDL2;

namespace SdlGames.Engine.Extensions;

public static class RectangleExtensions
{
    public static SDL.SDL_Rect ToSdlRect(this Rectangle rect)
    {
        return new SDL.SDL_Rect
        {
            x = rect.X,
            y = rect.Y,
            w = rect.Width,
            h = rect.Height
        };
    }
    
    public static SDL.SDL_Rect ToSdlRect(this RectangleF rect)
    {
        return new SDL.SDL_Rect
        {
            x = (int)rect.X,
            y = (int)rect.Y,
            w = (int)rect.Width,
            h = (int)rect.Height
        };
    }
    
    public static SDL.SDL_FRect ToSdlFRect(this Rectangle rect)
    {
        return new SDL.SDL_FRect
        {
            x = rect.X,
            y = rect.Y,
            w = rect.Width,
            h = rect.Height
        };
    }
    
    public static SDL.SDL_FRect ToSdlFRect(this RectangleF rect)
    {
        return new SDL.SDL_FRect
        {
            x = rect.X,
            y = rect.Y,
            w = rect.Width,
            h = rect.Height
        };
    }
}