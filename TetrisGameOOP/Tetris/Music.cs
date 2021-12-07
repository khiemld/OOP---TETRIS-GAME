using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Tetris
{
    public partial class Music : Form
    {
        public WindowsMediaPlayer music = new WindowsMediaPlayer();
        public Music()
        {
            InitializeComponent();
        }

        private void Music_Load(object sender, EventArgs e)
        {
            listBoxMusic.Items.Add("Tetris Original");
            listBoxMusic.Items.Add("For Foever - Hua Chen Yu");
            listBoxMusic.Items.Add("The Spectre - Alan Walker");
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if((string)listBoxMusic.SelectedItem == "Tetris Original")
            {
                music.URL = "TETRIS Original Track.mp3";
            }
            else if((string)listBoxMusic.SelectedItem == "For Foever - Hua Chen Yu")
            {
                music.URL = "For Forever - Hua Chen Yu.mp3";
            }
            else if((string)listBoxMusic.SelectedItem=="The Spectre - Alan Walker")
            {
                music.URL = "Alan Walker - The Spectre.mp3";
            }
            music.settings.setMode("loop", true);
            music.controls.play();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {

            if (buttonPause.Text == "Resume")
            {
                music.controls.play();
                buttonPause.Text = "Pause";
            }
            else
            {
                music.controls.pause();
                buttonPause.Text = "Resume";
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            music.controls.stop();
        }

        string[] paths, files;
        private void listBoxMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMusic.SelectedIndex > 2)
            {
                music.URL = paths[listBoxMusic.SelectedIndex - 3];
            }
        }

        private void Music_Leave(object sender, EventArgs e)
        {

        }

        private void Music_FormClosing(object sender, FormClosingEventArgs e)
        {
            music.controls.stop();
        }

        private void buttonAddMusic_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files = ofd.SafeFileNames;
                paths = ofd.FileNames;
                for(int i = 0; i < files.Length; i++)
                {
                    listBoxMusic.Items.Add(files[i]); 
                }
            }
            buttonAddMusic.Visible = false;
        }
    }
}

