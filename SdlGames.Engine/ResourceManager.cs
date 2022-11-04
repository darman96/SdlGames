using System.Runtime.InteropServices;
using SdlGames.Engine.Graphics;
using SdlGames.Engine.Interfaces;
using SdlGames.Engine.Internal;

namespace SdlGames.Engine;

public class ResourceManager
{
    private readonly IRenderer renderer;

    public ResourceManager(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public Texture LoadTexture(string file)
    {
        var bytes = EmbeddedResourceLoader
            .Instance
            .LoadBytes(file);
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
    
        var texture = this.renderer
            .CreateTexture(handle.AddrOfPinnedObject(), bytes.Length);
        handle.Free();
        
        return texture;
    }
}