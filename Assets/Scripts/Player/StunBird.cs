using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBird : MonoBehaviour
{
    //note: When you spawn a bird its radius needs to be 1 away from the player


    private int speed;
    bool playerIsStunned;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        speed = 150;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(player.transform.position, Vector3.up, speed * Time.deltaTime);
        //Debug.Log(player.transform.position);

        playerIsStunned = player.GetComponent<PlayerMovement>().stunned;

        if (!playerIsStunned) {
            Destroy(gameObject);
            //Debug.Log("destroy");
        }

        //Debug.Log("player stunned: " + playerIsStunned);
    }
}
