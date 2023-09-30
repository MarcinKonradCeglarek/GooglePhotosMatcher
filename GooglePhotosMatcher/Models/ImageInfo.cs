namespace GooglePhotosMatcher.Models;

internal class ImageInfo
{
    public ImageInfo(DateTime dateTaken, string cameraModel)
    {
        DateTaken = dateTaken;
        CameraModel = cameraModel;
    }

    public DateTime DateTaken { get; }
    public string CameraModel { get; }
}