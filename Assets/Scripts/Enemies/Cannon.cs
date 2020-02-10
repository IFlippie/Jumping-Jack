using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particle;

    [SerializeField]
    GameObject ballPrefab;

    //public List <GameObject> cannonBalls;

    [SerializeField]
    private Transform[] TargetPositions;

    int randomGeneratedIndex;
    Transform randomTargetPos;

    [HideInInspector]
    GameObject currentBall;

    [SerializeField]
    int amountSpawnAllowed = 5;

    [SerializeField]
    Transform startPos;

    public float speed = 5;

    bool coroutineAllowed;

    CannonBall cannonBall;

    void Start()
    {
        coroutineAllowed = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        StartCoroutine(CannonBallSpawning());

    }

    IEnumerator CannonBallSpawning()
    {
        coroutineAllowed = false;

            //instansiate ball
            currentBall = Instantiate(ballPrefab, startPos.position, Quaternion.identity);

            //do particle stuff.
             particle.Play();
            
            //generate index for position in array
            randomGeneratedIndex = Random.Range(0, TargetPositions.Length);
            randomTargetPos = TargetPositions[randomGeneratedIndex];

            cannonBall = currentBall.GetComponent<CannonBall>();
            
            //give positions to cannonball script.
            cannonBall.Positions(startPos, randomTargetPos);


            //Add to the list
            //cannonBalls.Add(currentBall);
            
        

            /*if (cannonBalls.Count == amountSpawnAllowed)
              {
    
            for (int i = 0; i < cannonBalls.Count; i++)
               {

                Debug.Log("Destroyed");
                cannonBalls.RemoveAt(i);

                GameObject.Destroy(cannonBalls[i]);
                */

                yield return StartCoroutine(WaitForSeconds(2.0f));

        coroutineAllowed = true;
    }

    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("Cannon is waiting");
    }

}



//rb = currentBall.GetComponent<Rigidbody>();

//rb.AddForce(transform.forward * speed);