namespace TicTacToe.Services
{
    public class GameWinnerService : IGameWinnerService
    {
        public char Validate(char[,] gameBoard)
        {
            var columnOneChar = gameBoard[0, 0];
            var columnTwoChart = gameBoard[0, 1];
            var columnThreeChar = gameBoard[0, 2];
            if (columnOneChar == columnTwoChart && columnTwoChart == columnThreeChar)
            {
                return columnOneChar;
            }

            var rowTwoChar = gameBoard[1, 0];
            var rowThreeChar = gameBoard[2, 0];
            if (columnOneChar == rowTwoChar && rowTwoChar == rowThreeChar)
            {
                return columnOneChar;
            }

            return ' ';
        }
    }
}
