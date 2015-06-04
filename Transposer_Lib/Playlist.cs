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

        public Playlist() { }

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
    }
}
