using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerMovement playerMovement;

    [SerializeField] Vector3 targetPosition;
    float moveTimer = 2;
    [SerializeField] float moveTime;
    [SerializeField] float speed;
    [SerializeField] float gravity;
    [SerializeField] float verticalSpeed;
    [SerializeField] bool isFalling = false;

    [SerializeField] bool reachedTargetPosition = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        targetPosition = transform.position;
        moveTimer = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        if (transform.position == targetPosition && moveTimer == 0)
        {
            RaycastHit hit;
            Ray rangeRay = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(rangeRay, out hit, 1f))
            {
                if (!reachedTargetPosition)
                {
                    // Start move timer
                    reachedTargetPosition = true;
                    moveTimer = moveTime;
                }
                else
                {
                    // Start movement
                    reachedTargetPosition = false;
                    setTargetPosition();
                }
            }
            else
            {
                // Die
                isFalling = true;
            }
        }
        moveTimer = timer(moveTimer, false);
        

        if (!isFalling)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            verticalSpeed += gravity * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down * verticalSpeed, verticalSpeed * Time.deltaTime);
        }


        // Rotate towards moving direction
        transform.LookAt(targetPosition, Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            //(Jesse: Plays punch sound when player gets hit by enemy)
            LevelAudio.PlayAudio("punch");

            collision.transform.GetComponent<PlayerMovement>().willBeStunned = true;
            collision.transform.GetComponent<PlayerMovement>().startCountdown = true;

            if (reachedTargetPosition)
            {
                // Push back player
                playerMovement.targetPosition = playerMovement.previousPosition;
            }
            else
            {
                // Find player target position
                float targetPositionX = player.transform.position.x;
                if (targetPosition.x > transform.position.x)
                {
                    targetPositionX = findTargetPosition((int) player.transform.position.x, 1);
                }
                else if (targetPosition.x < transform.position.x)
                {
                    targetPositionX = findTargetPosition((int) player.transform.position.x, -1);
                }

                // Push back player
                playerMovement.targetPosition = new Vector3(targetPositionX, player.transform.position.y, player.transform.position.z);
            }
        }
    }

    private void setTargetPosition()
    {
        float targetPositionX = playerMovement.targetPosition.x;
        if (targetPositionX > transform.position.x)
        {
            targetPositionX = findTargetPosition(transform.position.x, 1); //transform.position.x + playerMovement.amountOfSpaces;
        }
        else if (targetPositionX < transform.position.x)
        {
            targetPositionX = findTargetPosition(transform.position.x, -1); //transform.position.x - playerMovement.amountOfSpaces;
        }
        targetPosition = new Vector3(targetPositionX, transform.position.y, transform.position.z);
    }

    public static int findTargetPosition(float position, int direction)
    {
        // Get target position in direction
        position += direction;
        int targetPosition = (int)position;
        direction = (int) Mathf.Sign(direction);
        while (targetPosition % 4 != 0)
        {
            targetPosition += direction;
        }
        return targetPosition;
    }

    private float timer(float currentTime, bool unscaled)
    {
        if (unscaled)
        {
            currentTime -= Time.unscaledDeltaTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
        if (currentTime < 0)
        {
            currentTime = 0;
        }
        return currentTime;
    }
}
