using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyBehaviour
{
    public Transform spawnPoint;
    public GameObject bossProjectile;
    public ParticleSystem deathExplosionParticle;
    public ParticleSystem spawnParticle;
    public AudioClip bossSpawnAudio;
    public float returnCooldown;
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        spawnPoint = GameObject.Find("BossSpawner").GetComponent<Transform>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(bossSpawnAudio, 1.0f);
        Instantiate(spawnParticle, transform.position, transform.rotation);
        hasBeenHit = false;
        targetPlayer = false;
        enemyHealth = mainManager.enemyHealth * 8;
    }

    // Update is called once per frame
    void Update()
    {
        AttackRange(); //ABSTRACTION
        DeathSequence(); //ABSTRACTION
    }

    public override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.playerHealth -= 20;
            hasBeenHit = true;
            audioSource.PlayOneShot(hitPlayerAudio, 1.0f);
        }
        if (other.gameObject.CompareTag("Spawn"))
        {
            StartCoroutine(BossReturnCooldown());
            StartCoroutine(ProjectileAttackCooldown());
        }
        if (other.gameObject.CompareTag("Projectile"))
        {
            enemyHealth -= 25;
            audioSource.PlayOneShot(hitAudio, 1.0f);
        }
    }

    public override void AttackRange() //POLYMORPHISM
    {
        //base.AttackRange();
        float distance = Vector3.Distance(transform.position, target.position);
        if (hasBeenHit == false)
        {
            AttackSequence(); //INHERITANCE
        }
        else
        {
            ReturnToSpawn();
        }
    }


    IEnumerator BossReturnCooldown()
    {
        yield return new WaitForSeconds(returnCooldown);
        hasBeenHit = false;
    }
    
    IEnumerator ProjectileAttackCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        ProjectileAttack();
    }

    private void ProjectileAttack()
    {
        if (hasBeenHit == true)
        {
            Instantiate(bossProjectile, transform.position + new Vector3(0, 5, 0), transform.rotation);
            StartCoroutine(ProjectileAttackCooldown());
        }
    }

    public override void DeathSequence()
    {
        if (enemyHealth <= 0)
        {
            audioSource.PlayOneShot(deathAudio, 1.0f);
            Instantiate(deathExplosionParticle, transform.position, transform.rotation);
            Instantiate(deathParticle, transform.position, transform.rotation);
            Destroy(gameObject);
            gameManager.gameWon = true;
        }
    }

    private void ReturnToSpawn()
    {
        transform.LookAt(spawnPoint);
        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, spawnPoint.position, step);
    }

}
