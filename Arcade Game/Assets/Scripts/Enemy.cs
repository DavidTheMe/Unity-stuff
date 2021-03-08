using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float distanceFromPlayer;
    private float stateCountdown;
    private int wayToStrafe;
    private PlayerMovement playerMovementScript;
    private GameObject player;
    private float shootCooldownCountdown;
    private Collider other;

    //state 0 = follow
    //state 1 = shoot
    private int state = 0;

    public float rotateSpeed;
    public float forwardSpeed;
    public float strafeSpeed;
    public float distanceToStartAttacking;
    public float timeInAttackState;
    public float shootCooldown;
    public GameObject bullet;
    public GameObject pickup;
    public int hp;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        playerMovementScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get distance to player
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        //Face player
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);



        //Keep inside borders
        Vector3 pos = transform.position;

        if (pos.z > playerMovementScript.verticalBorder)
            pos.z -= playerMovementScript.pushForce;
        if (pos.z < -playerMovementScript.verticalBorder)
            pos.z += playerMovementScript.pushForce;

        if (pos.x > playerMovementScript.horizontalBorder)
            pos.x -= playerMovementScript.pushForce;
        if (pos.x < -playerMovementScript.horizontalBorder)
            pos.x += playerMovementScript.pushForce;

        transform.position = pos;

        //Follow state
        if (state == 0)
        {
            //Move forward
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

            //Go to shooting state
            if (distanceFromPlayer < distanceToStartAttacking)
            {
                state = 1;
                stateCountdown = timeInAttackState;

                wayToStrafe = Random.Range(0, 2) > 0 ? -1 : 1;
            }
        }

        //Shoot state
        if (state == 1)
        {
            //Strafe
            transform.Translate(Vector3.right * strafeSpeed * wayToStrafe * Time.deltaTime);

            //Countdown
            stateCountdown -= Time.deltaTime;
            shootCooldownCountdown -= Time.deltaTime;

            //Shoot
            if (shootCooldownCountdown <= 0)
            {
                shootCooldownCountdown = shootCooldown;
                Instantiate(bullet, transform.position, transform.rotation);
            }

            //Change state to follow
            if (stateCountdown <= 0)
            {
                state = 0;
            }
        }

        //If out of hp
        if (hp <= 1)
        {
            Destroy(gameObject);

            for (int i = 0; i < 5; i++)
            {
                Instantiate(pickup, transform.position, transform.rotation);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        other = collision.collider;

        //If hit by player
        if (other.tag == "Player Bullet")
        {
            hp -= 1;
        }
    }
}
