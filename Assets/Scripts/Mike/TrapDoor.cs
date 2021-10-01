using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public int doorNumber;

    private void OnDisable()
    {
        // EventSystem.onButtonPress.RemoveListener(OpenTrapDoor);
        // EventSystem.onButtonRelease.RemoveListener(CloseTrapDoor);
    }

    private void OnEnable()
    {
        // EventSystem.onButtonPress.AddListener(OpenTrapDoor);
        // EventSystem.onButtonRelease.AddListener(CloseTrapDoor);
    }


    void OpenTrapDoor(int doorNumber)
    {
        if (this.doorNumber == doorNumber)
        {
            StopCoroutine(CloseTrapDoorSequence());
            StartCoroutine(OpenTrapDoorSequence());
        }
    }
    void CloseTrapDoor(int doorNumber)
    {
        if (this.doorNumber == doorNumber)
        {
            StopCoroutine(OpenTrapDoorSequence());
            StartCoroutine(CloseTrapDoorSequence());
        }
    }

    protected virtual IEnumerator OpenTrapDoorSequence()
    {
        yield return null;
    }

    protected virtual IEnumerator CloseTrapDoorSequence()
    {
        yield return null;
    }
}
