using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingFish : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private float speed;
    GameObject player;
    PlayerMovement playerMovement;
    
    
    private float time;

    private Vector3 fishPosition;
    private int routeToGo;

    private bool coroutineAllowed;


    /* to use fishi, first add fishie and routeforfish to scene
     * create the loop which the fishi will follow (just drag begin, point 2 & 3 and end to position u want
     * in fishi add the route you just created sow fishi will follow.
     */

    void Start()
    {
        //get player movement script so we can stun it
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

        routeToGo = 0;
        time = 0f;
        coroutineAllowed = true;
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    IEnumerator GoByTheRoute(int routeNumber)
    {

        coroutineAllowed = false;
        //get points of the brezier out of childs of followroute.
        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (time < 1)
        {
            time += Time.deltaTime * speed;

            /*
            formula for a Quadratic Brezier Curve
            B(t) = (1 - t) ^ 3 * P0 +
            3t(1 - t) ^ 2 * P1 +
            3t ^ 2(1 - t) P2 +
            t ^ 3 P3
            */

            fishPosition = Mathf.Pow(1 - time, 3) * p0 +
                    3 * Mathf.Pow(1 - time, 2)* time * p1 +
                    3 * (1 - time) * Mathf.Pow(time, 2) * p2 +
                    Mathf.Pow(time, 3) * p3;

            //make fish look at the target position so it arc's
            transform.LookAt(fishPosition);
            transform.position = fishPosition;
            

            yield return new WaitForEndOfFrame();
           
        }

        time = 0f;
        routeToGo += 1;

        //reset route length
        if (routeToGo > routes.Length - 1)
            routeToGo = 0;
        

        yield return StartCoroutine(WaitForSeconds(2.0f));

        coroutineAllowed = true;
    }
    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("Fishi is waiting under water");
    }

    //if fish collides with player it will be shoved back to it's previous position.
    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {

            collision.transform.GetComponent<PlayerMovement>().stunned = true;
            collision.transform.GetComponent<PlayerMovement>().startCountdown = true;

            playerMovement.targetPosition = playerMovement.previousPosition;

        }
    }
}
