using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorRotate : TrapDoor
{
    private float startRotation;
    public float rotation;
    public bool turnsLeft;
    public float speed;

    private void Start()
    {
        startRotation = transform.eulerAngles.z;
    }

    public void Open()
    {
        StartCoroutine(OpenTrapDoorSequence());
    }

    public void Close()
    {
        StartCoroutine(CloseTrapDoorSequence());
    }

    protected override IEnumerator OpenTrapDoorSequence()
    {
        bool hasFinished = false;
        while (!hasFinished)
        {
            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, startRotation + rotation)) > speed * Time.deltaTime)
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + (turnsLeft ? 1 : -1) * speed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, startRotation + rotation);
                hasFinished = true;
            }

            yield return null;
        }
    }

    protected override IEnumerator CloseTrapDoorSequence()
    {
        bool hasFinished = false;
        while (!hasFinished)
        {
            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, startRotation - rotation)) > speed * Time.deltaTime)
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - (turnsLeft ? 1 : -1) * speed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, startRotation - rotation);
                hasFinished = true;
            }

            yield return null;
        }
    }
}
