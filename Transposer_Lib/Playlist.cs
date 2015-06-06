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
        File playlistFile = new File();
        public static string playlistDIR = System.IO.Directory.GetCurrentDirectory() + "\\Playlists";
        public Playlist()
        {
            playlistFile.Load(playlistDIR + "\\" + "default.slist");
            List<string> songFiles = playlistFile.GetFileContent();
            LoadFromList(songFiles);
        }
        public Playlist(string filename) 
        {
            playlistFile.Load(filename);
            playlistFile.SetDirectory(playlistDIR);
            playlistFile.Save();
            List<string> songFiles = playlistFile.GetFileContent();
            LoadFromList(songFiles);
        }

        private void LoadFromList(List<string> songFileNames)
        {
            foreach(string fileName in songFileNames){
                Song currentSong = new Song(Song.songDIR + "\\" + fileName);
                songList.Add(currentSong);
            }
        }

        private void ClearLists()
        {
            songList.Clear();
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

        public Song GetSong(int index)
        {
            Song song;
            if (index >= 0 && index < songList.Count())
            {
                song = songList[index];
            }
            else if(songList.Count() != 0)
            {
                song = songList.First();
            }
            else
            {
                song = new Song("default.txt");
            }

            return song;
        }
        public Song FirstSong()
        {
            if (!(songList.Count() == 0))
            {
                return songList.First();
            }

            return new Song("default.txt");
           
        }
        public void SaveState()
        {
            List<string> songFiles = new List<string>();
            foreach (Song song in songList)
            {
                song.SaveTransposed();
                string currentFileName = song.GetFileName();
                songFiles.Add(currentFileName);
            }

            playlistFile.SetFileContent(songFiles);
            playlistFile.Save();
        }
    }
}
