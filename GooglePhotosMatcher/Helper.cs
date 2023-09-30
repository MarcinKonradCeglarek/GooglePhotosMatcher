
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using GooglePhotosMatcher.Models;

namespace GooglePhotosMatcher
{
    internal class Helper
    {
        private static readonly Regex DateRegex = new Regex(":");

        public static int ProcessDirectory(string directory, string outDir)
        {
            var counter = 0;
            var files = Directory.GetFiles(directory);
            foreach (var caseSensitiveFilename in files)
            {
                var filename = caseSensitiveFilename.ToLowerInvariant();
                if (filename.EndsWith(".jpg"))
                {
                    var file = new GoogleFile(filename);
                    var exifData = GetExifData(file.ImgFile);
                    var googleDate = GetJsonData(file.JsonFile);

                    var bestDateTaken = googleDate ?? exifData?.DateTaken;

                    if (bestDateTaken.HasValue)
                    {
                        var dstFilename = GetDstPath(outDir, bestDateTaken.Value, exifData?.CameraModel);

                        Logger.Log($"MOV {filename} -> {dstFilename}", Logger.Target.File);

                        File.Move(filename, dstFilename);
                        File.SetCreationTime(dstFilename, bestDateTaken.Value);
                        if (File.Exists(file.JsonFile))
                        {
                            File.Copy(file.JsonFile, dstFilename + ".json");
                            
                        }
                    }
                    else
                    {
                        Logger.Log($"SKIP: no ex info: {filename}");
                    }
                    counter++;
                }
                else if (!filename.EndsWith(".json"))
                {
                    Logger.Log($"SKIP: unhandled extension: {filename}", Logger.Target.File);
                }
            }

            return counter;
        }

        private static string GetDstPath(string outDir, DateTime dateTaken, string? device = "unk")
        {
            var year = dateTaken.Year.ToString();
            var month = dateTaken.Month.ToString("D2");

            var yearDir = Path.Combine(outDir, year);
            var monthDir = Path.Combine(yearDir, month);
            if (!Directory.Exists(yearDir))
            {
                Directory.CreateDirectory(yearDir);
            }

            if (!Directory.Exists(monthDir))
            {
                Directory.CreateDirectory(monthDir);
            }

            var filePattern = $"{device}_{dateTaken:yyyyMMdd-HHmmss}";

            if (File.Exists(Path.Combine(monthDir, $"{filePattern}.jpg")))
            {
                var uniqueName = GetUniqueName(monthDir, filePattern, 1);
                return Path.Combine(monthDir, uniqueName);
            }

            return Path.Combine(monthDir, filePattern + ".jpg");
        }

        public static string GetUniqueName(string folder, string pattern, int index)
        {
            if (File.Exists(Path.Combine(folder, $"{pattern}-{index}.jpg")))
            {
                return GetUniqueName(folder, pattern, index + 1);
            }

            return $"{pattern}-{index}.jpg";
        }

        public static ImageInfo? GetExifData(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                try
                {
                    string cameraModel = "unk";
                    if (myImage.PropertyIdList.Contains(0x0110))
                    {
                        cameraModel = Encoding.UTF8.GetString(myImage.GetPropertyItem(0x0110).Value)
                            .Replace("\0", string.Empty).Trim();
                    }
                    
                    var dateTaken = Encoding.UTF8.GetString(myImage.GetPropertyItem(36867).Value);
                    var date = DateTime.Parse(DateRegex.Replace(dateTaken, "-", 2));

                    return new ImageInfo(date, cameraModel);
                }
                catch
                {
                    return null;
                }
            }
        }

        public static DateTime? GetJsonData(string? jsonFilePath)
        {
            if (jsonFilePath != null && File.Exists(jsonFilePath))
            {
                var contents = File.ReadAllText(jsonFilePath);
                var jsonObj = JsonSerializer.Deserialize<GooglePhotoJson>(contents);

                if (jsonObj != null)
                {
                    return UnixTimeStampToDateTime(int.Parse(jsonObj.photoTakenTime.timestamp));
                }
            }

            return null;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);
            return dateTime;
        }
    }


}

