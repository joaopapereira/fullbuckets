using System;

public class Bucket {
    private int maximumSize;
    private int currentSize;
    public int Size
    {
        get { return currentSize; }
    }

    public Bucket(int maximumSize)
    {
        currentSize = 0;
        this.maximumSize = maximumSize;
    }

    public void AddDrop()
    {
        currentSize++;
    }

    public bool Full()
    {
        return currentSize == maximumSize;
    }
}
