using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLIDE");
        if (other.tag == "Player")
        {
            Debug.Log("COLLIDE WITH PLAYER");
            // count up coin amount
            GameObject.Find("Coin Count").GetComponent<CoinCount>().coins++;
            Destroy(gameObject);
        }
    }
}
