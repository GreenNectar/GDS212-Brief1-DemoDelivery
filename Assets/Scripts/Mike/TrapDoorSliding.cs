using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorSliding : TrapDoor
{
    //private float startRotation;
    //public float rotation;
    //public bool turnsLeft;
    //public float speed;

    //private void Start()
    //{
    //    startRotation = transform.eulerAngles.z;
    //}

    //private void Update()
    //{
    //    float wantedRotation = isOpen ? startRotation + rotation : startRotation;
    //    if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, wantedRotation)) > speed * Time.deltaTime)
    //    {
    //        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + ((isOpen ? 1f : -1f) * (turnsLeft ? 1f : -1f) * speed * Time.deltaTime));
    //    }
    //    else
    //    {
    //        transform.rotation = Quaternion.Euler(0, 0, wantedRotation);
    //    }
    //}
    private Vector3 startPosition;
    public Vector3 newPosition;
    public bool movesLeft;
    public float speed;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        
        if(Mathf.Abs(Vector3.Distance(startPosition, newPosition)) > speed * Time.deltaTime)
        {
            
        }
    }
}
