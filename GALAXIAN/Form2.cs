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


        public Form2(GALAXIAN.Form1 parent, int selectedTheme)
        {
            InitializeComponent();

            this.selectedTheme = selectedTheme;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeGame();

            parentForm = parent;
        }
        private void InitializeGame()
        {
            // экземпляра класса Level
            gameLevel = new Level(this, this.Width, this.Height, selectedTheme);
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
    }
}
