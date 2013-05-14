namespace TicTacToe.Services
{
    public class GameWinnerService : IGameWinnerService
    {
        private const char SymbolForNoWinner = ' ';

        public char Validate(char[,] gameBoard)
        {
            var currentWinningSymbol = CheckForThreeInRowInHorizontalRow(gameBoard);
            if (currentWinningSymbol != SymbolForNoWinner)
            {
                return currentWinningSymbol;
            }

            currentWinningSymbol = CheckForThreeInRowDiagonally(gameBoard);
            if (currentWinningSymbol != SymbolForNoWinner)
            {
                return currentWinningSymbol;
            }

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
            return SymbolForNoWinner;
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

            return SymbolForNoWinner;
        }

        private char CheckForThreeInRowDiagonally(char[,] gameBoard)
        {
            var cellOneChar = gameBoard[0, 0];
            var cellTwoChar = gameBoard[1, 1];
            var cellThreChar = gameBoard[2, 2];

            if (cellOneChar == cellTwoChar && cellTwoChar == cellThreChar)
            {
                return cellOneChar;
            }

            return SymbolForNoWinner;
        }
    }
}
