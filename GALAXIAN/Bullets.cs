using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALAXIAN
{
    internal class Bullets
    {
        private System.Drawing.Image bulletImage;
        public int X { get; private set; }
        public int Y { get; set; }
        public Bullets(int startX, int startY)
        {
            X = startX;
            Y = startY;
            bulletImage = new Bitmap(Properties.Resources.bullet); // Сохраните изображение пули в поле
        }
        public void Update()
        {
            Y -= 10; //скорость пули
        }
        public System.Drawing.Image GetImage() 
        {
            return bulletImage;
        }
    }
}
