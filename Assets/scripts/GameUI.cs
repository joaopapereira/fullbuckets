using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private Game game;
    private Player player;
    public Transform drop;
    public int velocity;


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
    
    public bool ExplosionBucket(BucketUI bucket)
    {
        CreateDrop(new Vector3(velocity, 0, 0), bucket.transform.position);
        CreateDrop(new Vector3(-velocity, 0, 0), bucket.transform.position);
        CreateDrop(new Vector3(0, velocity, 0), bucket.transform.position);
        CreateDrop(new Vector3(0, -velocity, 0), bucket.transform.position);
        return true;
    }
    private void CreateDrop(Vector3 speed, Vector3 startLocation)
    {
        DropUI drop = Instantiate(this.drop, startLocation, transform.rotation).GetComponent<DropUI>();
        drop.speed = speed;
        drop.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        drop.transform.position = startLocation;

    }
}
