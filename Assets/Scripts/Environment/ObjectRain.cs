using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRain : MonoBehaviour
{
    //Spawing object rain after ... seconds
    private readonly float MaxCountdown = 1.5f;
    private float countdown = 0;

    //Calculate spawn positions
    [SerializeField] private float dropHeight = 27f;
    private readonly int MinCalcTileX = -8;
    private readonly int TileSpacing = 4;
    private Vector3[] dropPos;

    private int calcTileX = -8;
    private int calcTileZ = 0;

    private bool isTriggered;
    [SerializeField] GameObject fallingObject;

    // TileSpacing (4) * rows (8)
    //private float areaSizeZ = 32;



    // Start is called before the first frame update
    void Start()
    {
        // 40 / 5 = 8 rows
        dropPos = new Vector3[40];

        //Get fall positions 8 rows
        for (int iSlot = 0; iSlot < dropPos.Length; iSlot++)
        {
            //Gets position above each tile
            dropPos[iSlot] = new Vector3(transform.position.x + calcTileX, dropHeight,
                transform.position.z + calcTileZ);
            //Row itself from right to left
            calcTileX += TileSpacing;

            //Checks if calc needs to move on to the next row
            if (iSlot == 4 || iSlot == 9 || iSlot == 14 || iSlot == 19 || iSlot == 24 ||
                iSlot == 29 || iSlot == 34 || iSlot == 39) 
            {
                ResetCalcTile();
                //Adds another row
                calcTileZ += TileSpacing;
            }
        }
    }

    private void ResetCalcTile() {
        calcTileX = MinCalcTileX;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //player walked through object
        if (isTriggered) {
            countdown -= Time.deltaTime;
            //after countdown spawn falling objects
            if (countdown <= 0) {
                for (int i = 0; i < 2; i++)
                {
                    //spawn two objects
                    SpawnObject(Random.Range(0, dropPos.Length));
                }
                ResetCountdown();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            isTriggered = true;
            //When off: can't be triggered twice
            GetComponent<Collider>().enabled = false;
        }
    }

    private void ResetCountdown() {
        countdown = MaxCountdown;
    }

    private void SpawnObject(int iSlot) {
        Instantiate(fallingObject, dropPos[iSlot], transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        //Invis trigger
        Gizmos.DrawCube(transform.position, Vector3.one);

        //Affected area - (if var areaSize used space is halfed?)
        // 16 = areaSize / 2
        // 32 = areaSize
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, 16), new Vector3(20, dropHeight / 2, 32));
    }
}
