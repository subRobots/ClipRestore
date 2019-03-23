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

namespace ClipRestore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            }
            else
            {
                txtClip.Text = txtClip.Text;
            }

            // Image check
            if (tempImage != imgClip.Image)
            {
                imgClip.Image = tempImage;
                
            }
            else
            {
                imgClip.Image = imgClip.Image;
            }

        }

        private void saveText()
        {
            //DateTime CurrentDate;
            // CurrentDate = DateTime.Now;
            string theDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // @"C:\" + 
            File.WriteAllText(theDate + ".txt", txtClip.Text);
        }
    }
}
