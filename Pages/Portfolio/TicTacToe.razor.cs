using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Numerics;

namespace PraveenMatoria.Pages.Portfolio;

public partial class TicTacToe
{
    [Inject]
    public IJSRuntime JS { get; set; }

    //public TicTacToe(IJSRuntime js)
    //{
    //    JS = js;
    //}
    private string[] Board { get; set; } = ["", "", "", "", "", "", "", "", ""];
    private string Player { get; set; } = "X";
    private int[][] WinningCombos { get; set; } =
    [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];

    private async Task SquareCliked(int idx)
    {
        Board[idx] = Player;
        Player = Player == "X" ? "O" : "X";

        foreach (int[] combo in WinningCombos)
        {
            int p1 = combo[0];
            int p2 = combo[1];
            int p3 = combo[2];
            if (Board[p1] == string.Empty || Board[p2] == string.Empty || Board[p3] == string.Empty) continue;
            if (Board[p1] == Board[p2] && Board[p2] == Board[p3] && Board[p1] == Board[p3])
            {
                string winner = Player == "X" ? "Player TWO" : "Player ONE";
                await JS.InvokeVoidAsync("ShowSwal", winner);
                ResetGame(Board);
            }
        }

        if (Board.All(x => x != ""))
        {
            await JS.InvokeVoidAsync("ShowTie");
            ResetGame(Board);
        }
    }
    private static void ResetGame(string[] Board)
    {
        for (int i = 0; i < Board.Length; i++)
        {
            Board[i] = "";
        }
    }
}