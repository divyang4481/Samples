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
            [Fact]
            public void neither_player_should_win_when_board_is_empty()
            {
                // Arrange
                IGameWinnerService target = new GameWinnerService();
                const char expected = ' ';
                var gameBoard = new[,]
                    {
                        {' ', ' ', ' '},
                        {' ', ' ', ' '},
                        {' ', ' ', ' '}
                    };

                // Act
                var result = target.Validate(gameBoard);

                // Assert
                Assert.Equal(expected, result);
            }
        }
    }
}
