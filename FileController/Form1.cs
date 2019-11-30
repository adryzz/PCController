using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileController
{
    public partial class Form1 : Form
    {
        List<FileStream> files = new List<FileStream>();
        string data = File.ReadAllText("FileConfig.txt");
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int x, y;
            x = SystemInformation.VirtualScreen.Width - 310;
            y = 10;

            this.Location = new Point(x, y);
            fileSystemWatcher1.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void FileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            files.Add(File.Open(e.FullPath, FileMode.OpenOrCreate));
            if (data.Contains("ShowDialog = TRUE"))
            {
                label1.Text = "File retrieved! File: " + e.Name;
                this.Opacity = 100;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Opacity = 0;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
