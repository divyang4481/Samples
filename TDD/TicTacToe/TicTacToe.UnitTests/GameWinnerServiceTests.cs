using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using TicTacToe.Services;

namespace TicTacToe.UnitTests
{
    public class GameWinnerServiceTests
    {
        public class ValidateMethod
        {
            private readonly IGameWinnerService target;
            private readonly char[,] gameBoard = new[,]
                    {
                        {' ', ' ', ' '},
                        {' ', ' ', ' '},
                        {' ', ' ', ' '}
                    };

            public ValidateMethod()
            {
                target = new GameWinnerService();
            }

            [Fact]
            public void neither_player_should_win_when_board_is_empty()
            {
                // Arrange
                const char expected = ' ';
                
                // Act
                var result = target.Validate(gameBoard);

                // Assert
                Assert.Equal(expected, result);
            }

            [Fact]
            public void player_should_win_when_they_have_all_spaces_in_top_row()
            {
                // Arrange
                const char expected = 'X';
                for (int i = 0; i < 3; i++)
                {
                    gameBoard[0, i] = expected;
                }

                // Act 
                var result = target.Validate(gameBoard);

                // Assert
                Assert.Equal(expected, result);
            }

            [Fact]
            public void player_should_win_when_they_have_all_spaces_in_first_column()
            {
                // Arrange
                const char expected = 'X';
                for (int i = 0; i < 3; i++)
                {
                    gameBoard[i, 0] = expected;
                }

                // Act
                var result = target.Validate(gameBoard);

                // Assert
                Assert.Equal(expected, result);
            }

            [Fact]
            public void player_should_win_when_they_have_all_spaces_in_diagonal()
            {
                // Arrange
                const char expected = 'X';
                for (int cellIndex = 0; cellIndex < 3; cellIndex++)
                {
                    gameBoard[cellIndex, cellIndex] = expected;
                }

                // Act 
                var result = target.Validate(gameBoard);

                // Assert
                Assert.Equal(expected, result);
            }
        }
    }
}
