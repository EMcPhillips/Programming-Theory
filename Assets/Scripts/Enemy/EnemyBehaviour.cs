using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public int enemyHealth;
    public float moveSpeed = 6.0f;
    public float maxAttackDist = 50.0f;
    private float returnSpeed = 5.0f;
    public bool hasBeenHit = false;
    public bool targetPlayer = false;
    public Transform target;

    public ParticleSystem deathParticle;

    public AudioClip deathAudio;
    public AudioClip hitAudio;
    public AudioClip hitPlayerAudio;
    public AudioSource audioSource;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    public GameManager gameManager;
    public MainManager mainManager;
    
    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        audioSource = GetComponent<AudioSource>();

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        enemyHealth = mainManager.enemyHealth;

        GotoNextPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AttackRange(); //ABSTRACTION
    }

   public virtual void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            enemyHealth -= 25;
            hasBeenHit = false;
            targetPlayer = true;
            audioSource.PlayOneShot(hitAudio, 1.0f);
            DeathSequence();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            gameManager.playerHealth -= 10;
            hasBeenHit = true;
            targetPlayer = true;
            audioSource.PlayOneShot(hitPlayerAudio, 1.0f);
            StartCoroutine(HitCooldown());
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
        hasBeenHit = false;
        targetPlayer = false;
    }

    public virtual void AttackRange()
    {    
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < maxAttackDist || targetPlayer == true)
        {
            AttackSequence();
        }
        else
        {
            ReturnToPatrol();
        }
    }

    public virtual void DeathSequence()
    {
        if (enemyHealth <= 0)
        {
            hasBeenHit = true;
            targetPlayer = true;
            audioSource.PlayOneShot(deathAudio, 0.75f);
            StartCoroutine(DestroyCountdown());
            FourthObjective();
            gameManager.killCount++;
        }
    }

    IEnumerator DestroyCountdown()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(deathParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void AttackSequence()
    {
        if (hasBeenHit == false)
        {
            transform.LookAt(target);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (targetPlayer == true)
        {
            transform.position -= transform.forward * returnSpeed * Time.deltaTime;
        }
    }

    public void ReturnToPatrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 1.0f)
        {
            GotoNextPoint();
        }
        targetPlayer = false;
    }

    public void FourthObjective()
    {
        gameManager.fourthObjective = true;
    }
}
