using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class PlayerMovement : MonoBehaviour
{
    // movement
    [SerializeField]
    [HideInInspector] public float amountOfSpaces;
    private float minHeight;
    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] public Vector3 previousPosition;
    [SerializeField] float speed;
    [SerializeField] float gravity;
    public float verticalSpeed;
    [SerializeField] public bool isFalling = false;

    //swiping input
    private Vector2 endPosition;
    private Vector2 startPosition;
    [SerializeField] private bool isMoving = false;
    private static readonly int SWIPE_THRESHOLD = 69;
    private static readonly int SWIPE_VELOCITY_THRESHOLD = 69;
    private float startTime;
    private float diffTime;

    //Player Stun
    [HideInInspector] public bool willBeStunned;
    [HideInInspector] public bool stunned;
    [HideInInspector] public bool startCountdown;

    private float countdown;
    private readonly float StunTime = 1.5f;

    //play sounds
    private float waterLevel = -0.5f;
    private bool playedSplash;

    //Jesse: Analytics
    private int enemyHits;
    private int playerMoves;

    //stunbirds
    [SerializeField] GameObject stunBird;
    private bool birdsCanSpawn;

    private void Start()
    {
        minHeight = transform.position.y;
        targetPosition = transform.position;

        
        countdown = StunTime;
    }


    // Update is called once per frame
    void Update()
    {
        // Fall (die)
        if (isFalling)
        {
            verticalSpeed += gravity * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down * verticalSpeed, verticalSpeed * Time.deltaTime);
        }

        // Idle on tile
        if (transform.position == targetPosition)
        {
            // Set variables
            isMoving = false;
            if (willBeStunned)
            {
                stunned = true;
                willBeStunned = false;
            }


            // Check if tile exists
            RaycastHit hit;
            Ray rangeRay = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(rangeRay, out hit, 1f))
            {
                if (hit.transform.GetComponent<FallBlock>() != null) 
                {
                    //call collision script
                    hit.transform.GetComponent<FallBlock>().onPlayerEnter();

                    if (hit.transform.GetComponent<FallBlock>().falling == true)
                    {
                        isFalling = true;
                        //Debug.Log("Fall");
                    }
                }
            }
            else
            {
                isFalling = true;
                //Debug.Log("Fall");
            }

            // Input
            if (!isFalling && !stunned)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    onSwipeTop();
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    onSwipeBottom();
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    onSwipeLeft();
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    onSwipeRight();
                }
            }

        }
        else
        {
            // Set moving variable
            isMoving = true;
        }
        


        //if (noKeyPressed && 
        if (!isFalling)  //transform.position.y >= minHeight)
        {
            if (!stunned)
            {
                //Find beginning and end of swipe + time to use in the checkSwipe method

                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        startPosition = touch.position;
                        endPosition = touch.position;
                        startTime = Time.time;
                    }

                    if (transform.position != targetPosition)
                    {
                        //Debug.Log("Reset Start position");
                        startPosition = touch.position;
                    }

                    if (transform.position == targetPosition && touch.phase == TouchPhase.Moved)
                    {
                        endPosition = touch.position;
                        if (checkSwipe(startPosition, endPosition) == true)
                        {
                            startPosition = touch.position;
                        }
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        endPosition = touch.position;
                        if (checkSwipe(startPosition, endPosition) == true)
                        {
                            startPosition = touch.position;
                        }
                        diffTime = Time.time - startTime;
                        startTime = 0;
                    }
                }
                
            }
        }

        if (!isFalling)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        // Rotate towards moving direction
        transform.LookAt(targetPosition, Vector3.up);
    }

    private void FixedUpdate()
    {
        if (startCountdown)
        {
            //time the player is stunned
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                //stun reset
                stunned = false;
                StopCountdown();
                ResetCountdown();
            }
        }

        if (transform.position.y <= waterLevel) {
            if (!playedSplash)
            {
                LevelAudio.PlayAudio("watersplash");
                playedSplash = true;

                //Analytics.CustomEvent("Enemy hits + Lose", new Dictionary<string, object>
                //    {
                //        {"Enemy hits: ", enemyHits},
                //        {"Scene", SceneManager.GetActiveScene().name}
                //    });

                //Analytics.CustomEvent("Player Movement + Lose", new Dictionary<string, object>
                //    {
                //        {"Movements: ", playerMoves},
                //        {"Scene", SceneManager.GetActiveScene().name}
                //    });

            }
        }

        //spawn stun birds
        if (stunned & birdsCanSpawn)
        {
            SpawnStunBirds();
        }

        if (!stunned) {
            birdsCanSpawn = true;
        }

    }

    //Movement of your finger is checked from beginning to end to calculate velocity and distance and determine which direction it is
    bool checkSwipe(Vector2 startPosition, Vector2 endPosition)
    {
        bool result = false;
        float diffY = startPosition.y - endPosition.y;
        float diffX = startPosition.x - endPosition.x;
        //print("startposition is " + startPosition);
        //print("endposition is " + endPosition);
        float velocityY = diffY / diffTime;
        float velocityX = diffX / diffTime;
        if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
        {
            if (Mathf.Abs(diffX) > SWIPE_THRESHOLD && Mathf.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
            {
                if (diffX > 0)
                {
                    startPosition = endPosition;
                    onSwipeLeft();
                }
                else
                {
                    startPosition = endPosition;
                    onSwipeRight();
                }
                result = true;
            }
        }
        else
        {
            if (Mathf.Abs(diffY) > SWIPE_THRESHOLD && Mathf.Abs(velocityY) > SWIPE_VELOCITY_THRESHOLD)
            {
                if (diffY > 0)
                {
                    startPosition = endPosition;
                    onSwipeBottom();
                }
                else
                {
                    startPosition = endPosition;
                    onSwipeTop();
                }
                result = true;
            }
        }

        return result;
    }

    private void onSwipeTop()
    {
        previousPosition = transform.position;
        targetPosition = transform.position + Vector3.forward * amountOfSpaces;
        playerMoves++;

        RaycastHit hit;
        Ray rangeRay = new Ray(targetPosition, Vector3.down);
        if (!Physics.Raycast(rangeRay, out hit, 1f))
        {
            if (GetComponent<PlaceTile>() != null)
            {
                //Debug.Log("Has Component");
                GetComponent<PlaceTile>().placeTile(targetPosition);
            }
        }
    }

    private void onSwipeBottom()
    {
        previousPosition = transform.position;
        targetPosition = transform.position + Vector3.back * amountOfSpaces;
        playerMoves++;

        RaycastHit hit;
        Ray rangeRay = new Ray(targetPosition, Vector3.down);
        if (!Physics.Raycast(rangeRay, out hit, 1f))
        {
            if (GetComponent<PlaceTile>() != null)
            {
                //Debug.Log("Has Component");
                GetComponent<PlaceTile>().placeTile(targetPosition);
            }
        }
    }

    private void onSwipeLeft()
    {
        previousPosition = transform.position;
        targetPosition = transform.position + Vector3.left * amountOfSpaces;
        playerMoves++;

        RaycastHit hit;
        Ray rangeRay = new Ray(targetPosition, Vector3.down);
        if (!Physics.Raycast(rangeRay, out hit, 1f))
        {
            if (GetComponent<PlaceTile>() != null)
            {
                //Debug.Log("Has Component");
                GetComponent<PlaceTile>().placeTile(targetPosition);
            }
        }
    }

    private void onSwipeRight()
    {
        previousPosition = transform.position;
        targetPosition = transform.position + Vector3.right * amountOfSpaces;
        playerMoves++;

        RaycastHit hit;
        Ray rangeRay = new Ray(targetPosition, Vector3.down);
        if (!Physics.Raycast(rangeRay, out hit, 1f))
        {
            if (GetComponent<PlaceTile>() != null)
            {
                //Debug.Log("Has Component");
                GetComponent<PlaceTile>().placeTile(targetPosition);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FallingObject")
        {
            //Hit by object rain
            StunPlayer();
        }
    }

    //Jesse 
    private void ResetCountdown()
    {
        countdown = StunTime;
    }

    private void StopCountdown() {
        startCountdown = false;
    }

    private void StunPlayer() {
        stunned = true;
        //time player is stunned for
        startCountdown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemyHits++;
        }
    }

    private void SpawnStunBirds() {
        //so it doesn't loop this method
        birdsCanSpawn = false;
        //Pos: Around player
        Instantiate(stunBird, transform.position + new Vector3(0,1,-1), stunBird.transform.rotation);
        Instantiate(stunBird, transform.position + new Vector3(0, 1, 1), stunBird.transform.rotation);
        Instantiate(stunBird, transform.position + new Vector3(-1, 1, 0), stunBird.transform.rotation);
        Instantiate(stunBird, transform.position + new Vector3(1, 1, 0), stunBird.transform.rotation);
    }

    //public void offMovingTile()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        previousPosition = transform.position;
    //        targetPosition = (transform.position + Vector3.forward * amountOfSpaces) + new Vector3(0, 0.1f);
    //    }
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        previousPosition = transform.position;
    //        targetPosition = (transform.position + Vector3.back * amountOfSpaces) + new Vector3(0, 0.1f);
    //    }
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        previousPosition = transform.position;
    //        targetPosition = (transform.position + Vector3.left * amountOfSpaces) + new Vector3(0, 0.1f);
    //    }

    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        previousPosition = transform.position;
    //        targetPosition = (transform.position + Vector3.right * amountOfSpaces) + new Vector3(0, 0.1f);
    //    }
    //}

    //private IEnumerator WaitTimer()
    //{
    //    yield return new WaitForSeconds(waitSec);
    //    noKeyPressed = true;
    //}
}
