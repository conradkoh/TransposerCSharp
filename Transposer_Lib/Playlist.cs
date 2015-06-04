using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transposer_Lib
{
    class Playlist
    {
        List<Song> songList = new List<Song>();
        List<string> songFiles = new List<string>();
        File playlistFile;
        const string playlistDIR = ".\\Playlists";
        public Playlist()
        {
            playlistFile = new File(playlistDIR + "\\" + "default.slist");
            songFiles = playlistFile.GetFileContent();
            LoadFromList(songFiles);
        }
        public Playlist(string filename) 
        {
            playlistFile = new File(filename);
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

        public override string ToString()
        {
            List<string> songTitles = new List<string>();
            foreach (Song song in songList)
            {
                string currentSongTitle = song.GetTitle();
                songTitles.Add(currentSongTitle);
            }

            string output = String.Join("\n", songTitles);
            return output;
        }

        public string GetFileName()
        {
            return playlistFile.GetFileName();
        }
    }
}
