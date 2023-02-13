using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int m_playerHealth;
    public int playerHealth
    {
        get { return m_playerHealth; }
        set { m_playerHealth = value; }
    }
    public int enemyHealth;
    public float killCount;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI currentObjectivetext;
    public TextMeshProUGUI gameWonText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killCounttext;
    public TextMeshProUGUI finalScore;
    public Button menuButton;
    public Button restartButton;
    public float timer = 0f;
    public float killMultiplier = 5.0f;
    public bool gameOver = false;
    public bool gameWon = false;
    public bool firstObjective = false; //PlayerController.cs
    public bool secondObjective = false; //PlayerController.cs
    public bool thirdObjective = false; //Shoot.cs
    public bool fourthObjective = false; //EnemyBehaviour.cs
    public bool fifthObjective = false; //Spawner.cs
    private MainManager mainManager;
    // Start is called before the first frame update
    void Start()
    {
        m_playerHealth = 100;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        timer = mainManager.timer;
        killCount = mainManager.killCount;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        UI();
        CurrentObjective();
        GameWon();
        KillCount();
        Score();
    }

    void CheckDead()
    {
        if (m_playerHealth <= 0)
        {
            gameOver = true;
            gameOverText.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true); 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Game Over");
        }
        else if (m_playerHealth <= 30)
        {
            healthText.color = Color.red;
        }
    }

    private void UI()
    {
        healthText.text = "Player health : " + m_playerHealth;
    }


    public void GameWon()
    {
        if (gameWon == true)
        {
            gameWonText.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Score()
    {
        if (!gameOver && !gameWon)
        {
            timer += 1 * Time.deltaTime; 
            timerText.text = "Time: " + Mathf.Round(timer);
        }
        else if(gameWon == true)
        {
            finalScore.gameObject.SetActive(true);
            finalScore.text = "Final score: " + (Mathf.Round(timer) - (killCount * killMultiplier));
        }
    }

    private void KillCount()
    {
        killCounttext.text = "Enemies destroyed: " + killCount;
    }


    public void CurrentObjective()
    {
        if (firstObjective == false)
        {
            currentObjectivetext.text = "Use W, A, S and D to move";
        }
        else if (firstObjective == true)
        {
            if (secondObjective == false)
            {
                currentObjectivetext.text = "Hold W and Left Shift to sprint";
            }
            else if (secondObjective == true)
            {
                if (thirdObjective == false)
                {
                    currentObjectivetext.text = "Left Mouse click to shoot";
                }
                else if(thirdObjective == true)
                {
                    if (fourthObjective == false)
                    {
                        currentObjectivetext.text = "Shoot the creatures that have invaded your town!";
                    }
                    else if (fourthObjective == true)
                    {
                        if (fifthObjective == false)
                        {
                            currentObjectivetext.text = "Find out where the creatures are coming from";
                        }
                        else if (fifthObjective == true)
                        {
                            currentObjectivetext.text = "Defeat their leader and save the town!";
                        }
                    }
                }
            }
        }
    }
}
