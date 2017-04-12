using System;
using UnityEngine;
using UnityEngine.UI;

public class BucketUI : MonoBehaviour
{
    public int positionX;
    public int positionY;
    public Text label;
    public Button button;
    private Bucket bucket;
    private GameUI gameController;

    public void Update()
    {
       button.interactable = gameController.CanUserPlay();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Drop" && collider.transform.position != transform.position)
        {
            if (bucket.DropLanded())
                collider.SendMessage("DropStop");
        }
        UpdateButton();
    }

    public void AddDrop()
    {
        bucket.PlayerClick();
        UpdateButton();
        gameController.UpdateNumberOfDropsLeft();
    }

    public void SetGameController(GameUI gameController)
    {
        this.gameController = gameController;

        if (bucket == null)
        {
            bucket = new Bucket(gameController.maxDropsPerBucket, gameController.GetGame());
            UpdateButton();
            bucket.SetExplosionCallback(this.ExplosionBucket);
        }
    }

    public void UpdateButton()
    {
        label.text = bucket.Size.ToString();
    }

    public bool ExplosionBucket(Bucket bucket)
    {
        return this.gameController.ExplosionBucket(this);
    }
}
