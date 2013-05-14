namespace TicTacToe.Services
{
    public class GameWinnerService : IGameWinnerService
    {
        private char[,] gameBoard;

        private const char SymbolForNoWinner = ' ';

        public char Validate(char[,] gameBoard)
        {
            this.gameBoard = gameBoard;

            var currentWinningSymbol = CheckForThreeInRowInHorizontalRow();
            if (currentWinningSymbol != SymbolForNoWinner)
            {
                return currentWinningSymbol;
            }

            currentWinningSymbol = CheckForThreeInRowDiagonally();
            if (currentWinningSymbol != SymbolForNoWinner)
            {
                return currentWinningSymbol;
            }

            currentWinningSymbol = CheckForThreeInARowInVerticalColumn();
            return currentWinningSymbol;
        }

        private char CheckForThreeInARowInVerticalColumn()
        {
            var rowOneChar = gameBoard[0, 0];
            var rowTwoChar = gameBoard[1, 0];
            var rowThreeChar = gameBoard[2, 0];

            return AreSymbolsAreEqual(rowOneChar, rowTwoChar, rowThreeChar);
        }

        private char CheckForThreeInRowInHorizontalRow()
        {
            var columnOneChar = gameBoard[0, 0];
            var columnTwoChart = gameBoard[0, 1];
            var columnThreeChar = gameBoard[0, 2];

            return AreSymbolsAreEqual(columnOneChar, columnTwoChart, columnThreeChar);
        }

        private char CheckForThreeInRowDiagonally()
        {
            var cellOneChar = gameBoard[0, 0];
            var cellTwoChar = gameBoard[1, 1];
            var cellThreeChar = gameBoard[2, 2];

            return AreSymbolsAreEqual(cellOneChar, cellTwoChar, cellThreeChar);
        }

        private char AreSymbolsAreEqual(char firstSymbol, char secondSymbol, char thirdSymbol)
        {
            return (firstSymbol == secondSymbol && secondSymbol == thirdSymbol)
                       ? firstSymbol
                       : SymbolForNoWinner;
        }

    }
}
