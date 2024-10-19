using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public Transform target; // Target to home in on
    public float bulletSpeed = 10f; // Speed of the bullet
    public float rotationSpeed = 200f; // Rotation speed to follow the target
    public float homingDuration = 0.3f; //Time the bullet follows

    private Rigidbody rb;
    private bool isHoming = true;
    private Vector3 lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (target == null)
        {
            Debug.LogWarning("Target not set for HomingBullet!");
            Destroy(gameObject);
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
            // Move towards the target
            lastDirection = (target.position - transform.position).normalized;
            rb.velocity = lastDirection * bulletSpeed;

            // Rotate towards the target
            Quaternion lookRotation = Quaternion.LookRotation(lastDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = lastDirection* bulletSpeed;
        }
    }

    private IEnumerator DisableHomingAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isHoming = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }
}