using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace SetupSonic
{
    public static class Utility
    {
        public static bool BotDirectoryExists(string path = Constants.BotDirectory)
        {
            return Directory.Exists(path);
        }

        public static void ExtractBot()
        {
            var zip = $"{Constants.BotDirectory}.zip";

            if (File.Exists(zip))
            {
                ZipFile.ExtractToDirectory(zip, ".");
                Directory.Move($"{Constants.BotDirectory}-master", Constants.BotDirectory);
                RemoveBotDownloadArtifact();
            }
        }

        public static void RemoveBotDownloadArtifact()
        {
            File.Delete($"{Constants.BotDirectory}.zip");
        }

        public static void OnCancelOrCloseDownload()
        {
            RemoveBotDownloadArtifact();
            Environment.Exit(0);
        }

        public static bool IsD2botRunning()
        {
            return Process.GetProcessesByName("D2Bot").Length > 0;
        }
    }
}
