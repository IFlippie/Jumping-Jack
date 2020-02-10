using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTile : MonoBehaviour
{
    private bool isActive = false;
    private float activeTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get Activated
        if (!isActive)
        {
            RaycastHit hit;
            Ray rangeRay = new Ray(transform.position, transform.up);
            if (Physics.Raycast(rangeRay, out hit, 1f))
            {
                isActive = true;
            }
        }

        if (isActive)
        {
            activeTimer = timer(activeTimer, false);
            if (activeTimer == 0)
            {
                explode();
            }
        }

    }

    private void explode()
    {
        /// Instert Destruction Here
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
