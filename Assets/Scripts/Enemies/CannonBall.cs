using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Transform targetPosition;
    Transform startPosition;

    float journeyTime = 1.0f;

    private float startTime;

    GameObject player;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        //find the player so it can be stunned.
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    //take the positions from cannon script.
    public void Positions(Transform startPosition, Transform targetPosition)
    {

        this.targetPosition = targetPosition;
        this.startPosition = startPosition;

    }

    // Update is called once per frame
    void Update()
    {

        //calculate the center of the arc
        Vector3 center = (startPosition.position + targetPosition.position) * 0.5f;

        //lower it a bit so it won't be an obvious arc
        center -= new Vector3(0, 5, 0);

        //lower the start and target based on the center
        Vector3 StartRelevativeCenter = startPosition.position - center;
        Vector3 TargetRelevativeCenter = targetPosition.position - center;

        //journeytime
        float complete = (Time.time - startTime) / journeyTime;

        //update the position
        transform.position = Vector3.Slerp(StartRelevativeCenter, TargetRelevativeCenter, complete);
        transform.position += center;

        //destroy game object.
        if (transform.position == targetPosition.position)
        { 

           Destroy(gameObject);
        }

    }

    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("Cannon is waiting");
    }

    private void OnTriggerStay(Collider collision)
    {
        //if it collides with the player, knock it 4 steps forward and stun the player.
        if (collision.transform.tag == "Player")
        {
            playerMovement.targetPosition = playerMovement.transform.position + (Vector3.forward * 4);

            collision.transform.GetComponent<PlayerMovement>().stunned = true;
            collision.transform.GetComponent<PlayerMovement>().startCountdown = true;

            

        }
    }

}
