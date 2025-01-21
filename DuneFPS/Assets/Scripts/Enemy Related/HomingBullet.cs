using System.Collections;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public Transform target; // Target to home in on
    private float bulletSpeed = 150f; // Speed of the bullet
    public float rotationSpeed = 200f; // Rotation speed to follow the target
    public float homingDuration = 0.3f; // Time the bullet follows

    private Rigidbody rb;
    private bool isHoming = true;
    private Vector3 lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // If no target is set, find the nearest object with the "Player" tag
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform;
            }
        }

        // If still no target, bullet flies straight
        if (target == null)
        {
            Debug.LogWarning("No target found for HomingBullet! Bullet will fly straight.");
            isHoming = false; // Disable homing
            lastDirection = transform.forward; // Fly in the forward direction
        }
        else
        {
            StartCoroutine(DisableHomingAfterTime(homingDuration));
        }
    }

    void FixedUpdate()
    {
        if (target != null && isHoming)
        {
            // Move towards the target manually using transform
            lastDirection = (target.position - transform.position).normalized;
            transform.position += lastDirection * bulletSpeed * Time.deltaTime;

            // Rotate towards the target
            Quaternion lookRotation = Quaternion.LookRotation(lastDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Move manually in the last direction if homing is off
            transform.position += lastDirection * bulletSpeed * Time.deltaTime;
        }
    }


    private IEnumerator DisableHomingAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isHoming = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the bullet when it hits the player
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Rock"))
        {
            // Destroy the bullet when it hits a rock
            Destroy(gameObject);
        }
        else
        {
            // Ignore collisions with other objects
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }

}
