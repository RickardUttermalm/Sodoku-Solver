using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku.Web.Data
{
    public class SodokuSolver
    {
        public int[,] Board { get; set; } = new int[9, 9];

        public int[,] GetBoardFromString(string numbers)
        {
            if (numbers.Length < 81)
            {
                var count = 81 - numbers.Length;
                var b = new StringBuilder(numbers);
                for (int i = 0; i < count; i++)
                {
                    b.Append("0");
                }
                numbers = b.ToString();
            }
            var board = new int[9, 9];
            int nrCount = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = int.Parse(numbers[nrCount].ToString());
                    nrCount++;
                }
            }

            return board;
        }

        public bool IsValidPuzzle(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i,j] < 0 || board[i,j] > 9)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Solve(int[,] board)
        {
            Board = board;
            return FindSolution(Board);
        }

        public bool FindSolution(int[,] Board)
        {
            List<int> PossibleNumbers;
            for (int i = 1; i < 10; i++)
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (Board[row, col] == 0)
                        {
                            PossibleNumbers = GetPossibleNumbers(row, col, Board);
                            if (PossibleNumbers.Count == i)
                            {
                                return GuessNumbers(row, col, PossibleNumbers, Board);
                            }
                        }
                    }
                }
            }
            return true;
        }

        private bool GuessNumbers(int row, int col, List<int> Possiblenumbers, int[,] Board)
        {
            for (int Guess = 1; Guess < 10; Guess++)
            {
                if (Possiblenumbers.Contains(Guess))
                {
                    Board[row, col] = Guess;
                    if (FindSolution(Board))
                    {
                        if (IsSolved(Board))
                        {
                            return true;
                        }
                    }
                }
            }
            Board[row, col] = 0;
            return false;
        }

        private List<int> GetPossibleNumbers(int x, int y, int[,] board)
        {
            List<int> possible = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> notpossible = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (board[x, i] != 0)
                {
                    notpossible.Add(board[x, i]);
                }
                if (board[i, y] != 0)
                {
                    notpossible.Add(board[i, y]);
                }
            }

            x = (x / 3) * 3;
            y = (y / 3) * 3;
            for (int i = x; i < x + 3; i++)
            {
                for (int j = y; j < y + 3; j++)
                {
                    if (board[i, j] != 0)
                    {
                        notpossible.Add(board[i, j]);
                    }
                }
            }

            foreach (var item in possible.ToList())
            {
                if (notpossible.Contains(item))
                {
                    possible.Remove(item);
                }
            }
            return possible;
        }

        private bool IsSolved(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
