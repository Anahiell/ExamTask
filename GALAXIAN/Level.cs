using GALAXIAN;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

internal class Level
{
    public int CharacterPositionX { get; set; }
    public int CharacterPositionY { get; set; }
    private Point playerPosition; // Текущая позиция игрока

    int height;
    int width;

    int enemyMoveDown;//враги приближаются
    int enemySpeed;//скорость врагов

    private Bullets bullet;//Пуля
    private bool isBulletFired = false; // Переменная для отслеживания состояния пули

    private int currentLevel = 1; // начальный уровень
    private int initialEnemyCount = 5; // начальное количество врагов

    private List<Point> enemyPositions; // Хранит позиции врагов
    private Timer enemyTimer; // Таймер для движения врагов
    private Timer graphicsUpdateTimer; // таймер для графики

    public TileGraphics[,] objects;
    public PictureBox[,] images;

    public static Random r = new Random();

    public Form parent;

    public Level(Form parent, int width, int height, int selectedTheme)
    {
        this.width = width;
        this.height = height;
        this.parent = parent;
        TileGraphics.SetTheme(selectedTheme);

        CharacterPositionX = 7;
        CharacterPositionY = height-3;
        playerPosition = new Point(7, height - 3);

        objects = new TileGraphics[this.height, this.width];
        images = new PictureBox[this.height, this.width];

        enemyMoveDown = 0;
        enemySpeed = 1500;

        graphicsUpdateTimer = new Timer();
        graphicsUpdateTimer.Interval = 33;
        graphicsUpdateTimer.Tick += GraphicsUpdateTimer_Tick;
        enemyTimer = new Timer();
        enemyTimer.Interval = enemySpeed;
        enemyTimer.Tick += EnemyTimer_Tick;

        Generate();
    }

    void Generate()
    {
        enemyPositions = new List<Point>(initialEnemyCount);
       
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                TileGraphics.GraphicType current = TileGraphics.GraphicType.SPACE;

                if (y < (height - 2) / 2)
                {
                    if (r.Next(20) == 0)
                    {
                        if (initialEnemyCount > enemyPositions.Count)
                        {
                            current = TileGraphics.GraphicType.ENEMY_1; // Вверху враги
                            enemyPositions.Add(new Point(x, y));//записываем позицию врага
                        }
                    }
                }
                else if (y == CharacterPositionY && x == CharacterPositionX)
                {
                    current = TileGraphics.GraphicType.PLAYER_1; // Игрок
                }
                objects[y, x] = new TileGraphics(current);
                images[y, x] = new PictureBox();
                images[y, x].Location = new Point(x * 32, y * 32);
                images[y, x].Size = new Size(32, 32);
                images[y, x].Parent = parent;
                images[y, x].BackgroundImage = objects[y, x].texture;
            }
        }
        enemyTimer.Interval = enemySpeed; // Интервал движения врагов и пули
        enemyTimer.Start();
        graphicsUpdateTimer.Interval = 33; // Примерно 30 кадров в секунду 
        graphicsUpdateTimer.Start();
    }
    private void Paus()
    {
        enemyTimer.Stop();
        graphicsUpdateTimer.Stop();
    }
    private void Resume()
    {
        enemyTimer.Start();
        graphicsUpdateTimer.Start();
    }
    private void Retry()
    {
        enemyMoveDown = 0;
        enemySpeed = 1500;
        initialEnemyCount = 5;
        foreach (var item in enemyPositions)
        {
            UpdateGraphics(item.Y, item.X,TileGraphics.GraphicType.SPACE);
        
        }
        enemyPositions = new List<Point>(initialEnemyCount);
        Generate();
    }
    private void NextLevel()
    {
        currentLevel += 1;
        initialEnemyCount += currentLevel; // увеличиваем количество врагов
        enemySpeed = Math.Max(enemySpeed - 100, 100); // увеличиваем скорость врагов
        
        for (int y = 0; y < height / 2; y++)
        {
            for (int x = 0; x < width; x++)
            {
                TileGraphics.GraphicType current = TileGraphics.GraphicType.SPACE;
                if (y < (height - 2) )
                {
                    
                    if (r.Next(20) == 0)
                    {
                        if (initialEnemyCount > enemyPositions.Count)
                        {
                            current = TileGraphics.GraphicType.ENEMY_1; // Вверху враги
                            enemyPositions.Add(new Point(x, y));//записываем позицию врага
                        }
                    }
                }
                objects[y, x] = new TileGraphics(current);
            }

        }
    }
    public void Update()
    {
        if (enemyPositions.Count == 0)
        {
            Paus();
            DialogResult result = MessageBox.Show($"Level {currentLevel}", "Next Level", MessageBoxButtons.OK);
            if (result == DialogResult.OK)
            {
                NextLevel();
                Resume();

            }
        }
        if (isBulletFired)
        {
            int prevBulletY = bullet.Y; // Сохраняем предыдущую позицию пули
            bullet.Y -= 1; // Перемещаем пулю вверх на 1
            CheckCollisions(); // Проверка столкновений пули с врагами

            // Обновление изображения позади пули
            if (prevBulletY >= 0 && prevBulletY < height)
            {
                objects[prevBulletY, bullet.X] = new TileGraphics(TileGraphics.GraphicType.SPACE);
                UpdateGraphics(prevBulletY, bullet.X, TileGraphics.GraphicType.SPACE);
            }

            if (bullet.Y >= 0) // Проверка на нахождение пули внутри границ экрана
            {
                // Обновление изображения пули
                objects[bullet.Y, bullet.X] = new TileGraphics(TileGraphics.GraphicType.BULLET);
            }

            // Если пуля достигла верхней границы, сбрасываем состояние
            if (bullet.Y == 0)
            {
                objects[bullet.Y, bullet.X] = new TileGraphics(TileGraphics.GraphicType.SPACE);
                isBulletFired = false;
            }
        }


        // Обновление графики 
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                images[y, x].Image = objects[y, x].texture;
            }
        }
    }
    private void FireBullet()
    {
        bullet = new Bullets(playerPosition.X, playerPosition.Y - 1);
    }
    private void CheckCollisions()
    {
        if (bullet != null && bullet.Y > 0 && bullet.Y - 1 >= 0 && bullet.Y - 1 < height)
        {
            var enemiesToRemove = enemyPositions
                .Where(enemyPos => (bullet.X == enemyPos.X && bullet.Y - 1 == enemyPos.Y) ||
                                   (bullet.X == enemyPos.X && bullet.Y == enemyPos.Y))
                .ToList();

            foreach (var enemyToRemove in enemiesToRemove)
            {
                enemyPositions.Remove(enemyToRemove);
                objects[enemyToRemove.Y, enemyToRemove.X] = new TileGraphics(TileGraphics.GraphicType.SPACE);
                UpdateGraphics(enemyToRemove.Y, enemyToRemove.X, TileGraphics.GraphicType.SPACE);
            }
        }
    }
    private void EnemyTimer_Tick(object sender, EventArgs e)
    {
        enemyMoveDown++;
        List<Point> newEnemyPositions = new List<Point>();

        foreach (Point enemyPos in enemyPositions)
        {
            int newY = enemyPos.Y + (enemyMoveDown == 1 ? 1 : 0);
            int newX = r.Next(2) == 0 ? Math.Max(0, enemyPos.X - 1) : Math.Min(width - 1, enemyPos.X + 1);

            if (newY == height - 4)
            {
                LoseGame();
                return;
            }

            if (newX >= 0 && newX < width && objects[newY, newX].type != TileGraphics.GraphicType.ENEMY_1)
            {
                UpdateGraphics(enemyPos.Y, enemyPos.X, TileGraphics.GraphicType.SPACE);
                UpdateGraphics(newY, newX, TileGraphics.GraphicType.ENEMY_1);
                newEnemyPositions.Add(new Point(newX, newY));
            }
            else
            {
                newEnemyPositions.Add(enemyPos);
            }
        }

        enemyPositions = newEnemyPositions;
        if (enemyMoveDown >= 1)
        {
            enemyMoveDown = 0;
        }
    }
    private void GraphicsUpdateTimer_Tick(object sender, EventArgs e)
    {
        Update(); // Вызывайте метод обновления графики здесь
    }
    private void UpdateGraphics(int y, int x, TileGraphics.GraphicType newType)
    {
        objects[y, x] = new TileGraphics(newType);
        images[y, x].Image = objects[y, x].texture;
    }
    public void LoseGame()
    {
        Paus();
      
        DialogResult result = MessageBox.Show("You lost! Do you want to retry the level?", "Game Over", MessageBoxButtons.YesNo);
        if (result == DialogResult.Yes)
        {
            Retry();
            Resume();
        }
        else if (result == DialogResult.No)
        {
            // Вернуться в главное меню
            parent.Close();
        }
    }

    public void MovePlayer(Keys type)
    {
        int newPlayerX = playerPosition.X;
        int newPlayerY = playerPosition.Y;

        switch (type)
        {
            case Keys.Left:
                {
                    newPlayerX = Math.Max(0, playerPosition.X - 1); // Движение влево
                    break;
                }
            case Keys.Right:
                {
                    newPlayerX = Math.Min(width - 1, playerPosition.X + 1); // Движение вправо
                    break;
                }
            case Keys.Up:
                {
                    newPlayerY = Math.Max(height - 3, playerPosition.Y - 1); // Движение вверх, но не выше трех клеток
                    break;
                }
            case Keys.Down:
                {
                    newPlayerY = Math.Min(height - 1, playerPosition.Y + 1); // Движение вниз
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

        // Проверяем, что новая позиция не выходит за границы
        if (newPlayerX >= 0 && newPlayerX < width && newPlayerY >= height - 3 && newPlayerY < height)
        {
            UpdateGraphics(playerPosition.Y, playerPosition.X, TileGraphics.GraphicType.SPACE); // Убираем старую позицию игрока
            UpdateGraphics(newPlayerY, newPlayerX, TileGraphics.GraphicType.PLAYER_1); // Устанавливаем новую позицию игрока
            playerPosition.X = newPlayerX; // Обновляем текущую позицию игрока
            playerPosition.Y = newPlayerY;
        }
    }
}