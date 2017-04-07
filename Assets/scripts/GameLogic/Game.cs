﻿using System;

public interface Player
{
    int Drops { get; set; }
}

public class PlayerImpl : Player
{
    private int dropsLeft;
    public int Drops
    {
        get
        {
            return dropsLeft;
        }

        set
        {
            dropsLeft = value;
        }
    }
}

public class Game {
    private const int boardSize = 5;
    public int BoardSize { get { return boardSize; } }
    private Player player;

    private int[][] board;
    public Game(Player player)
    {
        this.player = player;
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        board = new int[boardSize][];
        for (int i = 0; i < boardSize; i++)
        {
            board[i] = new int[boardSize];
            for(int j = 0; j < boardSize; j++)
            {
                board[i][j] = 3;
            }
        }
    }

    public void CleanBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board[i][j] = 0;
            }
        }
    }

    public bool Ended()
    {
        if (player.Drops == 0)
            return true;
        return false;
    }

    public bool Winner()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (board[i][j] > 0)
                    return false;
            }
        }
        return true;
    }
}