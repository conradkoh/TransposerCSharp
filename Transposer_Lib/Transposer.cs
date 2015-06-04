using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transposer_Lib
{
    public class Transposer
    {
        Playlist currentPlaylist;
        Playlist library;
        const string songLibraryPath = ".\\System\\songlist.slist";
        const string systemFilePath = ".\\System\\Active.tsys";
        File sysFile;
        public string DISPLAY_MAIN;
        public string DISPLAY_PLAYLIST;
        public Transposer()
        {
            sysFile = new File(systemFilePath);
            List<string> fileContent = sysFile.GetFileContent();
            string activePlaylist = fileContent.FirstOrDefault();
            if (activePlaylist != null)
            {
                currentPlaylist = new Playlist(activePlaylist);
            }
            library = new Playlist(songLibraryPath);

        }

        public void TransposeUp()
        {
            DISPLAY_MAIN = sysFile.GetFileName();
        }

        public void TransposeDown()
        {

        }

        public void LoadPlaylist(string playlistFileName)
        {
            currentPlaylist = new Playlist(playlistFileName);
            DISPLAY_PLAYLIST = currentPlaylist.ToString();
        }

        private void UpdateSystemFile()
        {
            sysFile.SetFileContent(currentPlaylist.GetFileName());
            sysFile.Save();
        }
    }
}
