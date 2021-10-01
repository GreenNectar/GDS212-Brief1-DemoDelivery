using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate90 : MonoBehaviour
{

    public bool active;
    public int startRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // needs to be lerped

        if (active)
        {
            // take the transform rotation and change its rotation to 90 (on the forward axis to make it rotate from the facing that matters)
            transform.rotation = Quaternion.Euler(Vector3.forward * 90);
        }
        else
        {
            // take the transform rotation and reset it to the set number
            transform.rotation = Quaternion.Euler(Vector3.forward * startRotation);
        }


    }
}
