namespace GooglePhotosMatcher;

internal class GoogleFile
{
    private const string EditedSuffix = "-edited.jpg";
    private const string ImgSuffix = ".jpg";
    private const string JsonSuffix = ".jpg.json";

    public string ImgFile { get;  }
    public string? JsonFile { get; }

    internal GoogleFile(string path)
    {
        JsonFile = null;
        ImgFile = path;

        if (path.EndsWith(EditedSuffix))
        {
            var jsonPath = path.Replace(EditedSuffix, JsonSuffix);
            if (File.Exists(jsonPath))
            {
                JsonFile = jsonPath;
            }
        }
        else
        {
            var jsonPath = path.Replace(ImgSuffix, JsonSuffix);
            if (File.Exists(jsonPath))
            {
                JsonFile = jsonPath;
            }
        }
    }
}