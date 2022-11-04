using SdlGames.Engine.Internal.Sdl;
using static SDL2.SDL;

// ReSharper disable once CheckNamespace
namespace SdlGames.Engine;

#if SDL

public abstract partial class Game
{
    protected Game(string title, int width, int height)
    {
        SDL_Init(SDL_INIT_EVERYTHING);
        this.Window = new SdlWindow(title, width, height);
        this.Renderer = new SdlRenderer((SdlWindow)this.Window);
    }
}

#endif