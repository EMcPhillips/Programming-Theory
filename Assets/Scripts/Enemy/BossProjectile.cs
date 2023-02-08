using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Transform target;
    public float moveSpeed;
    public float moveDelay;
    public float launchForce;
    public bool hasLaunched;
    public AudioSource audioSource;
    public AudioClip launchAudio;
    public AudioClip explosionAudio;
    public ParticleSystem hitParticle;
    private Rigidbody bossProjectileRb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        bossProjectileRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
        hasLaunched = false;
        LaunchSequence();
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.playerHealth -= 5;
            audioSource.PlayOneShot(explosionAudio, 0.5f);
            Instantiate(hitParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Terrain") || other.gameObject.CompareTag("Projectile"))
        {
            audioSource.PlayOneShot(explosionAudio, 0.5f);
            Instantiate(hitParticle, transform.position, transform.rotation);
            if (other.gameObject.CompareTag("Projectile"))
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
    private void LaunchSequence()
    {
        bossProjectileRb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        StartCoroutine(DelayMove());
        audioSource.PlayOneShot(launchAudio, 1.0f);
        Debug.Log("Projectile Launched");
    }

    IEnumerator DelayMove()
    {
        yield return new WaitForSeconds(moveDelay);
        hasLaunched = true;
    }

    private void MoveTowardsPlayer()
    {
        if(hasLaunched == true)
        {
            transform.LookAt(target);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            Debug.Log("Targeting player");        
        }
    }
}
