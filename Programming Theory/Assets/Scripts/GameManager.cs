using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerHealth;
    public int killCount = 0;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI currentObjectivetext;
    public TextMeshProUGUI gameWonText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killCounttext;
    public Button menuButton;
    public Button restartButton;
    public bool gameOver = false;
    public bool gameWon = false;
    public bool firstObjective = false; //PlayerController.cs
    public bool secondObjective = false; //PlayerController.cs
    public bool thirdObjective = false; //Shoot.cs
    public bool fourthObjective = false; //EnemyBehaviour.cs
    public bool fifthObjective = false; //Spawner.cs

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        UI();
        CurrentObjective();
        GameWon();
        KillCount();
    }

    void CheckDead()
    {
        if (playerHealth <= 0)
        {
            gameOver = true;
            gameOverText.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            Cursor.visible = true;
            Debug.Log("Game Over");
        }
        else if (playerHealth <= 30)
        {
            healthText.color = Color.red;
        }
    }

    private void UI()
    {
        healthText.text = "Player health : " + playerHealth;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void GameWon()
    {
        if (gameWon == true)
        {
            gameWonText.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            Cursor.visible = true;
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
