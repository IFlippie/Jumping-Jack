using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 targetDistance = new Vector3(0, -8.6f, 4.5f);
    Quaternion targetRotation;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;


    private void Start()
    {
        transform.position = target.position - targetDistance;
        transform.LookAt(target.position + Vector3.up * 6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position - targetDistance, moveSpeed * 
            (Vector3.Distance(transform.position, target.position - targetDistance) / (moveSpeed/2)) * Time.deltaTime);

        Quaternion currentRotation = transform.rotation;
        //transform.LookAt(target.position + Vector3.up * 2f);
        targetRotation = transform.rotation;

        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
