using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioSwitcher.AudioApi.CoreAudio;

namespace AudioController
{
    public partial class Form1 : Form
    {

        CoreAudioDevice device = new CoreAudioController().DefaultPlaybackDevice;
        int volume;
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
            volume = device.
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
