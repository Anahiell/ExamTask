using GALAXIAN;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Drawing2D;

internal class Level
{
    public int CharacterPositionX { get; set; }
    public int CharacterPositionY { get; set; }
    private TileGraphics player; // позиция игрока

    private HashSet<TileGraphics> enemyse;// Используем HashSet, почти тоже самое что и лист, но нельзя добавлять одинаковые

    private int enemyMoveDirection = 1; // 1 для движения вправо, -1 для движения влево
    private int enemyMoveSpeedDown = 1;

    int height;
    int width;

    private Bullets bullet;//Пуля
    private bool isBulletFired = false; // Переменная для отслеживания состояния пули

    private int currentLevel = 1; // начальный уровень
    private int initialEnemyCount = 5; // начальное количество врагов

    private Timer graphicsUpdateTimer; // таймер для графики

    public static Random r = new Random();

    public Form parent;

    public Level(Form parent, int width, int height, int selectedTheme)
    {
        this.width = width;
        this.height = height;
        this.parent = parent;
        TileGraphics.SetTheme(selectedTheme);

        CharacterPositionX = 7;
        CharacterPositionY = height - 3;
        player = new TileGraphics(TileGraphics.GraphicType.PLAYER);
        player.Pos = new Point(7, height - 3);

        enemyse = new HashSet<TileGraphics>(); 

        for (int i = 0; i < initialEnemyCount; i++)
        {
            TileGraphics enemy = new TileGraphics(TileGraphics.GraphicType.ENEMY); 
            enemyse.Add(enemy); 
        }
        graphicsUpdateTimer = new Timer();
        graphicsUpdateTimer.Interval = 100; // установите желаемый интервал обновления
        graphicsUpdateTimer.Tick += GraphicsUpdateTimer_Tick;
        graphicsUpdateTimer.Start();
        Generate();
    }
    public void DrawEnemies(Graphics g)
    {
        foreach (var enemy in enemyse)
        {
            // Проверка и коррекция положения врага, чтобы они не выходили за границы формы
            if (enemy.Pos.X < 0)
            {
                enemy.Pos = new Point(0, enemy.Pos.Y);
            }
            else if (enemy.Pos.X > width - enemy.Width)
            {
                enemy.Pos = new Point(width - enemy.Width, enemy.Pos.Y);
            }
            if (TileGraphics.Theme == 2)
            {
                Image enemyImage = enemy.texture;

                // созаем матрицу вращения
                Matrix rotationMatrix = new Matrix();
                rotationMatrix.RotateAt(enemy.rotationAngle, new PointF(enemy.Pos.X + enemy.Width / 2, enemy.Pos.Y + enemy.Height / 2));

                // применяем матрицу
                g.Transform = rotationMatrix;

                // рисуем
                g.DrawImage(enemyImage, enemy.Pos.X, enemy.Pos.Y, enemy.Width, enemy.Height);

                // сброс
                g.ResetTransform();
            }
            else
            {
                g.DrawImage(enemy.texture, enemy.Pos.X, enemy.Pos.Y, enemy.Width, enemy.Height);
            }
            g.DrawImage(enemy.texture, enemy.Pos.X, enemy.Pos.Y, enemy.Width, enemy.Height);
        }
    }
    private void DrawPlayer(Graphics g)
    {
        g.DrawImage(player.texture, player.Pos.X, player.Pos.Y, player.Width, player.Height);
    }

    public void Draw(Graphics g)
    {
        // Отрисовка врагов
        DrawEnemies(g);

        // Отрисовка игрока
        DrawPlayer(g);

        if (bullet != null)
        {
            g.DrawImage(bullet.GetImage(), bullet.X, bullet.Y);
        } 
    }
    void Generate()
    {
        foreach (var enemy in enemyse)
        {
            int randomX, randomY;
            do
            {
                randomX = r.Next(0, width);
                randomY = r.Next(0, height / 2);
            } while (enemyse.Any(e => Math.Abs(e.Pos.X - randomX) < enemy.Width && Math.Abs(e.Pos.Y - randomY) < enemy.Height));

            enemy.Pos = new Point(randomX, randomY);
        }
        player.Pos = new Point(CharacterPositionX * player.Height, height - player.Width - 60);
    }

    private void MoveEnemies()
    {
        foreach (var enemy in enemyse)
        {
            // Случайное движение влево или вправо
            int randomMove = r.Next(2);
            if (randomMove == 0)
            {
                enemy.Pos = new Point(enemy.Pos.X + 5, enemy.Pos.Y);
            }
            else
            {
                enemy.Pos = new Point(enemy.Pos.X - 5, enemy.Pos.Y);
            }

            // Проверка и корректировка положения врага по горизонтали
            if (enemy.Pos.X <= 0 || enemy.Pos.X >= width - enemy.Width)
            {
                enemyMoveDirection *= -1; 
                enemy.Pos = new Point(enemy.Pos.X, enemy.Pos.Y);
            }

            // случайное мув вниз
            int randomDown = r.Next(2);
            if (randomDown == 0)
            {
                enemy.Pos = new Point(enemy.Pos.X, enemy.Pos.Y + enemyMoveSpeedDown);
            }
            enemy.rotationAngle += 5;
        }
    }

    private void NextLevel()
    {
        currentLevel++; // Увеличиваем уровень

        // Увеличиваем количество врагов и их скорость
        initialEnemyCount += 2; 
        enemyMoveSpeedDown += 1; 

        // Создаем новых врагов
        for (int i = 0; i < initialEnemyCount; i++)
        {
            TileGraphics enemy = new TileGraphics(TileGraphics.GraphicType.ENEMY); // Ckplftv trptvgkzh dfhuf
            enemyse.Add(enemy); // lj,fdkztv tuj
        }

        // utythbhetv yfxfkmye. gjpbwb.z
        Generate();
    }

    public void Update()
    {
        MoveEnemies();
        if (bullet != null)
        {
            // Обновление положения пули
            bullet.Update();
            // Проверка, достигла ли пуля края карты
            if (bullet != null && bullet.Y < 0)
            {
                bullet = null; // Удаление пули
                isBulletFired = false;
            }
        }
        CheckCollisions();
    }
    private void Lost()
    {
        graphicsUpdateTimer.Stop(); // Остановка игрового таймера

        DialogResult result = MessageBox.Show("Вы проиграли! Хотите начать заново?", "Поражение", MessageBoxButtons.YesNo);

        if (result == DialogResult.Yes)
        {
            Generate();
            graphicsUpdateTimer.Start();
        }
        else
        {
            // Закрыть форму
            parent.Close();
        }
    }
    private void FireBullet()
    {
        bullet = new Bullets(player.Pos.X + player.Width / 2, player.Pos.Y - 1);
    }
    private void CheckCollisions()
    {

        Rectangle playerRect = new Rectangle(player.Pos.X, player.Pos.Y, player.Width, player.Height);

        // Проверка столкновений пуль с врагами
        foreach (var enemy in enemyse.ToList())
        {
            Rectangle enemyRect = new Rectangle(enemy.Pos.X, enemy.Pos.Y, enemy.Width, enemy.Height);
            if (bullet != null)
            {
                Rectangle bulletRect = new Rectangle(bullet.X, bullet.Y, bullet.GetImage().Width, bullet.GetImage().Height);
                if (bulletRect.IntersectsWith(enemyRect))
                {
                    enemyse.Remove(enemy);
                    bullet = null;
                    isBulletFired = false;
                    break;
                }
            }
        }

        // Проверка столкновения игрока с врагами
        foreach (var enemy in enemyse.ToList())
        {
            Rectangle enemyRect = new Rectangle(enemy.Pos.X, enemy.Pos.Y, enemy.Width, enemy.Height);
            if (enemyRect.IntersectsWith(playerRect))
            {
                Lost();
                break;
            }
        }

        // Проверка столкновения врагов с нижней границей
        foreach (var enemy in enemyse.ToList())
        {
            if (enemy.Pos.Y + enemy.Height >= height)
            {
                enemyse.Remove(enemy);
                Lost();
                break;
            }
        }
    }
    private void GraphicsUpdateTimer_Tick(object sender, EventArgs e)
    {
        Update();
        parent.Invalidate(); // Перерисовать форму
        if (enemyse.Count == 0)
        {
            NextLevel();
        }
    }
    public void MovePlayer(Keys type)
    {
        int moveDistance = 20; // Расстояние перемещения игрока

        switch (type)
        {
            case Keys.Left:
                {
                    if (player.Pos.X - moveDistance >= 0) // Проверка на выход за левую границу
                    {
                        player.Pos = new Point(player.Pos.X - moveDistance, player.Pos.Y);
                    }
                    break;
                }
            case Keys.Right:
                {
                    if (player.Pos.X + player.Width + moveDistance <= width - 20) 
                    {
                        player.Pos = new Point(player.Pos.X + moveDistance, player.Pos.Y);
                    }
                    break;
                }
            case Keys.Up:
                {
                    if (player.Pos.Y - moveDistance >= 0) 
                    {
                        player.Pos = new Point(player.Pos.X, player.Pos.Y - moveDistance);
                    }
                    break;
                }
            case Keys.Down:
                {
                    if (player.Pos.Y + player.Height + moveDistance <= height - 20) 
                    {
                        player.Pos = new Point(player.Pos.X, player.Pos.Y + moveDistance);
                    }
                    break;
                }
            case Keys.Space:
            case Keys.F:
                {
                    if (!isBulletFired)
                    {
                        FireBullet(); // Создаем пулю
                        isBulletFired = true;
                    }
                    break;
                }
            default:
                break;
        }
        parent.Invalidate();
    }
}