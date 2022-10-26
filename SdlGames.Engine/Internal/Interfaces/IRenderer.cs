using System.Numerics;
using SdlGames.Engine.Graphics;
using SdlGames.Engine.Math;

namespace SdlGames.Engine.Internal.Interfaces;

public interface IRenderer
{
    internal Texture CreateTexture(IntPtr bufferHandle, int bufferSize);
    void DrawTexture(Vector2 position, Texture texture);
    void DrawSprite(Vector2 position, Texture texture, Rect sourceRect);
}