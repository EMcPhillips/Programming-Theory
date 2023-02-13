using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject projectilePrefab;
    private Rigidbody enemyRb;
    public float force = 1.0f;
    public float cooldown = 1.0f;
    public bool hasShot = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameOver == false || gameManager.gameWon == false)
        {
            ShootGun();
        }
    }

    void ShootGun()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hasShot == false)
            {
                GameObject shoot = Instantiate(projectilePrefab, transform.position, transform.rotation);
                shoot.GetComponent<Rigidbody>().AddForce(transform.forward * force);
                hasShot = true;
                StartCoroutine(Cooldown());
                ThirdObjective();
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        hasShot = false;
    }

    void ThirdObjective()
    {
        if (hasShot == true)
        {
            gameManager.thirdObjective = true;
        }
    }
}
