using System.Data;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class Grid
    {
        private readonly int rows;
        private readonly int columns;
        private readonly string[,] grid;
        private readonly string[,] grid1;


        public Grid(int rows, int columns)
        {
            this.rows = rows - 1;
            this.columns = columns - 1;
            this.grid = new string[rows, columns];
            this.grid1 = new string[rows, columns];
        }


        public string[,] GenerateInitialGrid(int cellNumber)
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    grid[i, j] = " ";
                }
            }

            for (int i = 0; i < cellNumber; i++)
            {
                int rowRandom = random.Next(0 , rows);
                int columnRandom = random.Next(0 , columns);

                this.grid[rowRandom, columnRandom] = "0";
            }

            Array.Copy(grid, grid1, grid.Length);

            return grid;
        }

        public void ShowGrid()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    sb.Append(this.grid[i, j]);
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb);
        }

        public int CheckMiddleCells(int i, int j)
        {
            int neighborsAlive = 0;

            if (grid[i + 1, j] == "0")
            {
                neighborsAlive += 1;
            }

            if (grid[i + 1, j + 1] == "0")
            {
                neighborsAlive += 1;
            }

            if (grid[i + 1, j - 1] == "0")
            {
                neighborsAlive += 1;
            }

            if (grid[i - 1, j] == "0")
            {
                neighborsAlive += 1;
            }

            if (grid[i - 1, j + 1] == "0")
            {
                neighborsAlive += 1;
            }

            if (grid[i - 1, j - 1] == "0")
            {
                neighborsAlive += 1;
            }

            if (grid[i, j + 1] == "0")
            {
                neighborsAlive += 1;
            }

            if (grid[i, j - 1] == "0")
            {
                neighborsAlive += 1;
            }

            return neighborsAlive;
        }



        public void UpdateGrid()
        {
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < columns; j++)
                {
                    int num = CheckMiddleCells(i, j);
                    bool alive = false;
                    if (grid[i, j] == "0")
                    {
                        alive = true; 
                    }

                    if (alive && (num < 2 || num > 3))
                    {
                        grid1[i, j] = " ";
                    }
                    else if (!alive && num == 3)
                    {
                        grid1[i, j] = "0";
                    }

                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    grid[i, j] = grid1[i, j];
                }
            }
        }
    }

    static class RunGame
    {
        public static void Main(string[] arg)
        {
            Grid grid = new Grid(40, 150);
            grid.GenerateInitialGrid(750);
            Console.CursorVisible = false;
            int value = 0;
            bool on = false;
            Console.BackgroundColor = ConsoleColor.Blue;

            while (true)
            {

                
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                    {   
                        on = !on;
                        value = on? 500 : 0;
                    }
                }

                grid.ShowGrid();
                grid.UpdateGrid();
                Thread.Sleep(value);
                Console.Clear();
            }
        }
    }
}



