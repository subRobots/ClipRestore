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
        

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr newVIEW);
        private IntPtr _clipViewer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadHistory();
            _clipViewer = SetClipboardViewer(this.Handle);
        }

        protected override void WndProc(ref System.Windows.Forms.Message message)
        {
             switch (message.Msg)
            {
                case 0x308:
                    checkClipboard();
                    break;
                default:
                    base.WndProc(ref message);
                    break;
            }
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

           // checkClipboard();
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
            imgClip.Image = tempImage;            
            saveImage();

            //Update History list
            loadHistory();
            /* Image check
            if (tempImage != imgClip.Image)
            {
                imgClip.Image = tempImage;
                //saveImage(); Temporarily commented out as need to create compare image function.
                //loadHistory();

            }
            else
            {
                imgClip.Image = imgClip.Image;
            }*/


        }

        private void saveText()
        {
            string theDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // @"C:\"  
            File.WriteAllText(appPath + "\\history\\" +  theDate + ".txt", txtClip.Text);
        }

        private void saveImage() 
        {
            string theDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // Timestamp for filename
            if (imgClip.Image != null)
            {
                imgClip.Image.Save(appPath + "\\history\\" + theDate + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            
        }

        private void loadHistory()
        {
            lstHistory.Items.Clear();
            string[] txtHistory = Directory.GetFileSystemEntries(appPath + "\\history\\", "*.*"); // Add all files in 'History' Folder.
            
            lstHistory.Items.AddRange(txtHistory);
        }

        private void lstHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                string text = File.ReadAllText(lstHistory.SelectedItem.ToString());                
                
                if(lstHistory.SelectedItem.ToString().Contains(".txt"))
                {
                    txtPreview.Visible = true;
                    imgPreview.Visible = false;
                    txtPreview.Text = text;
                }

                if (lstHistory.SelectedItem.ToString().Contains(".jpg"))
                {
                    txtPreview.Visible = false;
                    imgPreview.Visible = true;
                    imgPreview.ImageLocation = lstHistory.SelectedItem.ToString();
                }
                else
                {
                    Console.WriteLine("Unsupported filetype");
                }
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
