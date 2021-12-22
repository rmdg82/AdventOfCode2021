using Day4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Day4.Tests
{
    public class BoardTests
    {
        public static Board PopulateBoardCompletely()
        {
            var board = new Board(5, 5);

            for (int row = 0; row < board.NumRows; row++)
            {
                board.AddRow(
                    new List<Number>
                        {
                            new Number(1 + row),
                            new Number(2 + row),
                            new Number(3 + row),
                            new Number(4 + row),
                            new Number(5 + row)
                        }, row);
            }

            return board;
        }

        [Fact]
        public void IsFullyPopulated_ThereAreSomeNulls_ReturnFalse()
        {
            var board = new Board(5, 5);

            for (int row = 1; row < board.NumRows; row++)
            {
                board.AddRow(
                    new List<Number>
                        {
                            new Number(1),
                            new Number(2),
                            new Number(3),
                            new Number(4),
                            new Number(5)
                        }, row);
            }

            Assert.False(board.IsFullyPopulated());
        }

        [Fact]
        public void IsFullyPopulated_ThereAreNoNulls_ReturnTrue()
        {
            var board = PopulateBoardCompletely();

            Assert.True(board.IsFullyPopulated());
        }

        [Fact]
        public void ExtractNumber_WhenCalled_ChangeIsDrawnProp()
        {
            var number = new Number(1);

            number.Extract();

            Assert.True(number.IsDrawn);
        }

        [Fact]
        public void AddRow_ListSizeNotCorrect_ThrowArgumentOutOfRangeException()
        {
            var board = new Board(5, 5);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => board.AddRow(new List<Number> { new Number(1) }, 0));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => board.AddRow(new List<Number>
            {
                new Number(1),
                new Number(1),
                new Number(1),
                new Number(1),
                new Number(1),
                new Number(1),
            }, 0));
        }

        [Fact]
        public void AddRow_PositionNotCorrect_ThrowArgumentOutOfRangeException()
        {
            var board = new Board(5, 5);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => board.AddRow(new List<Number>
            {
                new Number(1),
                new Number(2),
                new Number(3),
                new Number(4),
                new Number(5),
            }, board.NumRows + 1));
        }

        [Fact]
        public void AddRow_ListSizeCorrect_NumbersSaved()
        {
            var board = new Board(5, 5);

            board.AddRow(new List<Number>
            {
                new Number(1),
                new Number(2),
                new Number(3),
                new Number(4),
                new Number(5),
            }, 0);

            Assert.Equal(1, board.Matrix[0, 0].Value);
            Assert.Equal(2, board.Matrix[0, 1].Value);
            Assert.Equal(3, board.Matrix[0, 2].Value);
            Assert.Equal(4, board.Matrix[0, 3].Value);
            Assert.Equal(5, board.Matrix[0, 4].Value);
        }

        [Fact]
        public void HasWin_ThereIsNoAWin_ReturnFalse()
        {
            var board = PopulateBoardCompletely();

            Assert.False(board.HasWin());
        }

        [Fact]
        public void HasWin_ThereIsRowCompleted_ReturnTrue()
        {
            var board = new Board(5, 5);

            board.AddRow(
                new List<Number>
                    {
                        new Number(1, true),
                        new Number(2, true),
                        new Number(3, true),
                        new Number(4, true),
                        new Number(5, true)
                    }, 0);

            for (int row = 1; row < board.NumRows; row++)
            {
                board.AddRow(
                    new List<Number>
                        {
                            new Number(1),
                            new Number(2),
                            new Number(3),
                            new Number(4),
                            new Number(5)
                        }, row);
            }

            Assert.True(board.HasWin());
        }

        [Fact]
        public void HasWin_ThereIsColCompleted_ReturnTrue()
        {
            var board = PopulateBoardCompletely();

            for (int row = 0; row < board.NumRows; row++)
            {
                board.Matrix[row, 1].Extract();
            }

            Assert.True(board.HasWin());
        }

        [Fact]
        public void DrawNumber_ExtractNotExistingNumber_ReturnFalse()
        {
            var board = PopulateBoardCompletely();

            var result = board.DrawNumber(100);

            Assert.False(result);
        }

        [Fact]
        public void DrawNumber_ExtractExistingNumber_ReturnTrue()
        {
            var board = PopulateBoardCompletely();

            var result = board.DrawNumber(1);

            Assert.True(result);
        }

        [Fact]
        public void DrawNumber_ExtractExistingNumber_ExtractedNumSetIsDrawnTrue()
        {
            var board = PopulateBoardCompletely();

            var result = board.DrawNumber(1);

            Assert.True(result);
            Assert.True(board.Matrix[0, 0].IsDrawn);
        }

        [Fact]
        public void GetUnmarkedNumbers_WhenCalled_GetCorrectNumbers()
        {
            var board = PopulateBoardCompletely();

            // Extract first row
            for (int col = 0; col < board.NumCols; col++)
            {
                board.Matrix[0, col].Extract();
            }

            var result = board.GetUnmarkedNumbers();

            Assert.Equal(20, result.Count);
        }
    }
}