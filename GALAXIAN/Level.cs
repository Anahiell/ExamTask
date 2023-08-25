using GALAXIAN;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Drawing2D;

internal class Level
{
    public bool play_paus;
    public int CharacterPositionX { get; set; }
    public int CharacterPositionY { get; set; }
    private TileGraphics player; // позиция игрока

    private HashSet<TileGraphics> enemyse;// Используем HashSet, почти тоже самое что и лист, но нельзя добавлять одинаковые

    private int enemyMoveSpeedDown = 1;

    int height;
    int width;

    private Bullets bullet;//Пуля
    private int bulletSpeed = 10;
    private bool isBulletFired = false; // Переменная для отслеживания состояния пули

    private int currentLevel = 1; // начальный уровень
    private int initialEnemyCount = 5; // начальное количество врагов

    private Timer graphicsUpdateTimer; // таймер для графики

    public static Random r = new Random();

    public Form parent;

    private SoundManager soundManager;

    public Level(Form parent, int width, int height, int selectedTheme, SoundManager s)
    {
        play_paus = true;
        this.width = width;
        this.height = height;
        this.parent = parent;
        TileGraphics.SetTheme(selectedTheme);

        soundManager = s;

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
        parent.Text = $"Level: {currentLevel}";
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
            int randomMove = r.Next(5);
            int horizontalMove = 0;

            if (randomMove == 0 || randomMove == 1)
            {
                horizontalMove = 5; // Движение вправо
            }
            else if (randomMove == 2 || randomMove == 3)
            {
                horizontalMove = -5; // Движение влево
            }
            // Иначе остается без движения

            int newPosX = enemy.Pos.X + horizontalMove;

            // Проверка и корректировка положения врага по горизонтали
            if (newPosX <= 0)
            {
                newPosX = 0;
            }
            else if (newPosX >= width - enemy.Width)
            {
                newPosX = width - enemy.Width;
            }

            // Проверка на пересечение с другими врагами
            foreach (var otherEnemy in enemyse.Where(e => e != enemy))
            {
                Rectangle enemyRect = new Rectangle(newPosX, enemy.Pos.Y, enemy.Width, enemy.Height);
                Rectangle otherEnemyRect = new Rectangle(otherEnemy.Pos.X, otherEnemy.Pos.Y, otherEnemy.Width, otherEnemy.Height);

                if (enemyRect.IntersectsWith(otherEnemyRect))
                {
                    horizontalMove = 0; // Остановка движения
                    break;
                }
            }

            enemy.Pos = new Point(newPosX, enemy.Pos.Y);

            // Случайное движение вниз
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
        parent.Text = $"Level: {currentLevel}";
        bulletSpeed += 10;
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
            bullet.Update(bulletSpeed);
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

        DialogResult result = MessageBox.Show("You lost! Do you want to start over?", "Defeat", MessageBoxButtons.YesNo);

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
        soundManager.PlaySound(SoundManager.SoundType.Shot);
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
                    soundManager.PlaySound(SoundManager.SoundType.Enemy_die);
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
        if (play_paus)
        {
            Update();
            parent.Invalidate(); // Перерисовать форму
            if (enemyse.Count == 0)
            {
                NextLevel();
            }
        }
    }
    public void MovePlayer(Keys type)
    {
        if (!play_paus)
            return; // Если игра на паузе
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
    public void Paus()
    {
        graphicsUpdateTimer.Stop();
        play_paus = false;
    }
    public void Play()
    {
        if (!play_paus)
        {
            graphicsUpdateTimer.Start();
            play_paus = true;
            // Дополнительный код для возобновления логики игры
        }
    }
}