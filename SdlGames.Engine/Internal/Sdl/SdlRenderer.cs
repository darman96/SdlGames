using static SDL2.SDL;
using static SDL2.SDL_image;
using System.Drawing;
using System.Numerics;
using SdlGames.Engine.Extensions;
using SdlGames.Engine.Graphics;
using SdlGames.Engine.Interfaces;

namespace SdlGames.Engine.Internal.Sdl;

internal class SdlRenderer : IRenderer
{
    private readonly IntPtr rendererHandle;

    public SdlRenderer(SdlWindow window)
    {
        this.rendererHandle = SDL_CreateRenderer(
            window.Handle,
            -1,
            SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
        
        if (this.rendererHandle == IntPtr.Zero)
            throw new Exception("Failed to create renderer");
    }

    public void Clear(Color color)
    {
        SDL_SetRenderDrawColor(this.rendererHandle, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
        SDL_RenderClear(this.rendererHandle);
    }

    public void Present()
    {
        SDL_RenderPresent(this.rendererHandle);
    }

    Texture IRenderer.CreateTexture(IntPtr bufferHandle, int bufferSize)
    {
        var rwOps = SDL_RWFromConstMem(bufferHandle, bufferSize);
        var textureHandle = IMG_LoadTexture_RW(this.rendererHandle, rwOps, 0);
        SDL_QueryTexture(textureHandle, out _, out _, out var width, out var height);
        
        return new Texture(textureHandle, new Vector2(width, height));
    }

    public void DrawTexture(PointF position, Texture texture)
    {
        var rect = new SDL_Rect
        {
            x = (int)position.X,
            y = (int)position.Y,
            w = (int)texture.Size.X,
            h = (int)texture.Size.Y
        };

        SDL_RenderCopy(this.rendererHandle, texture.Handle, IntPtr.Zero, ref rect);
    }

    public void DrawSprite(PointF position, Sprite sprite)
    {
        var dstRect = new SDL_FRect
        {
            x = position.X,
            y = position.Y,
            w = sprite.SourceRect.Width,
            h = sprite.SourceRect.Height
        };

        var sourceRect = sprite.SourceRect.ToSdlRect();
        SDL_RenderCopyF(this.rendererHandle, sprite.Texture.Handle, ref sourceRect, ref dstRect);
    }
}