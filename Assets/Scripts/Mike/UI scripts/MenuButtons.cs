using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created by Michael

public class MenuButtons : MonoBehaviour
{
    
    // when the connected button is clicked 
    public void LoadLevel(string level)
    {

        Debug.Log("clicked play");
        SceneManager.LoadScene(level);
        
    }


}
