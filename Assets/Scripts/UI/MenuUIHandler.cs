using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    private MainManager mainManager;

    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    public void Menu() //Return to menu
    {
        mainManager.Menu();
    }

    public void Restart()//Start new game
    {
        mainManager.StartGame();
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
