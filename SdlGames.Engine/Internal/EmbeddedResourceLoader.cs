using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace SdlGames.Engine.Internal;

public class EmbeddedResourceLoader
{
    private static EmbeddedResourceLoader? instance;
    
    public static EmbeddedResourceLoader Instance
    {
        get { return instance ??= new EmbeddedResourceLoader(); }
    }

    private readonly EmbeddedFileProvider fileProvider;
    
    private EmbeddedResourceLoader()
    {
        this.fileProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly()!);
    }
    
    public string LoadText(string path)
    {
        using var stream = this.fileProvider.GetFileInfo(path).CreateReadStream();
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
    
    public byte[] LoadBytes(string path)
    {
        using var stream = this.fileProvider.GetFileInfo(path).CreateReadStream();
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}