using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float fadeDuration = 2f; // Duration of the fade-out effect in seconds
    public float deathDuration = 5f;
    public float speed = 1.5f; // Movement speed of the enemy
    public GameObject enemyBullet; // Prefab for the enemy's bullet (make sure you have this otherwise you get issues!)
    public Transform spawnPoint; 
    public float bulletSpeed = 8f; // Speed of the bullets
    public float shootInterval = 3f; // Time between shots
    public float detectionRange = 12f; // Distance at which the enemy starts moving
    public float stoppingDistance = 4f; // Distance at which the enemy stops moving

    private Renderer enemyRenderer;
    private Color originalColor;
    private float shootTimer;
    private int hitCount = 0; // Tracks how many times the enemy has been hit
    private GameObject player;
    private bool isRunningAway = false; // Flag to indicate if the enemy is running away

    private Animator animator;
    private bool stopped = true;
    private bool speeding = false;
    private bool slowing = false;
    private float walkSpeed = 0f;
    private bool canShoot = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        enemyRenderer = GetComponent<Renderer>();

        animator.SetBool("Aiming", true);

        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }

        shootTimer = shootInterval;
    }

    void Update()
    {
        animator.SetFloat("Y", walkSpeed);
        if (isRunningAway || player == null)
        {
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Move towards the player only if within detection range and beyond stopping distance
        if (distanceToPlayer <= detectionRange && distanceToPlayer > stoppingDistance)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (stopped && !speeding) 
            {
                StartCoroutine(WalkTransition());
            }
        }
        else 
        {
            if (!stopped && !slowing) 
            {
                StartCoroutine(WalkTransition());
            }
        }

        // Shooting mechanic: Shoot only if the enemy is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0 && canShoot)
            {
                shootTimer = shootInterval;

                // Instantiate and shoot bullet
                GameObject bullet = Instantiate(enemyBullet, spawnPoint.position, Quaternion.identity);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                if (bulletRb != null)
                {
                    Vector3 direction = (player.transform.position - spawnPoint.position).normalized;
                    bulletRb.isKinematic = false;
                    bulletRb.velocity = direction * bulletSpeed;
                }

                Destroy(bullet, 3f); // Destroy bullet after 3 seconds
            }
        }
        //Faces player if within range
        if (distanceToPlayer <= detectionRange) 
        {
            Vector3 moveDir = player.transform.position - transform.position;
            moveDir.y = 0;
            moveDir.Normalize();
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg, 0);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerBullet"))
        {
            Destroy(other.gameObject); // Destroy the player's bullet
            hitCount++;

            if (hitCount == 1)
            {
                StartCoroutine(RunAway());
            }
            else if (hitCount > 1)
            {
                StartCoroutine(DieAndDestroy());
            }
        }
    }

    private IEnumerator RunAway()
    {
        StartCoroutine(Spin());
        //isRunningAway = true; // Disable other behaviors
        float runAwayTime = 2f; // Time spent running away
        float elapsedTime = 0f;

        while (elapsedTime < runAwayTime)
        {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            if (stopped && !speeding)
            {
                StartCoroutine(WalkTransition());
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(Spin());
        //isRunningAway = false; // Re-enable other behaviors
    }

    private IEnumerator FadeAndDestroy()
    {
        if (enemyRenderer == null)
        {
            Destroy(gameObject);
            yield break;
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            enemyRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator DieAndDestroy()
    {
        isRunningAway = true;
        canShoot = false;
        float elapsedTime = 0f;
        animator.SetTrigger("Dead");
        while (elapsedTime < deathDuration)
        {
            elapsedTime += Time.deltaTime;
            //float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            //enemyRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }

    //Turns them 180 deg then sets isRunningAway to the opposite
    IEnumerator Spin() 
    {
        isRunningAway = !isRunningAway;
        float startAngle = transform.eulerAngles.y;
        float approxAngle = startAngle;
        while (approxAngle < startAngle + 180)
        {
            approxAngle += 1;
            transform.eulerAngles = new Vector3(0,approxAngle, 0);
            yield return null;
        }
    }
    IEnumerator WalkTransition() 
    {
        if (stopped)
        {
            speeding = true;
            slowing = false;
            while (walkSpeed < 1 && speeding == true) 
            {
                walkSpeed += 0.01f;
                yield return null;
            }
            stopped = false;
        }
        else 
        {
            slowing = true;
            speeding = false;
            while (walkSpeed > 0 && slowing == true)
            {
                walkSpeed -= 0.01f;
                yield return null;
            }
            stopped = true;
        }
        
    }
}
