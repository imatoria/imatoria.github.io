using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Numerics;

namespace PraveenMatoria.Pages.Portfolio;

public partial class TicTacToe
{
    private string[,] Board { get; set; } = { { "", "", "" }, { "", "", "" }, { "", "", "" } };
    private string Player { get; set; } = "X";
    private int[,,] WinningCombos { get; set; } = {
        { { 0, 0 }, { 0, 1 }, { 0, 2 } },
        { { 1, 0 }, { 1, 1 }, { 1, 2 } },
        { { 2, 0 }, { 2, 1 }, { 2, 2 } },
        { { 0, 0 }, { 1, 0 }, { 2, 0 } },
        { { 0, 1 }, { 1, 1 }, { 2, 1 } },
        { { 0, 2 }, { 1, 2 }, { 2, 2 } },
        { { 0, 0 }, { 1, 1 }, { 2, 2 } },
        { { 0, 2 }, { 1, 1 }, { 2, 0 } }
    };

    private async Task SquareCliked(int i, int j)
    {
        Board[i, j] = Player;

        //Making a separate thread to avoid blocking current UI update.
        await Task.Run(() => RunOpponentMove());
    }
    private async Task RunOpponentMove()
    {
        var random = new Random();
        var waitTime = random.Next(500, 1500);
        Thread.Sleep(waitTime);

        string Opponent = Player == "X" ? "O" : "X";
        Move move = FindBestMove(Board, Opponent, Player);
        Board[move.row, move.col] = Opponent;


        int len = WinningCombos.GetLength(0);
        for (int xi = 0; xi < len; xi++)
        {
            int px1 = WinningCombos[xi, 0, 0];
            int py1 = WinningCombos[xi, 0, 1];

            int px2 = WinningCombos[xi, 1, 0];
            int py2 = WinningCombos[xi, 1, 1];

            int px3 = WinningCombos[xi, 2, 0];
            int py3 = WinningCombos[xi, 2, 1];

            if (Board[px1, py1] == string.Empty || Board[px2, py2] == string.Empty || Board[px3, py3] == string.Empty) continue;

            if (Board[px1, py1] == Board[px2, py2] && Board[px2, py2] == Board[px3, py3] && Board[px3, py3] == Board[px1, py1])
            {
                string winner = Player == "X" ? "AI " : "You";
                await JS.InvokeVoidAsync("ShowSwal", winner);
                return;
            }
        }

        if (HasGameTied())
        {
            await JS.InvokeVoidAsync("ShowTie");
            return;
        }
    }

    [JSInvokable]
    public void ResetGame()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Board[i, j] = "";
            }
        }

        StateHasChanged();
    }

    private async void PlayOpponent()
    {

    }
    private bool HasGameTied()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Board[i, j] == "")
                {
                    return false;
                }
            }
        }
        return true;
    }

    private class Move
    {
        public int row, col;
    };
    // This function returns true if there are moves 
    // remaining on the board. It returns false if 
    // there are no moves left to play. 
    private bool IsMovesLeft(string[,] board)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j] == "")
                    return true;
        return false;
    }

    // This is the evaluation function as discussed 
    // in the previous article ( http://goo.gl/sJgv68 ) 
    private int Evaluate(string[,] board, string player, string opponent)
    {
        // Checking for Rows for X or O victory. 
        for (int row = 0; row < 3; row++)
        {
            if (board[row, 0] == board[row, 1] &&
                board[row, 1] == board[row, 2])
            {
                if (board[row, 0] == player)
                    return +10;
                else if (board[row, 0] == opponent)
                    return -10;
            }
        }

        // Checking for Columns for X or O victory. 
        for (int col = 0; col < 3; col++)
        {
            if (board[0, col] == board[1, col] &&
                board[1, col] == board[2, col])
            {
                if (board[0, col] == player)
                    return +10;

                else if (board[0, col] == opponent)
                    return -10;
            }
        }

        // Checking for Diagonals for X or O victory. 
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            if (board[0, 0] == player)
                return +10;
            else if (board[0, 0] == opponent)
                return -10;
        }

        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            if (board[0, 2] == player)
                return +10;
            else if (board[0, 2] == opponent)
                return -10;
        }

        // Else if none of them have won then return 0 
        return 0;
    }

    // This is the minimax function. It considers all 
    // the possible ways the game can go and returns 
    // the value of the board 
    private int MinMax(string[,] board, int depth, Boolean isMax, string player, string opponent)
    {
        int score = Evaluate(board, player, opponent);

        // If Maximizer has won the game  
        // return his/her evaluated score 
        if (score == 10)
            return score;

        // If Minimizer has won the game  
        // return his/her evaluated score 
        if (score == -10)
            return score;

        // If there are no more moves and  
        // no winner then it is a tie 
        if (IsMovesLeft(board) == false)
            return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == "")
                    {
                        // Make the move 
                        board[i, j] = player;

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = Math.Max(best, MinMax(board, depth + 1, !isMax, player, opponent));

                        // Undo the move 
                        board[i, j] = "";
                    }
                }
            }
            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = 1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == "")
                    {
                        // Make the move 
                        board[i, j] = opponent;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = Math.Min(best, MinMax(board, depth + 1, !isMax, player, opponent));

                        // Undo the move 
                        board[i, j] = "";
                    }
                }
            }
            return best;
        }
    }

    // This will return the best possible 
    // move for the player 
    private Move FindBestMove(string[,] board, string player, string opponent)
    {
        int bestVal = -1000;
        Move bestMove = new()
        {
            row = -1,
            col = -1
        };

        // Traverse all cells, evaluate minimax function  
        // for all empty cells. And return the cell  
        // with optimal value. 
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Check if cell is empty 
                if (board[i, j] == "")
                {
                    // Make the move 
                    board[i, j] = player;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = MinMax(board, 0, false, player, opponent);

                    // Undo the move 
                    board[i, j] = "";

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        Console.Write("The value of the best Move is : {0}\n\n", bestVal);

        return bestMove;
    }
}