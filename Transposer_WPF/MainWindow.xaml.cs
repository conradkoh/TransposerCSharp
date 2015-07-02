using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Transposer_Lib;
namespace Transposer_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Transposer transposer;
        public MainWindow()
        {
            InitializeComponent();
            MaintainDirectories();
            transposer = new Transposer();
            UpdateDisplays();
            Search("");
            
        }

        private void BUTTON_TRANSPOSE_UP_Click(object sender, RoutedEventArgs e)
        {
            transposer.TransposeUp();
            UpdateDisplays();
        }
        private void MaintainDirectories()
        {
            string songDIR = Song.songDIR;
            string playlistDIR = Playlist.playlistDIR;
            string systemDIR = Transposer.systemDIR;
            System.IO.Directory.CreateDirectory(songDIR);
            System.IO.Directory.CreateDirectory(playlistDIR);
            System.IO.Directory.CreateDirectory(systemDIR);

        }
        private void UpdateDisplays()
        {
            DISPLAY_MAIN.Text = transposer.DISPLAY_MAIN;
            DISPLAY_PLAYLIST.Text = transposer.DISPLAY_PLAYLIST;
            OPTIONS_DISPLAY_PLAYLIST.Text = transposer.DISPLAY_PLAYLIST;
            Search("");
            
            
        }

        private void Search(string fileName)
        {
            string[] allFiles = transposer.allFiles;
            OPTIONS_LISTBOX_SEARCHRESULTS.Items.Clear();
            if (fileName == "")
            {
                
                foreach (string filePath in allFiles)
                {
                    OPTIONS_LISTBOX_SEARCHRESULTS.Items.Add(System.IO.Path.GetFileName(filePath));
                }

            }
            else
            {
                //string[] searchResults = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory() + "//Songs", fileName);
                foreach (string filePath in allFiles)
                {
                    if (System.IO.Path.GetFileName(filePath).ToLower().Contains(fileName.ToLower()))
                    {
                        OPTIONS_LISTBOX_SEARCHRESULTS.Items.Add(System.IO.Path.GetFileName(filePath));
                    }
                    
                }
            }

        }

        private void OPTIONS_BUTTON_LOAD_PLAYLIST_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                transposer.LoadPlaylist(filename);
                UpdateDisplays();
                TABCONTROL_MAIN.SelectedItem = TABCONTROL_MAIN_TAB_MAIN;
            }
            
            
        }

        private void BUTTON_TRANSPOSE_DOWN_Click(object sender, RoutedEventArgs e)
        {
            transposer.TransposeDown();
            UpdateDisplays();
        }

        private void MAIN_BUTTON_EDIT_PLAYLIST_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(transposer.GetPlaylistFilePath());
            }
            catch (Exception except)
            {

            }
            
        }

        private void MAIN_BUTTON_NEXT_Click(object sender, RoutedEventArgs e)
        {
            transposer.Next();
            UpdateDisplays();
        }

        private void MAIN_BUTTON_PREVIOUS_Click(object sender, RoutedEventArgs e)
        {
            transposer.Previous();
            UpdateDisplays();
        }

        private void MAIN_BUTTON_EDIT_SONG_Click(object sender, RoutedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(transposer.GetCurrentSongFilePath()); }
            catch { }
            
        }

        private void MAIN_BUTTON_SAVE_STATE_Click(object sender, RoutedEventArgs e)
        {
            transposer.SaveState();
            UpdateDisplays();
        }

        private void OPTIONS_BUTTON_PLAYLIST_DIRECTORY_Click(object sender, RoutedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(Playlist.playlistDIR); }
            catch { }
        }

        private void OPTIONS_BUTTON_SONG_DIRECTORY_Click(object sender, RoutedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(Song.songDIR); }
            catch { }

            
        }

        private void BUTTON_HOME_Click(object sender, RoutedEventArgs e)
        {
            TABCONTROL_MAIN.SelectedItem = TABCONTROL_MAIN_TAB_MAIN;
        }

        private void OPTIONS_BUTTON_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            OPTIONS_CREATE_FILE_MESSAGEBOX.Visibility = System.Windows.Visibility.Hidden;
            OPTIONS_TEXTBOX_INPUT.Text = "";
            
        }

        private void OPTIONS_BUTTON_CREATE_PLAYLIST_Click(object sender, RoutedEventArgs e)
        {
            if (OPTIONS_TEXTBOX_INPUT.Text != "")
            {
                string filePath = transposer.CreateNewPlaylist(OPTIONS_TEXTBOX_INPUT.Text);
                OPTIONS_CREATE_FILE_MESSAGEBOX.Visibility = System.Windows.Visibility.Hidden;
                UpdateDisplays();
                System.Diagnostics.Process.Start(filePath);
                OPTIONS_TEXTBOX_INPUT.Text = "";
            }
            

        }

        private void BUTTON_RELOAD_Click(object sender, RoutedEventArgs e)
        {
            transposer.Reload();
            UpdateDisplays();
        }

        private void OPTIONS_BUTTON_CREATE_SONG_Click(object sender, RoutedEventArgs e)
        {
            if (OPTIONS_TEXTBOX_INPUT.Text != "")
            {
                string fileName = OPTIONS_TEXTBOX_INPUT.Text;
                string filePath = transposer.CreateSong(fileName);
                OPTIONS_CREATE_FILE_MESSAGEBOX.Visibility = System.Windows.Visibility.Hidden;
                UpdateDisplays();
                System.Diagnostics.Process.Start(filePath);
                OPTIONS_TEXTBOX_INPUT.Text = "";
            }
            
        }

        private void OPTIONS_BUTTON_CREATE_FILE_Click(object sender, RoutedEventArgs e)
        {
            transposer.Reload();
            UpdateDisplays();
            if (OPTIONS_CREATE_FILE_MESSAGEBOX.Visibility == System.Windows.Visibility.Visible)
            {
                OPTIONS_CREATE_FILE_MESSAGEBOX.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                OPTIONS_CREATE_FILE_MESSAGEBOX.Visibility = System.Windows.Visibility.Visible;
            }
            OPTIONS_TEXTBOX_INPUT.Text = "";
        }

        private void OPTIONS_BUTTON_ADD_SONG_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                transposer.AddSong(filePath);
                //Song mySong = new Song(filename);
                //DISPLAY_MAIN.Text = mySong.ToString();
                UpdateDisplays();
                TABCONTROL_MAIN.SelectedItem = TABCONTROL_MAIN_TAB_MAIN;
            }
        }

        private void DISPLAY_PLAYLIST_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    transposer.LoadPlaylist(file);
                }
                transposer.Reload();
                UpdateDisplays();

            }
        }

        private void DISPLAY_MAIN_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    transposer.AddSong(file);
                }
                UpdateDisplays();

            }
        }

        private void DISPLAY_MAIN_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
        }

        private void DISPLAY_PLAYLIST_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
        }

        private void OPTIONS_GRID_FILE_DROP(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                System.ComponentModel.BackgroundWorker loader = new System.ComponentModel.BackgroundWorker();
                System.Threading.Thread.CurrentThread.IsBackground = true;
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    transposer.AddSong(file);
                }
                transposer.Reload();
                UpdateDisplays();

            }
        }

        private void BUTTON_INCREASE_FONT_SIZE_Click(object sender, RoutedEventArgs e)
        {
            DISPLAY_MAIN.FontSize = DISPLAY_MAIN.FontSize + 1;
        }

        private void BUTTON_DECREASE_FONT_SIZE_Click(object sender, RoutedEventArgs e)
        {
            if (DISPLAY_MAIN.FontSize > 1)
            {
                DISPLAY_MAIN.FontSize = DISPLAY_MAIN.FontSize - 1;
            }
            
        }

        private void MAIN_GRID_SONGDROP_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    transposer.AddSong(file);
                }
                UpdateDisplays();

            }
        }

        private void MAIN_GRID_SONGDROP_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
        }

        private void OPTIONS_BUTTON_ADD_SELECTED_Click(object sender, RoutedEventArgs e)
        {
            System.Collections.IList selectedItems = OPTIONS_LISTBOX_SEARCHRESULTS.SelectedItems;
            foreach (Object item in selectedItems)
            {
                transposer.AddExistingSong(item.ToString());
            }
            UpdateDisplays();
        }

        private void OPTIONS_DISPLAY_SEARCHRESULTS_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search(OPTIONS_DISPLAY_SEARCHRESULTS.Text);
        }

        private void OPTIONS_BUTTON_CLEARPLAYLIST_Click(object sender, RoutedEventArgs e)
        {
            transposer.ClearPlaylist();
            UpdateDisplays();
        }

    }
}
