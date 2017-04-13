using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private Game game;
    private Player player;
    public Transform drop;
    public int velocity;
    public bool gameUnderWay = false;


    public BucketUI[][] buckets;
    public Button[] bucketButtons;
    public Text dropsLeftCounter;
    public GameObject gameOverPanel;
    public Text gameInformationText;
    public Text restartButtonText;

    public int maxDropsPerBucket;
    private int flyingDrops;

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

            UpdateNumberOfDropsLeft();
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
        ShowGameOverPanel("Press Start game");
        SetRestartButtonText("Start");
    }

    public void Update()
    {
        if(game.Ended() && flyingDrops == 0)
        {
            if(game.Winner())
            {

            }else
            {
                ShowGameOverPanel("Game Over");
            }
        }
    }

    public void RestartButtonClick()
    {
        HideGameOverPanel();
        if(!gameUnderWay)
        {
            gameUnderWay = true;
            SetRestartButtonText("Restart");
        }
        game.DistributeDropOnBoard();
    }

    private void SetRestartButtonText(string text)
    {
        restartButtonText.text = text;
    }

    public bool ExplosionBucket(BucketUI bucket)
    {
        game.BucketExploded();
        CreateDrop(new Vector3(velocity, 0, 0), bucket.transform.position, 90);
        CreateDrop(new Vector3(-velocity, 0, 0), bucket.transform.position, 270);
        CreateDrop(new Vector3(0, velocity, 0), bucket.transform.position, 180);
        CreateDrop(new Vector3(0, -velocity, 0), bucket.transform.position, 0);
        UpdateNumberOfDropsLeft();
        return true;
    }

    private void CreateDrop(Vector3 speed, Vector3 startLocation, int rotationAngle)
    {
        DropUI drop = Instantiate(this.drop, startLocation, transform.rotation).GetComponent<DropUI>();
        drop.speed = speed;
        drop.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        drop.transform.position = startLocation;
        drop.transform.Rotate(new Vector3(0, 0, rotationAngle));
    }

    public void UpdateNumberOfDropsLeft()
    {
        dropsLeftCounter.text = game.TotalDropsLeft.ToString();
    }

    public void AddFlyingDrop()
    {
        flyingDrops++;
    }

    public void DiedFlyingDrop()
    {
        flyingDrops--;
    }

    public bool CanUserPlay()
    {
        return flyingDrops == 0 && !game.Ended();
    }

    public Game GetGame()
    {
        return game;
    }

    private void ShowGameOverPanel(string informationText)
    {
        gameOverPanel.SetActive(true);
        gameInformationText.text = informationText;
    }

    private void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
}
