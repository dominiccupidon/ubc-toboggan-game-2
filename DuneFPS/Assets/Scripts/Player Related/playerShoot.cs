using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerShoot : MonoBehaviour
{

    public GameObject playerBullet;
    public ParticleSystem flash;
    public GameObject impactEffect;
    public Transform bulletSpawn;
    public float range = 50f;
    public bool semiAutomatic = false;
    public float cooldownTime = 2.0f;
    public int maxShots = 12;

    private int currentShots = 0;
    private float bulletTime;
    private bool isInCooldown = false;

    private TextMeshProUGUI tmp;
    GameObject cooldown;

    // Start is called before the first frame update
    void Start()
    {
        maxShots = semiAutomatic ? 12 : 40;
        tmp = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<TextMeshProUGUI>();
        tmp.text = getRemainingShots() + "/" + maxShots;
        cooldown = tmp.transform.GetChild(0).gameObject;
        cooldown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCooldown) return;

        if (currentShots > maxShots) 
        {
            StartCoroutine(Cooldown());
            return;
        }

        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        if (semiAutomatic)
        {
            shootEnemy();
        } else
        {
            automatic();
        }
    }

    void shootEnemy()
    {
        if (Input.GetMouseButtonDown(0) && bulletTime <= 0)
        {
            bulletTime = 2f;
            FireBullet(25f);
        }

    }

    void automatic()
    {
        if (Input.GetMouseButton(0)) // Hold down left mouse button
        {
            bulletTime = 0.1f; // Set timer to 0.1 seconds (10 bullets per second)
            FireBullet(30f);
        }
    }

    void FireBullet(float force)
    {
        flash.Play();
        Vector3 shootDirection = GetShootDirection();

        // Instantiate the bullet and set its target
        // GameObject bulletObj = Instantiate(playerBullet, bulletSpawn.position, Quaternion.identity) as GameObject;
        // Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        // bulletRig.useGravity = false;
        // bulletObj.SetActive(true);
        // bulletRig.AddForce(shootDirection * force, ForceMode.VelocityChange);

        // // I think we should only have the bullet dissappear automatically if it won't hit an enemy
        // Destroy(bulletObj, 3.5f);

        RaycastHit collision;
        bool didCollide = Physics.Raycast(Camera.main.transform.position, shootDirection, out collision, range);
        if (didCollide) {

            if (collision.transform.CompareTag("Enemy")) 
            {
                //Debug.Log(collision.transform.name);
                collision.transform.gameObject.GetComponent<EnemyBehavior>().DamageEnemy();
            }
            GameObject impactInst = Instantiate(impactEffect, collision.point, Quaternion.LookRotation(collision.normal));
            Destroy(impactInst, 1f);
            tmp.text = getRemainingShots() + "/" + maxShots;

        }

        currentShots++;
    }

    IEnumerator Cooldown()
    {
        isInCooldown = true;
        //Debug.Log("Entering cooldown");
        cooldown.SetActive(true);
        yield return new WaitForSeconds(cooldownTime); // waits for time specified in game

        currentShots = 0;
        isInCooldown = false;
        tmp.text = getRemainingShots() + "/" + maxShots;
        cooldown.SetActive(false);
    }

    Vector3 GetShootDirection()
    {
        // TODO: Determine how to have the gun point in the same direction as the camera
        return Camera.main.transform.forward;
    }

    public int getRemainingShots() 
    {
        return maxShots - currentShots;
    }
}
