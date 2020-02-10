using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlaceTile : MonoBehaviour
{
    /* whenever it detects that no tile is beneath the player it
     * a. checks if there are any pickuptiles that the player has
     * b. puts them underneath the player 
     * c. removes one pickup tile from the players inventory
     * 
     * 
     * */
    [SerializeField] GameObject PickUpTileCount;
    [SerializeField] GameObject PickUpTile;

    void Start()
    {
        //Locally save the objects to not constanlty keep "finding" them.
        //Player = GameObject.Find("Player");
        //PickUpTileCount = GameObject.Find("Pick Up Tile Count");
        //PickUpTile = GameObject.Find("Pick Up Tile");
    }

    // Update is called once per frame
    public bool placeTile(Vector3 position)
    {
        //if (GetComponent<PlayerMovement>().isFalling == true && PickUpTileCount.GetComponent<PickUpTileCount>().tiles > 0)
        if (PickUpTileCount.GetComponent<PickUpTileCount>().tiles <= 0)
        {
            //Debug.Log("FUCK");
            return true;
            //Debug.Log("Fall");

        }
        else
        {
            //transform.position = GetComponent<PlayerMovement>().targetPosition; //new Vector3(Player.transform.position.x, 0.3f, Player.transform.position.z);

            //GetComponent<PlayerMovement>().isFalling = false;
            //GetComponent<PlayerMovement>().verticalSpeed = 0;

                Debug.Log("Place Tile");


            GameObject tile = Instantiate(PickUpTile); //, new Vector3(Player.transform.position.x, Player.transform.position.y - 0.75f , Player.transform.position.z), Quaternion.Identity);    
            tile.transform.position = new Vector3(position.x, tile.transform.position.y, position.z);   //transform.position.x, tile.transform.position.y, transform.position.z);

            PickUpTileCount.GetComponent<PickUpTileCount>().tiles--;
            return false;
        }
        

       // if (Player.GetComponent<PlayerMovement>().isFalling == true)
           // Debug.Log("KILLMENOW");
 


        //Remove one pickup tile from players inventory


    }
}
