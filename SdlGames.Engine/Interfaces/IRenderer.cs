using System.Drawing;
using SdlGames.Engine.Graphics;

namespace SdlGames.Engine.Interfaces;

public interface IRenderer
{
    void Clear(Color color);
    void Present();
    internal Texture CreateTexture(IntPtr bufferHandle, int bufferSize);
    void DrawTexture(PointF position, Texture texture);
    void DrawSprite(PointF position, Sprite sprite);
}