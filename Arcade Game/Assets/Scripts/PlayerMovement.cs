using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float verticalBorder;
    public float horizontalBorder;
    public float pushForce = 1;
    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turret4;
    public GameObject bullet;
    public float shootCooldown;
    public float shootCooldownAmount;
    public AudioClip shoot;

    private GameObject gameHudObject;
    private GameManager gameManager;

    private Collider other;


    // Update is called once per frame
    void Update()
    {
        //Get input
        float horizontalInput = Input.GetAxis("LeftJoyHorizontal");
        float verticalInput = Input.GetAxis("LeftJoyVertical");

        float horizontalInput2 = Input.GetAxis("RightJoyHorizontal");
        float verticalInput2 = -Input.GetAxis("RightJoyVertical");

        //Move
        Vector3 pos = transform.position;

        pos.x += horizontalInput * movementSpeed * Time.deltaTime;
        pos.z += -verticalInput * movementSpeed * Time.deltaTime;

        //Rotation
        float heading;

        if (horizontalInput2 == 0 && verticalInput2 == 0)
        {
            heading = Mathf.Atan2(horizontalInput, -verticalInput);
            transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);
        }
        else
        {
            heading = Mathf.Atan2(horizontalInput2, verticalInput2);
            transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);

            if (shootCooldown <= 0)
            {
                Instantiate(bullet, turret1.transform.position, transform.rotation);
                Instantiate(bullet, turret2.transform.position, transform.rotation);
                Instantiate(bullet, turret3.transform.position, transform.rotation);
                Instantiate(bullet, turret4.transform.position, transform.rotation);
                shootCooldown = shootCooldownAmount;
                AudioSource.PlayClipAtPoint(shoot, transform.position, 1.0F);
            }
        }

        //Keep inside borders

        if (pos.z > verticalBorder)
            pos.z -= pushForce;
        if (pos.z < -verticalBorder)
            pos.z += pushForce;
        
        if (pos.x > horizontalBorder)
            pos.x -= pushForce;
        if (pos.x < -horizontalBorder)
            pos.x += pushForce;


        //Apply movement
        transform.position = pos;

        //Count down cooldowns
        shootCooldown -= Time.deltaTime * 60;
    }
}
