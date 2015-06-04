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
        }

        private void BUTTON_TRANSPOSE_UP_Click(object sender, RoutedEventArgs e)
        {
            transposer.TransposeUp();
            UpdateDisplays();
        }

        private void UpdateDisplays()
        {
            DISPLAY_MAIN.Text = transposer.DISPLAY_MAIN;
            DISPLAY_PLAYLIST.Text = transposer.DISPLAY_PLAYLIST;
        }

        private void OPTIONS_BUTTON_LOAD_SONGLIST_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.ShowDialog();
            string filename = dialog.FileName;
            transposer.LoadPlaylist(filename);
            UpdateDisplays();
        }
    }
}
