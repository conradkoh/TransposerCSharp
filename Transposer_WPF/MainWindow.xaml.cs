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
        Transposer transposer = new Transposer();
        public MainWindow()
        {
            InitializeComponent();
            MaintainDirectories();
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
                    DISPLAY_FEEDBACK.Text = file;
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
            dialog.ShowDialog();
            string filename = dialog.FileName;
            transposer.LoadPlaylist(filename);
            UpdateDisplays();
        }

        private void OPTIONS_BUTTON_LOAD_SONG_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            dialog.ShowDialog();
            string filename = dialog.FileName;
            Song mySong = new Song(filename);
            DISPLAY_MAIN.Text = mySong.ToString();
        }
    }
}
