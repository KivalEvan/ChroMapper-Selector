using System.Reflection;
using UnityEngine;

namespace Selector;

internal static class Utils
{
    internal static Sprite LoadSpriteFromResources(string path)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
        var data = new byte[stream.Length];
        stream.Read(data, 0, (int)stream.Length);

        var texture2D = new Texture2D(256, 256);
        texture2D.LoadImage(data);

        return Sprite.Create(texture2D, new(0, 0, texture2D.width, texture2D.height),
            new(0, 0), 100.0f);
    }
}