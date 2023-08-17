using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GALAXIAN
{
    public partial class Form2 : Form
    {
        private Form1 parentForm;
        private int selectTheme;
        public Form2(GALAXIAN.Form1 parent,int selectTheme)
        {
            InitializeComponent();
            
                this.selectTheme = selectTheme;
            Options();
            StartGame();
            parentForm = parent;
           
        }
        // размеры лабиринта в ячейках 32х32 пикселей
        int columns = 13;
        int rows = 22;

        int pictureSize = 32; // ширина и высота одной ячейки

        Level l; // ссылка на логику всего происходящего в игре

        public void Options()
        {
            Text = "GALAXIAN the game";


            // размеры клиентской области формы (того участка, на котором размещаются ЭУ)
            ClientSize = new Size((columns * pictureSize), rows * pictureSize);

            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartGame()
        {
            l = new Level(this, columns, rows,selectTheme);
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
                l.MovePlayer(e.KeyCode);
        }
    }
}
