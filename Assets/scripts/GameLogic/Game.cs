using System;

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
    private int boardSize = 5;
    public int BoardSize { get { return boardSize; } set { boardSize = value; } }
    private Player player;
    private int maxDropsPerBucket;
    public int MaxDropsPerBucket { get { return maxDropsPerBucket; } set { maxDropsPerBucket = value;  } }
    public int TotalDropsLeft { get { return player.Drops; } }

    private Bucket[][] board;
    public Game(Player player)
    {
        this.player = player;
        GenerateBoard();
        this.player.Drops = 10;
    }

    private void GenerateBoard()
    {
        board = new Bucket[boardSize][];
        for (int i = 0; i < boardSize; i++)
        {
            board[i] = new Bucket[boardSize];
        }
    }
    public void DistributeDropOnBoard()
    {
        int dropToDistribute = 30;
        Random random = new Random();
        random.Next(0, 10);
        for (int i = 0; i < boardSize; i++)
        {
            for(int j = 0; j < boardSize; j++)
            {
                int drops = random.Next(0, 10);
                if(drops < 5)
                {
                    if (drops > dropToDistribute)
                        drops = dropToDistribute;
                } else
                {
                    drops = 0;
                }
                board[i][j].Reset(drops);
                dropToDistribute -= drops;
            }
        }
    }

    public void CleanBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board[i][j].Reset();
            }
        }
    }

    public bool Ended()
    {
        if (player.Drops == 0 || Winner())
            return true;
        return false;
    }

    public bool Winner()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (board[i][j].Size > 0)
                    return false;
            }
        }
        return true;
    }

    public void PlayerClickBucket()
    {
        player.Drops--;
    }

    public void BucketExploded()
    {
        player.Drops++;
    }

    public void RegisterBucket(int positionX, int positionY, Bucket bucket)
    {
        board[positionY][positionX] = bucket;
    }
}
