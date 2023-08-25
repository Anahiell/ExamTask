using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GALAXIAN
{
    internal class TileGraphics
    {
        public enum GraphicType { ENEMY, PLAYER, BULLET };

        public static Bitmap[] var1 = {
            Properties.Resources.enemy_1,
            Properties.Resources.player_1,
            Properties.Resources.bullet1
        };
        public static Bitmap[] var2 = {
            Properties.Resources.enemy_2,
            Properties.Resources.player_2,
            Properties.Resources.bullet2
        };

        public static Bitmap[] images = var1;
        public GraphicType type;
        public int Width { get; } = 50;//настройка размера картинки
        public int Height { get; } = 50;//такаяже настройка
        public Image texture;
        public static int Theme;
        public float rotationAngle = 0.0f;
        public Point Pos { get; set; }
        private Point targetPos; // Целевая позиция для интерполяции

        public Point TargetPos
        {
            get { return targetPos; }
            set { targetPos = value; }
        }
        public int MovementDirection { get; set; } 

        public TileGraphics(GraphicType type)
        {
            this.type = type;
            texture = images[(int)type];
        }
        public static void SetTheme(int i)
        {
            switch (i)
            {
                case 1:
                    {
                        images = var1;
                        Theme = 1;
                        break;
                    }
                case 2:
                    {
                        images = var2;
                        Theme = 2;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}

