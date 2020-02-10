using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Canon : MonoBehaviour
{


    [SerializeField]
    private Transform[] TargetPositions;
    //target positions

    [SerializeField]
    private Transform startPos;

    public float speed = 2;
    public float arcHeight;

    bool spawned = true;
    bool instanciatedBall = false;

    private int randomChosenTarget;

    GameObject cannonBall;

    [SerializeField]
    GameObject Ball;

    Vector3 nextPos;

    void Update()
    {
        if (instanciatedBall == false)
        {
            randomChosenTarget = Random.Range(0, TargetPositions.Length);

            cannonBall = Instantiate(Ball, startPos.position, Quaternion.identity);

            instanciatedBall = true;

        }

        if (spawned)
        {
            cannonBall.transform.position = CalculatePositionWithArc(TargetPositions[randomChosenTarget]);
        }

    }

    //y = -(x-x/2)^2 + b + c



    Vector3 CalculatePositionWithArc(Transform targetPos)
{

        
        //Vector3 nextPos;
        //    float x0 = spawnPosition.position.x;
        //    float x1 = targetPosition.position.x;

        //    float dist = x1 - x0;


        //    float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        //    float baseY = Mathf.Lerp(spawnPosition.position.y, targetPosition.position.y, (nextX = x0) / dist);

        //    float arc = archHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);

        //    nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        //    if (nextPos == targetPosition.position)
        //    {
        //        Arrived();
        //    }

        //    return nextPos;
            


        //Do something when we reach the target
    if (nextPos == targetPos.position) Arrived();

        return nextPos;

        //transform.position = nextPos;
    }




void Arrived()
{
    Destroy(cannonBall);

        spawned = false;
        instanciatedBall = false;
    }
    
}
