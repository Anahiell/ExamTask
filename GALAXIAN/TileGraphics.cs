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
        public enum GraphicType { ENEMY_1, PLAYER_1, SPACE, BLOCK, BULLET };


        public static Bitmap[] var1 = {
            Properties.Resources.enemy_1,
            Properties.Resources.player_1,
            Properties.Resources.space,
            Properties.Resources.block,
            Properties.Resources.bullet
        };
        public static Bitmap[] var2 = {
            Properties.Resources.enemy_2,
            Properties.Resources.player_2,
            Properties.Resources.space,
            Properties.Resources.block,
            Properties.Resources.bullet
        };
        public static Bitmap[] images = var1;
        public GraphicType type;
        public int Width { get; } = 32;
        public int Height { get; } = 32;
        public Image texture;
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
                        break;
                    }
                case 2:
                    {
                        images = var2;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}

