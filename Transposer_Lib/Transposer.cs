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
        public const string systemDIR = ".\\System";
        const string songLibraryPath = ".\\System\\songlist.slist";
        const string systemFilePath = ".\\System\\Active.tsys";
        const string debugFilePath = ".\\System\\Debug.txt";
        File sysFile = new File();
        File debugFile = new File();
        

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

        public void LoadPlaylist(string playlistFileName)
        {
            currentPlaylist = new Playlist(playlistFileName);
            DISPLAY_PLAYLIST = currentPlaylist.GetPlaylist();
        }

        private void UpdateSystemFile()
        {
            sysFile.SetFileContent(currentPlaylist.GetFileName());
            sysFile.Save();
        }

        private void UpdateDisplays()
        {
            DISPLAY_PLAYLIST = currentPlaylist.GetPlaylist();

        }
    }
}
