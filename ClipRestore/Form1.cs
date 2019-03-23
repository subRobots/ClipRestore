using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace ClipRestore
{
    public partial class Form1 : Form
    {
        string appPath = Environment.CurrentDirectory;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadHistory();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void tmrClipCheck_Tick(object sender, EventArgs e)
        {
            // txtClip.Text = Clipboard.GetText();
            // imgClip.Image = Clipboard.GetImage();

            checkClipboard();
        }

        private void checkClipboard()
        {
            string tempText = Clipboard.GetText();
            Image tempImage = Clipboard.GetImage();

            // Text check
            if (tempText != txtClip.Text) 
            {
                txtClip.Text = tempText;
                saveText();
                loadHistory();
            }
            else
            {
                txtClip.Text = txtClip.Text;
            }

            // Image check
            if (tempImage != imgClip.Image)
            {
                imgClip.Image = tempImage;
                loadHistory();

            }
            else
            {
                imgClip.Image = imgClip.Image;
            }

            
        }

        private void saveText()
        {
      
            string theDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // @"C:\"  
            File.WriteAllText(appPath + "\\history\\" +  theDate + ".txt", txtClip.Text);
        }

        private void saveImage()
        {

        }

        private void loadHistory()
        {
            lstHistory.Items.Clear();
            string[] txtHistory = Directory.GetFileSystemEntries(appPath + "\\history\\", "*.txt");
            
            lstHistory.Items.AddRange(txtHistory);
        }

        private void lstHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string text = File.ReadAllText(lstHistory.SelectedItem.ToString());
                txtPreview.Text = text;
            }

            catch
            {
                Console.Write("Could not load text");
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
