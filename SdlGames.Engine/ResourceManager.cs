using System.Runtime.InteropServices;
using SdlGames.Engine.Graphics;
using SdlGames.Engine.Internal;

namespace SdlGames.Engine;

public class ResourceManager
{
    public Texture LoadTexture(string file)
    {
        var bytes = EmbeddedResourceLoader
            .Instance
            .LoadBytes(file);
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

        var texture = GameManager
            .Instance
            .Renderer
            .CreateTexture(handle.AddrOfPinnedObject(), bytes.Length);
        handle.Free();
        
        return texture;
    }
}