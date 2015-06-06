﻿using System;
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
                currentPlaylist = new Playlist(Playlist.playlistDIR + "\\" + activePlaylist);
                currentSong = currentPlaylist.FirstSong();
            }
            else
            {
                currentPlaylist = new Playlist(songLibraryPath);
            }
            library = new Playlist(songLibraryPath);
            UpdateDisplays();
        }

        public void TransposeUp()
        {
            try{
                currentSong.TransposeUp();
                DISPLAY_MAIN = currentSong.ToString();
            }
            catch{

            }
            
        }

        public void TransposeDown()
        {
            try
            {
                currentSong.TransposeDown();
                DISPLAY_MAIN = currentSong.ToString();
            }
            catch
            {

            }
        }

        public void Next()
        {
            try
            {
                currentIdx = (currentIdx + 1 + currentPlaylist.Count()) % currentPlaylist.Count();
                currentSong = currentPlaylist.GetSong(currentIdx);
                UpdateDisplays();
            }
            catch
            {

            }
            
        }

        public void Previous()
        {
            try
            {
                currentIdx = (currentIdx - 1 + currentPlaylist.Count()) % currentPlaylist.Count();
                currentSong = currentPlaylist.GetSong(currentIdx);
                UpdateDisplays();
            }
            catch
            {

            }
            
        }

        public void LoadPlaylist(string playlistFileName)
        {
            currentPlaylist = new Playlist(playlistFileName);
            currentSong = currentPlaylist.FirstSong();
            UpdateDisplays();
            UpdateSystemFile();
        }

        private void UpdateSystemFile()
        {
            sysFile.SetFileContent(currentPlaylist.GetFileName());
            sysFile.Save();
        }

        private void UpdateDisplays()
        {
            try
            {
                DISPLAY_PLAYLIST = currentPlaylist.GetPlaylist();
                DISPLAY_MAIN = currentSong.ToString();
            }
            catch (Exception e) { }
            
        }

        public string GetPlaylistFilePath()
        {
            string output = Playlist.playlistDIR + "\\" + currentPlaylist.GetFileName();
            return output;
        }
        public string GetCurrentSongFilePath()
        {
            return currentSong.GetFilePath();
        }
        public void SaveState()
        {
            currentPlaylist.SaveState();
            UpdateDisplays();
        }

        public void CreateNewPlaylist(string fileName)
        {
            currentPlaylist = new Playlist(Playlist.playlistDIR + "\\" + fileName);
            UpdateSystemFile();
        }
    }
}
