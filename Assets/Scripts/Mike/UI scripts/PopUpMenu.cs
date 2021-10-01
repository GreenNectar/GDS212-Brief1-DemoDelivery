using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{

    public GameObject popUpBox;
    public TMP_Text menuText;


    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        menuText.text = text;
    }
}
