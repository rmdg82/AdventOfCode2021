using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4.Models
{
    public class Board
    {
        public Number?[,] Matrix { get; private set; }
        public int NumCols { get; }
        public int NumRows { get; }

        public Board(int numCols, int numRows)
        {
            Matrix = new Number[numCols, numRows];
            NumCols = numCols;
            NumRows = numRows;
        }

        public bool IsFullyPopulated()
        {
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    if (Matrix[row, col] is null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void Print()
        {
            if (!IsFullyPopulated())
            {
                throw new Exception("The board is not fully populated.");
            }
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    Console.Write($"[{Matrix[row, col]}]");
                }
                Console.WriteLine();
            }
        }

        public void AddRow(List<Number> row, int position)
        {
            if (position < 0 || position > NumRows)
            {
                throw new ArgumentOutOfRangeException($"Value {position} is not > 0 or < {NumRows}");
            }

            if (row.Count != NumCols)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            for (int i = 0; i < NumCols; i++)
            {
                Matrix[position, i] = row[i];
            }
        }

        public bool HasWin()
        {
            if (!IsFullyPopulated())
            {
                throw new Exception("The board is not fully populated.");
            }

            return HasAnyColCompleted() || HasAnyRowCompleted();
        }

        public bool DrawNumber(int extractedNum)
        {
            if (!IsFullyPopulated())
            {
                throw new Exception("The board is not fully populated.");
            }

            for (int i = 0; i < NumRows; i++)
            {
                if (DrawNumberInRow(extractedNum, i))
                {
                    return true;
                }
            }

            return false;
        }

        public List<int> GetUnmarkedNumbers()
        {
            if (!IsFullyPopulated())
            {
                throw new Exception("The board is not fully populated.");
            }

            List<int> result = new();
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    if (!Matrix[row, col]!.IsDrawn)
                    {
                        result.Add(Matrix[row, col]!.Value);
                    }
                }
            }

            return result;
        }

        private bool DrawNumberInRow(int extractedNum, int rowNum)
        {
            if (rowNum < 0 || rowNum > NumRows)
            {
                throw new ArgumentOutOfRangeException(nameof(rowNum));
            }

            for (int i = 0; i < NumCols; i++)
            {
                if (Matrix[rowNum, i].Value == extractedNum)
                {
                    Matrix[rowNum, i].Extract();
                    return true;
                }
            }

            return false;
        }

        private bool HasAnyRowCompleted()
        {
            for (int i = 0; i < NumRows; i++)
            {
                if (IsRowCompleted(i))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasAnyColCompleted()
        {
            for (int i = 0; i < NumCols; i++)
            {
                if (IsColCompleted(i))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsColCompleted(int colNumber)
        {
            if (colNumber < 0 || colNumber >= NumCols)
            {
                throw new ArgumentOutOfRangeException($"Value {colNumber} is not > 0 or < {NumCols}");
            }

            for (int row = 0; row < NumRows; row++)
            {
                var number = Matrix[row, colNumber];
                if (!number.IsDrawn)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsRowCompleted(int rowNumber)
        {
            if (rowNumber < 0 || rowNumber >= NumRows)
            {
                throw new ArgumentOutOfRangeException($"Value {rowNumber} is not > 0 or < {NumRows}");
            }

            for (int i = 0; i < NumCols; i++)
            {
                var number = Matrix[rowNumber, i];
                if (!number.IsDrawn)
                {
                    return false;
                }
            }

            return true;
        }
    }
}