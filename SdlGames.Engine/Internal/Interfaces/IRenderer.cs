using System.Numerics;
using SdlGames.Engine.Graphics;

namespace SdlGames.Engine.Internal.Interfaces;

public interface IRenderer
{
    internal Texture CreateTexture(IntPtr bufferHandle, int bufferSize);
    void DrawTexture(Vector2 position, Texture texture);
}