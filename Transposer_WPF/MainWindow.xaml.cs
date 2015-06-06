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
        }

        private void GRID_TABCONTROL_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    transposer.LoadPlaylist(file);
                }
                
            }
        }

        private void GRID_TABCONTROL_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
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

        private void OPTIONS_BUTTON_LOAD_SONG_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                Song mySong = new Song(filename);
                DISPLAY_MAIN.Text = mySong.ToString();
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

        private void OPTIONS_BUTTON_CREATE_PLAYLIST_Click(object sender, RoutedEventArgs e)
        {
            OPTIONS_TEXTBOX_INPUT.Text = "";
            OPTIONS_CREATEPLAYLIST_MESSAGEBOX.Visibility = System.Windows.Visibility.Visible;
            
        }

        private void OPTIONS_BUTTON_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            OPTIONS_CREATEPLAYLIST_MESSAGEBOX.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void OPTIONS_BUTTON_ACCEPT_Click(object sender, RoutedEventArgs e)
        {
            transposer.CreateNewPlaylist(OPTIONS_TEXTBOX_INPUT.Text);
            OPTIONS_CREATEPLAYLIST_MESSAGEBOX.Visibility = System.Windows.Visibility.Hidden;
            UpdateDisplays();

        }

    }
}
