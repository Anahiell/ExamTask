using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GALAXIAN
{
    public partial class Form2 : Form
    {
        private Form1 parentForm;
        private int selectedTheme; // Переменная для выбранной темы
        private Level gameLevel;

        private SoundManager soundManager;
        public Form2(GALAXIAN.Form1 parent, int selectedTheme, SoundManager soundManager)
        {
            InitializeComponent();

            this.selectedTheme = selectedTheme;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            parentForm = parent;

            this.soundManager = soundManager; // Передаем soundManager из Form1

            InitializeGame();
        }
        private void InitializeGame()
        {
            // экземпляра класса Level
            gameLevel = new Level(this, this.Width, this.Height, selectedTheme,soundManager);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            gameLevel.Draw(g);
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Show();
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            gameLevel.MovePlayer(e.KeyCode);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
          
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (gameLevel.play_paus == true)
            {
                toolStripButton1.Image = new Bitmap(Properties.Resources.play);
                gameLevel.Paus();
            }
            else
            {
                toolStripButton1.Image = new Bitmap(Properties.Resources.pause);
                gameLevel.Play();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (soundManager.IsSoundEnabled == false)
            {
                soundManager.IsSoundEnabled = true;
                toolStripButton2.Image = new Bitmap(Properties.Resources.noizON);
                soundManager.PlaySound(SoundManager.SoundType.Music);
                soundManager.SetVolume(1);
            }
            else
            {
                soundManager.IsSoundEnabled = false;
                toolStripButton2.Image = new Bitmap(Properties.Resources.noizOFF);
                soundManager.SetVolume(0);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
