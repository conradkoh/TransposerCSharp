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
        static string currentDIR = System.IO.Directory.GetCurrentDirectory();
        public static string systemDIR = currentDIR + "\\System";
        static string songLibraryPath = currentDIR + "\\System\\songlist.slist";
        static string systemFilePath = currentDIR + "\\System\\Active.tsys";
        static string debugFilePath = currentDIR + "\\System\\Debug.txt";
        File sysFile = new File();
        File debugFile = new File();
        int currentIdx = 0;
        

        public string DISPLAY_MAIN;
        public string DISPLAY_PLAYLIST;
        public string DISPLAY_FEEDBACK;

        Song currentSong;
        public Transposer()
        {
            debugFile.Load(debugFilePath);
            sysFile.Load(systemFilePath);
            List<string> fileContent = sysFile.GetFileContent();
            string activePlaylist = fileContent.FirstOrDefault();
            if (activePlaylist != null)
            {
                currentPlaylist = new Playlist(activePlaylist);
                currentSong = currentPlaylist.FirstSong();
            }
            library = new Playlist(songLibraryPath);
            

        }

        public void TransposeUp()
        {
            if (currentSong != null)
            {
                currentSong.TransposeUp();
                DISPLAY_MAIN = currentSong.ToString();
            }
            
        }

        public void TransposeDown()
        {
            if (currentSong != null)
            {
                currentSong.TransposeDown();
                DISPLAY_MAIN = currentSong.ToString();
            }
            
        }

        public void Next()
        {
            currentIdx = (currentIdx + 1 + currentPlaylist.Count())%currentPlaylist.Count();
        }

        public void Previous()
        {
            currentIdx = (currentIdx - 1 + currentPlaylist.Count()) % currentPlaylist.Count();
        }

        public void LoadPlaylist(string playlistFileName)
        {
            currentPlaylist = new Playlist(playlistFileName);
            currentSong = currentPlaylist.FirstSong();
            UpdateDisplays();
        }

        private void UpdateSystemFile()
        {
            sysFile.SetFileContent(currentPlaylist.GetFileName());
            sysFile.Save();
        }

        private void UpdateDisplays()
        {
            DISPLAY_PLAYLIST = currentPlaylist.GetPlaylist();
            DISPLAY_MAIN = currentSong.ToString();
        }
    }
}
