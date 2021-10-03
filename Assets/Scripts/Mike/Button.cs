using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private float startDistance;
    private bool isPressed = false;
    public Rigidbody2D parent;
    public int doorNumber;

    private void Start()
    {
        startDistance = Vector2.Distance(transform.position, parent.position);
    }

    private void Update()
    {
        if (!isPressed)
        {
            if (Vector2.Distance(transform.position, parent.position) <= 0.5f * startDistance)
            {
                isPressed = true;
                EventManager.onButtonPress.Invoke(doorNumber);
            }

        }
        else
        {
            if (Vector2.Distance(transform.position, parent.position) > 0.5f * startDistance)
            {
                isPressed = false;
                EventManager.onButtonRelease.Invoke(doorNumber);
            }
        }
        
    }



}