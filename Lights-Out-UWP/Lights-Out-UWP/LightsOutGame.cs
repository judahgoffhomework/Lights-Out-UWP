using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights_Out_UWP
{
    class LightsOutGame
    {
        private int gridSize;
        private bool[,] grid;
        private Random random;

        public const int MaxGridSize = 5;
        public const int MinGridSize = 3;

        public int GridSize
        {
            get
            {
                return gridSize;
            }
            set
            {
                if (value >= MinGridSize && value <= MaxGridSize)
                {
                    gridSize = value;
                    grid = new bool[gridSize, gridSize];
                    NewGame();
                }
            }
        }

        public LightsOutGame()
        {
            random = new Random();
            GridSize = MinGridSize;
        }

        public bool GetGridValue(int row, int col)
        {
            return grid[row, col];
        }

        public void NewGame()
        {
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    grid[r, c] = random.Next(2) == 1;
                }
            }
        }

        public void Move(int row, int col)
        {
            if (row < 0 || row >= gridSize || col < 0 || col >= gridSize)
            {
                throw new ArgumentException("Row or column is outside the legal range of 0 to " + gridSize + 1);
            }

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < gridSize && j >= 0 && j < gridSize)
                    {
                        grid[i, j] = !grid[i, j];
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    if (grid[r,c])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
