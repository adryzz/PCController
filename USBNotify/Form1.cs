using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using Dolinay;

namespace USBNotify
{
    public partial class Form1 : Form
    {

        private DriveDetector driveDetector = null;

        string data = File.ReadAllText("USBConfig.txt");
        public Form1()
        {
            InitializeComponent();
            driveDetector = new DriveDetector();
            driveDetector.DeviceArrived += new DriveDetectorEventHandler(OnDriveArrived);
            driveDetector.DeviceRemoved += new DriveDetectorEventHandler(OnDriveRemoved);
            driveDetector.QueryRemove += new DriveDetectorEventHandler(OnQueryRemove);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            int x, y;
            x = SystemInformation.VirtualScreen.Width - 310;
            y = 10;

            this.Location = new Point(x, y);
        }

        // Called by DriveDetector when removable device in inserted
        private void OnDriveArrived(object sender, DriveDetectorEventArgs e)
        {
            // e.Drive is the drive letter, e.g. "E:\\"
            // If you want to be notified when drive is being removed (and be
            // able to cancel it),
            // set HookQueryRemove to true
            e.HookQueryRemove = true;
            if (data.Contains("OnInsert = SHOW"))
            {
                label1.Text = "USB device attached!" + " Drive: " + e.Drive;
                this.Opacity = 100;
            }
        }

        // Called by DriveDetector after removable device has been unplugged
        private void OnDriveRemoved(object sender, DriveDetectorEventArgs e)
        {
            if (data.Contains("OnRemove = SHOW"))
            {
                label1.Text = "USB device detached!" + " Drive: " + e.Drive;
                this.Opacity = 100;
            }
            
        }

        // Called by DriveDetector when removable drive is about to be removed
        private void OnQueryRemove(object sender, DriveDetectorEventArgs e)
        {
            
            if (data.Contains("OnQueryRemove = ASK"))
            {
                DialogResult res = MessageBox.Show(e.Drive + " will be detached. Continue?", "Windows", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (data.Contains("OnQueryRemove = ALLOW"))
            {
                e.Cancel = false;
            }
            else if (data.Contains("OnQueryRemove = NOT_ALLOW"))
            {
                e.Cancel = true;
            }
            
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);        // call default p

            if (driveDetector != null)
            {
                driveDetector.WndProc(ref m);
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
