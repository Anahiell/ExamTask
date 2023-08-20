using GALAXIAN;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GALAXIAN
{
    public partial class Form1 : Form
    {
        private bool isMusicPlaying = true; // По умолчанию музыка включена
        public Form1()
        {
            InitializeComponent();
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GALAXIAN.Form2 form2 = new GALAXIAN.Form2(this, selectedTheme); // Передаем this (Form1) как родительскую форму
            form2.Show(); // Показываем Form2
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button_Sond_Click(object sender, EventArgs e)
        {
            if (isMusicPlaying)
            {
                // Остановить музыку
                // Ваш код для остановки музыки
                isMusicPlaying = false;
                button_Sond.BackgroundImage = Properties.Resources.noizOFF;
            }
            else
            {
                // Включить музыку
                // Ваш код для включения музыки
                isMusicPlaying = true;
                button_Sond.BackgroundImage = Properties.Resources.noizON;
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