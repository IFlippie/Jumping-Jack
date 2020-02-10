using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class FallBlock : MonoBehaviour
{
    //Tiles will always fall
    [SerializeField] private float fallAfterSec = 5f;

    [SerializeField] private float fragileSec = 1f;
    [SerializeField] private float bombSec = 1.5f;
    private int superFragileSec = 0;
    private float speed = 3.9f;

    private readonly int MinY = -5;
    public bool falling;
    public bool willFall;
    private bool toTheRight;
    private PlayerMovement player2;
    private GameObject player;

    //moving tile
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;
    [SerializeField] private float movingTileSpeed = 0.3f;
    //private Vector3 posDiffVer = new Vector3(4f, 0f, 0f);
    //private Vector3 posDiffHor = new Vector3(0f, 0f, 4f);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FallTimer());

        player = GameObject.FindWithTag("Player");
        player2 = player.GetComponent<PlayerMovement>();
        //pos1 = transform.position;
        //pos2 = transform.position + posDiffVer;
    }

    void FixedUpdate()
    {
        if (falling) {
            Fall();
        }


        if (transform.position.y < MinY) {
            Destroy(gameObject);
        }

        if (gameObject.tag == "Moving")
        {
            //lerp is for finding a position between pos1 and pos2 which is indicated by a number between 0 and 1, 
            //PingPong gives a certain speed with a limit where it will bounce back and forward between 0 and the limit
            transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * movingTileSpeed, 1.0f));
        }
    }

    //for fragile blocks
    void OnCollisionStay(Collision other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Enemy")
        {
            //Debug.Log("before Hit");
            if (other.transform.position.x == transform.position.x)
            {
                //Debug.Log("Hit");
                onPlayerEnter();
            }
        }
    }
    void OnCollisionExit(Collision other)
    {
        player2.transform.parent = null;
    }

        public void onPlayerEnter()
    {
        if (!willFall)
        {
            switch (gameObject.tag)
            {
                case "Fragile":
                    //player2.targetPosition = transform.position + new Vector3(0, 0.75f);
                    fallAfterSec = fragileSec;
                    StartCoroutine(FallTimer());
                    willFall = true;
                    break;

                case "SuperFragile":
                    fallAfterSec = superFragileSec;
                    StartCoroutine(FallTimer());
                    falling = true;
                    willFall = true;
                    AnalyticsEvent.Custom("Death by Super Fragile Crate");
                    break;

                case "Moving":
                        player.transform.position = transform.position + new Vector3(0, 2.55f);
                    player2.targetPosition = player.transform.position;
                    break;

                case "field":
                    //player2.targetPosition = transform.position + new Vector3(0, 2.55f);
                    break;

                case "PickUp":
                    Debug.Log("HOLD ME MOMMY");
                    //count up the tiles amount
                    GameObject.Find("Pick Up Tile Count").GetComponent<PickUpTileCount>().tiles++;
                    Destroy(gameObject);
                    break;

                case "Bomb":
                    //player2.targetPosition = transform.position + new Vector3(0, 0.75f);
                    fallAfterSec = bombSec;
                    StartCoroutine(FallTimer());
                    willFall = true;

                    //Debug.Log("BOOM");
                    RaycastHit hit;
                    Ray rangeRay;


                    /// start other falltimers
                    // Left
                    rangeRay = new Ray(transform.position, -transform.right);
                    if (Physics.Raycast(rangeRay, out hit, 4f))
                    {
                        HitBlockExplosion(hit);
                    }
                    // Right
                    rangeRay = new Ray(transform.position, transform.right);
                    if (Physics.Raycast(rangeRay, out hit, 4f))
                    {
                        HitBlockExplosion(hit);
                    }
                    // Up
                    rangeRay = new Ray(transform.position, transform.forward);
                    if (Physics.Raycast(rangeRay, out hit, 4f))
                    {
                        HitBlockExplosion(hit);
                    }
                    // Down
                    rangeRay = new Ray(transform.position, -transform.forward);
                    if (Physics.Raycast(rangeRay, out hit, 4f))
                    {
                        HitBlockExplosion(hit);
                    }
                    break;
            }

        }
    }

    private IEnumerator FallTimer()
    {
        yield return new WaitForSeconds(fallAfterSec);
        falling = true;
    }

    private void Fall() {
        transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
    }

    private void HitBlockExplosion(RaycastHit hit)
    {
        FallBlock blockScript = hit.transform.GetComponent<FallBlock>();
        if (blockScript.willFall == false)
        {
            blockScript.fallAfterSec = bombSec + 0.25f;
            blockScript.bombSec = bombSec + 0.25f;
            if (hit.transform.tag == "Bomb") blockScript.onPlayerEnter();
            blockScript.willFall = true;
            blockScript.StartCoroutine(blockScript.FallTimer());
            hit.transform.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;
            hit.transform.GetComponent<Renderer>().materials = GetComponent<Renderer>().materials;
        }
    }
}
