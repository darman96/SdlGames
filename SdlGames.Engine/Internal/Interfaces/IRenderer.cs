using System.Drawing;
using System.Numerics;
using SdlGames.Engine.Graphics;
using SdlGames.Engine.Math;

namespace SdlGames.Engine.Internal.Interfaces;

public interface IRenderer
{
    internal Texture CreateTexture(IntPtr bufferHandle, int bufferSize);
    void DrawTexture(PointF position, Texture texture);
    void DrawSprite(PointF position, Texture texture, RectangleF sourceRect);
}