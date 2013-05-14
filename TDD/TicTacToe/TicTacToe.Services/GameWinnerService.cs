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

            return ' ';
        }
    }
}
