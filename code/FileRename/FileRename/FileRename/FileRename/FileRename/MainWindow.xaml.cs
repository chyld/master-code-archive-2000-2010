using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;

namespace FileRename
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonStandard_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ProcessFiles(dlg.SelectedPath, null);
        }

        private void buttonRecursive_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ProcessDirectories(dlg.SelectedPath);
        }

        private void buttonNewDate_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ProcessFiles(dlg.SelectedPath, textboxNewDate.Text);
        }

        private void ProcessDirectories(String directory)
        {
            ProcessFiles(directory, null);

            foreach (var dirIn in Directory.EnumerateDirectories(directory).ToList())
                ProcessDirectories(dirIn);
        }

        private void ProcessFiles(String directory, String newdate)
        {
            Int64 counter;

            // temporary reset of file names to prevent duplicate collisions
            counter = 0;
            foreach (var fileIn in Directory.EnumerateFiles(directory).ToList())
            {
                var fileInfo = new FileInfo(fileIn);
                fileInfo.LastWriteTime = (newdate != null) ? DateTime.Parse(newdate) : fileInfo.LastWriteTime;

                var fileOut = String.Format("{0}\\{1:yyyy.MMdd}.reset.{2:X4}{3}", fileInfo.DirectoryName, fileInfo.LastWriteTime, counter, fileInfo.Extension).ToLower();
                File.Move(fileIn, fileOut);

                counter++;
            }

            // use actual file names
            counter = 0;
            foreach (var fileIn in Directory.EnumerateFiles(directory).ToList())
            {
                var fileInfo = new FileInfo(fileIn);

                var fileOut = String.Format("{0}\\{1:yyyy.MMdd}.{2:X4}{3}", fileInfo.DirectoryName, fileInfo.LastWriteTime, counter, fileInfo.Extension).ToLower();
                File.Move(fileIn, fileOut);

                counter++;
            }

            // delete any duplicate files
            var filedict = new Dictionary<String, String>();

            foreach (var file in Directory.EnumerateFiles(directory).ToList())
            {
                var fi = new FileInfo(file);
                var key = fi.LastWriteTime.ToString() + "-" + fi.Length.ToString();

                if (!filedict.ContainsKey(key))
                    filedict.Add(key, file);
                else
                    File.Delete(file);
            }
        }
    }
}
