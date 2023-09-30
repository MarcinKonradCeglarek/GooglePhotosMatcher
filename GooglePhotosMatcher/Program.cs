// See https://aka.ms/new-console-template for more information

using GooglePhotosMatcher;

var path = "\\\\192.168.0.25\\home\\Downloads\\Takeout\\XXX";   // 15967
//var path = "\\\\192.168.0.25\\home\\Photos\\GooglePhotosBackup\\Takeout\\Google Photos";   //~2400
var outDir = "\\\\192.168.0.25\\home\\Downloads\\Takeout\\Output";

var directories = Directory.GetDirectories(path);
Logger.Init(outDir);

foreach (var directory in directories)
{
    Logger.Log("------------------------------------------");
    Logger.Log($"Processing: {directory}");
    Logger.Log();
    Helper.ProcessDirectory(directory, outDir);
}

