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
        private TileGraphics image = new TileGraphics(TileGraphics.GraphicType.BULLET);
        private System.Drawing.Image bulletImage;
        public int X { get; private set; }
        public int Y { get; set; }
        public Bullets(int startX, int startY)
        {
            X = startX;
            Y = startY;
            bulletImage = image.texture; // Сохраните изображение пули в поле
        }
        public void Update(int bulletSpeed)
        {
            Y -= bulletSpeed; //скорость пули
        }
        public System.Drawing.Image GetImage() 
        {
            return bulletImage;
        }
    }
}
