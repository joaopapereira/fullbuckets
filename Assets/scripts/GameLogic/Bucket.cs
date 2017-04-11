using System;
delegate void ExplosionCallback(Bucket bucket);

public class Bucket {
    private int maximumSize;
    private int currentSize;
    private Func<Bucket, bool> explosionCallback;
    private Game game;

    public int Size
    {
        get { return currentSize; }
    }

    public Bucket(int maximumSize, Game game)
    {
        Reset();
        this.maximumSize = maximumSize;
        this.game = game;
    }

    public void AddDrop()
    {
        if (Full())
        {
            Reset();
            explosionCallback(this);
        }
        else
        {
            currentSize++;
        }
    }

    public bool Full()
    {
        return currentSize == maximumSize;
    }

    public void SetExplosionCallback(Func<Bucket, bool> explosionCallback)
    {
        this.explosionCallback = explosionCallback;
    }

    public void Reset(int currentSize = 0)
    {
        this.currentSize = currentSize;
    }

    public bool DropLanded()
    {
        if(Size == 0)
            return false;

        AddDrop();
        return true;
    }
}
