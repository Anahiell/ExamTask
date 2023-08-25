using GALAXIAN;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GALAXIAN
{
    public partial class Form1 : Form
    {
        private bool isMusicPlaying = false; // По умолчанию музыка включена
        private SoundManager soundManager;

        public Form1()
        {
            InitializeComponent();
            soundManager = new SoundManager(isMusicPlaying);
        }
        private int selectedTheme = 1;
        private void Form1_Load(object sender, EventArgs e)
        {
            Color transparentColor = Color.FromArgb(150, 150, 150, 250);

            button_start.FlatStyle = FlatStyle.Flat;
            button_Sond.FlatStyle = FlatStyle.Flat;
            button_EXIT.FlatStyle = FlatStyle.Flat;
            button_Theme.FlatStyle = FlatStyle.Flat;

            button_start.BackColor = transparentColor;
            button_Sond.BackColor = transparentColor;
            button_EXIT.BackColor = transparentColor;
            button_Theme.BackColor = transparentColor;

            button_start.Font = new Font("Arial", 12, FontStyle.Bold);
            button_Sond.Font = new Font("Arial", 12, FontStyle.Bold);
            button_EXIT.Font = new Font("Arial", 12, FontStyle.Bold);
            button_Theme.Font = new Font("Arial", 12, FontStyle.Bold);
            button_Theme.Text = $"THEME\n{selectedTheme}";

            trackBar1.BackColor = Color.Aqua;
            trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
            this.Text = "GALAXIAN the game";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GALAXIAN.Form2 form2 = new GALAXIAN.Form2(this, selectedTheme, soundManager);
            form2.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_Sond_Click(object sender, EventArgs e)
        {
            if (!isMusicPlaying)
            {
                soundManager.IsSoundEnabled = true;
                isMusicPlaying = true;
                button_Sond.BackgroundImage = new Bitmap(Properties.Resources.noizON);
                soundManager.PlaySound(SoundManager.SoundType.Music);
                trackBar1.Value = trackBar1.Maximum; // Устанавливаем значение трекбара в текущую громкость
                soundManager.SetVolume(1);
            }
            else
            {
                soundManager.IsSoundEnabled = false;
                isMusicPlaying = false;
                button_Sond.BackgroundImage = new Bitmap(Properties.Resources.noizOFF);
                soundManager.SetVolume(0);
                trackBar1.Value = 0; // Устанавливаем значение трекбара в 0
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value > 0)
            {
                isMusicPlaying = true;
                soundManager.SetVolume((double)trackBar1.Value / trackBar1.Maximum); // Установить громкость на основе значения трекбара
                button_Sond.BackgroundImage = new Bitmap(Properties.Resources.noizON);
            }
            else
            {
                isMusicPlaying = false;
                soundManager.SetVolume(0);
                button_Sond.BackgroundImage = new Bitmap(Properties.Resources.noizOFF);
            }
            if (isMusicPlaying == false && trackBar1.Value > 0)
            {
                isMusicPlaying = true;
                soundManager.PlaySound(SoundManager.SoundType.Music);
                soundManager.SetVolume((double)trackBar1.Value / trackBar1.Maximum); // Установить громкость на основе значения трекбара
                button_Sond.BackgroundImage = new Bitmap(Properties.Resources.noizON);
            }
        }

        private void button_Theme_Click(object sender, EventArgs e)
        {
            if (selectedTheme == 1)
            {
                selectedTheme = 2;
            }
            else if (selectedTheme == 2)
            {
                selectedTheme = 1;
            }
            button_Theme.Text = $"THEME\n{selectedTheme}";
        }
    }
}