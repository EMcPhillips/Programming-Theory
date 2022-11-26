using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

// Attatched to the canvas game object in all scenes

public class MenuUIHandler : MonoBehaviour
{
    public void Menu() //Return to menu
    {
        SceneManager.LoadScene(0);
    }

    public void StartNew()//Start new game
    {

        //if (MainManager.Instance.playerHasName == true)
        //{
            //SceneManager.LoadScene(1);
           // MainManager.Instance.LoadHighScore();
        //}
    }

    public void Exit() //Exits the game
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); //Closes the application
#endif
    }
}
