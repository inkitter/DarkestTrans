using System;
using System.IO;

namespace FullTexttoChar
{
    public class GameFolder
    {
        private string oripath, gamepath;
        public GameFolder()
        {
            oripath = @"ori\";
            gamepath = @"D:\SteamLibrary\steamapps\common\DarkestDungeon\";
        }
        public GameFolder(string GamePath)
        {
            oripath = @"ori\";
            if (GamePath.Substring(GamePath.Length - 1, 1) != @"\") { GamePath = GamePath + @"\"; }
            gamepath = GamePath;
        }
        public void SetGameFolder(string GameFolder)
        {
            gamepath = GameFolder;
        }

        public string GamePath
        {
            get { return gamepath; }
        }

        public string OriPath
        {
            get { return oripath; }
        }

        public string GameXMLFolder
        {
            get { return gamepath+ "localization\\"; }
        }

        public string GameFontFolder
        {
            get { return gamepath + "fonts\\"; }
        }

        public string OriXMLFolder
        {
            get { return oripath + "chnxml\\"; }
        }

        public string OriFontFolder
        {
            get { return oripath + "fonts\\"; }
        }

        public string OriFontFile
        {
            get { return oripath + "fonts\\Original.fnt"; }
        }
    }
}