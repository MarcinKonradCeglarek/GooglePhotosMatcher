namespace GooglePhotosMatcher
{
    internal class Logger
    {
        private static string OutDir;

        public static void Init(string outDir)
        {
            OutDir = outDir;
        }

        public enum Target 
        {
            File = 1,
            Console = 2,
            All = 255,
        }

        public static void Log(string message = "", Target target = Target.All)
        {
            if (target.HasFlag(Target.File))
            {
                using FileStream aFile =
                    new FileStream(Path.Combine(OutDir, "merger.log"), FileMode.Append, FileAccess.Write);
                using StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine($"{DateTime.Now:yyyyMMdd-HHmmss}: " + message);

            }

            if (target.HasFlag(Target.Console))
            {
                Console.WriteLine(message);
            }
        }
    }
}
