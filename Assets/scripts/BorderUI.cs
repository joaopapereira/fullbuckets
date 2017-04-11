using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderUI : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Drop")
        {
            collider.SendMessage("DropStop");
        }
    }
}
