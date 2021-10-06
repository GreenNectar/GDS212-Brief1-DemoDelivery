using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created by Michael

public class MenuButtons : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    // when the connected button is clicked 
    public void OnClickPlay()
    {
        Debug.Log("clicked play");
        //SceneManager.LoadScene(level select scene)
    }

    public void OnClickOptions()
    {
        Debug.Log("options opened");
        //SceneManager.LoadScene(options scene)
    }

    public void OnClickCredits()
    {
        Debug.Log("credits seen");
        //SceneManager.loadScene(credits scene)
    }
}
