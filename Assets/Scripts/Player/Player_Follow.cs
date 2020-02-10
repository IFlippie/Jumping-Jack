using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
  //  [SerializeField]
   // private Transform PlayerTransform;

   // private Vector3 _cameraOffset;

   // [SerializeField]
   // private float SmoothFactor = 0.5f;

   // [SerializeField]
   // private bool LookAtPlayer = false;

    void Start()
    {
        //_cameraOffset = transform.position - PlayerTransform.position;
    }

    // So that this is the last thing that's being updated.
    void LateUpdate()
    {
       // Vector3 newPos = PlayerTransform.position + _cameraOffset;

      //  transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

       // if (LookAtPlayer)
        //    transform.LookAt(PlayerTransform);
    }
}
