using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transposer_Lib
{
    public class Playlist
    {
        List<Song> songList = new List<Song>();
        List<string> songFiles = new List<string>();
        File playlistFile = new File();
        public static string playlistDIR = System.IO.Directory.GetCurrentDirectory() + "\\Playlists";
        public Playlist()
        {
            playlistFile.Load(playlistDIR + "\\" + "default.slist");
            songFiles = playlistFile.GetFileContent();
            LoadFromList(songFiles);
        }
        public Playlist(string filename) 
        {
            playlistFile.Load(filename);
            playlistFile.SetDirectory(playlistDIR);
            //playlistFile.SetDirectory(playlistDIR);
            //string dbg = playlistFile.GetFilePath();
            //playlistFile.Save();
            songFiles = playlistFile.GetFileContent();
            LoadFromList(songFiles);
        }

        private void LoadFromList(List<string> songFileNames)
        {
            foreach(string fileName in songFileNames){
                Song currentSong = new Song(fileName);
                songList.Add(currentSong);
            }
        }

        private void ClearLists()
        {
            songList.Clear();
            songFiles.Clear();
        }

        public string GetPlaylist()
        {
            List<string> output = playlistFile.GetFileContent();
            string result = String.Join(System.Environment.NewLine, output);
            return result;
        }

        public string GetFileName()
        {
            return playlistFile.GetFileName();
        }

        public int Count()
        {
            return songList.Count();
        }

        public Song FirstSong()
        {
            return songList.First();
        }
    }
}
