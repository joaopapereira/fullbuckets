using System;
using UnityEngine;
using UnityEngine.UI;

public class BucketUI : MonoBehaviour
{
    public int positionX;
    public int positionY;
    public Text label;
    private Bucket bucket;
    private GameUI gameController;

    public void Awake()
    {
    }

    public void AddDrop()
    {
        bucket.AddDrop();
        UpdateButton();
    }

    public void SetGameController(GameUI gameController)
    {
        this.gameController = gameController;

        if (bucket == null)
        {
            bucket = new Bucket(gameController.maxDropsPerBucket);
            UpdateButton();
            bucket.SetExplosionCallback(gameController.ExplosionBucket);
        }
    }

    public void UpdateButton()
    {
        label.text = bucket.Size.ToString();
    }
}
