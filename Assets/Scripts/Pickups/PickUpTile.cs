using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTile : MonoBehaviour
{

    //if the player collides with a pickup tile, it will pick it up and add to the tile count.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //count up the tiles amount
            GameObject.Find("Pick Up Tile Count").GetComponent<PickUpTileCount>().tiles++;
            Destroy(gameObject);
        }
    }

}
