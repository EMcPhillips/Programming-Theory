using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public InputField inputName;
    public string input;

    public void StoreName() //Saves name input to playerName in MainManager and sets playerHasName to true
    {
        Debug.Log(inputName.text);
        MainManager.Instance.playerName = inputName.text;
        MainManager.Instance.playerHasName = true;
    }
}
