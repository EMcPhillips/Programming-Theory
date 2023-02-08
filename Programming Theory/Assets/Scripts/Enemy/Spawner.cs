using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool hasSpawned;
    public GameObject boss;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        hasSpawned = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hasSpawned == false)
        {
            SpawnBoss();
            FifthObjective();
        }
    }

    public void SpawnBoss()
    {
        Instantiate(boss, transform.position, transform.rotation);
        hasSpawned = true;
        Debug.Log("Boss spawned");
    }

    public void FifthObjective()
    {
        gameManager.fifthObjective = true;
    }
}
