using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public bool difficultySet = false;
    [SerializeField]
    private int m_enemyHealth = 100;
    public int enemyHealth //ENCAPSULATION
    {
        get { return m_enemyHealth; }
        set { m_enemyHealth = value; }
    }
    public int killCount;
    public int difficulty;
    public float timer;

    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log(Application.persistentDataPath);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Menu(); //ABSTRACTION
        }
    }
    public void Menu() //Return to menu
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        if (difficultySet == true)
        {
            SceneManager.LoadScene(1);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            killCount = 0;
            timer = 0f;
        }
    }

    public void StartNew(int difficulty)
    {
        if (!difficultySet)
        {
            m_enemyHealth *= difficulty;
            difficultySet = true;
        }
        else
        {
            m_enemyHealth = 0;
            m_enemyHealth = difficulty * 100;
        }
    }

}

