using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private Game game;
    private Player player;

    public BucketUI[][] buckets;
    public Button[] bucketButtons;

    public int maxDropsPerBucket;

    public void Start()
    {
        if (player == null)
        {
            player = new PlayerImpl();
        }
        if (game == null)
        {
            game = new Game(player)
            {
                MaxDropsPerBucket = maxDropsPerBucket
            };
        }
        if (buckets == null)
        {
            buckets = new BucketUI[game.BoardSize][];
            for(int i = 0; i < game.BoardSize; i++)
            {
                buckets[i] = new BucketUI[game.BoardSize];
                for(int j= 0; j < game.BoardSize; j++)
                {
                    buckets[i][j] = bucketButtons[i * game.BoardSize + j].GetComponent<BucketUI>();
                    buckets[i][j].positionX = j;
                    buckets[i][j].positionY = i;
                    buckets[i][j].SetGameController(this);
                }
            }
        }
    }   
    
    public bool ExplosionBucket(Bucket bucket)
    {
        return true;
    }
}
