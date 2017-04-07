using System;
using UnityEngine;
using UnityEngine.UI;

public class BucketUI : MonoBehaviour
{
    public int positionX;
    public int positionY;
    public Text label;

    public void AddDrop()
    {
        Console.WriteLine(positionX + " " + positionY);
        label.text = positionX + " " + positionY;
    }
}
