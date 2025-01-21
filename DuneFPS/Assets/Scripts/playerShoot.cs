using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    private GameObject enemy;

    public GameObject playerBullet;
    public Transform bulletSpawn;
    private float bulletTime;
    public float timer = 5.0f;
    private Collider playerCollider;
    public bool semiAutomatic = true;
    public int maxShots = 12;
    private int currentShots = 0;
    private bool isReloading = false;
    public float reloadTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            Debug.LogWarning("Enemy not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;

        if ( Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (currentShots >= maxShots) return;

        if (semiAutomatic)
        {
            shootEnemy();
        }
        if (!semiAutomatic)
        {
            automatic();
        }
    }

    void shootEnemy()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        if (Input.GetMouseButtonDown(0) && bulletTime <= 0)
        {
            bulletTime = timer;

            Vector3 shootDirection = GetShootDirection();

            // Instantiate the bullet and set its target
            GameObject bulletObj = Instantiate(playerBullet, bulletSpawn.position, Quaternion.identity) as GameObject;
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

            bulletRig.AddForce(shootDirection * 75f, ForceMode.VelocityChange);
            bulletRig.useGravity = false;

            Collider bulletCollider = bulletRig.GetComponent<Collider>();

            Destroy(bulletObj, 3.5f);

            currentShots++;
        }

    }
    void automatic()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        if (Input.GetMouseButton(0)) // Hold down left mouse button
        {
            bulletTime = 0.1f; // Set timer to 0.1 seconds (10 bullets per second)

            Vector3 shootDirection = GetShootDirection();

            // Instantiate the bullet and set its target
            GameObject bulletObj = Instantiate(playerBullet, bulletSpawn.position, Quaternion.identity) as GameObject;
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

            bulletRig.AddForce(shootDirection * 30f, ForceMode.VelocityChange);
            bulletRig.useGravity = false;

            Collider bulletCollider = bulletRig.GetComponent<Collider>();

            Destroy(bulletObj, 3.5f);

            currentShots++;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime); // waits for time specified in game

        currentShots = 0;
        isReloading = false;
    }

    Vector3 GetShootDirection()
    {
        return Camera.main.transform.forward;
    }
}