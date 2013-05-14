namespace TicTacToe.Services
{
    public class GameWinnerService : IGameWinnerService
    {
        public char Validate(char[,] gameBoard)
        {
            var currentWinningSymbol = ' ';
            currentWinningSymbol = CheckForThreeInRowInHorizontalRow(gameBoard);
            currentWinningSymbol = CheckForThreeInARowInVerticalColumn(gameBoard);
            return currentWinningSymbol;
        }

        private char CheckForThreeInARowInVerticalColumn(char[,] gameBoard)
        {
            var rowOneChar = gameBoard[0, 0];
            var rowTwoChar = gameBoard[1, 0];
            var rowThreeChar = gameBoard[2, 0];
            if (rowOneChar == rowTwoChar && rowTwoChar == rowThreeChar)
            {
                return rowOneChar;
            }
            return ' ';
        }

        private char CheckForThreeInRowInHorizontalRow(char[,] gameBoard)
        {
            var columnOneChar = gameBoard[0, 0];
            var columnTwoChart = gameBoard[0, 1];
            var columnThreeChar = gameBoard[0, 2];
            if (columnOneChar == columnTwoChart && columnTwoChart == columnThreeChar)
            {
                return columnOneChar;
            }

            return ' ';
        }
    }
}
