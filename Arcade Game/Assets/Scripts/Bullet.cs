using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private Collider other;
    private GameObject gameHudObject;
    private GameManager gameManager;

    void Awake()
    {
        gameHudObject = GameObject.Find("Game Hud");
        gameManager = gameHudObject.GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        //Go forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //Delete
        Vector3 pos = transform.position;

        if (pos.z > 10 || pos.z < -10 || pos.x > 18 || pos.x < -18)
            Destroy(gameObject);
    }

    //On collision
    void OnCollisionEnter(Collision collision)
    {
        other = collision.collider;

        //Enemy bullet hits player
        if (other.name == "Player" && tag == "Enemy Bullet")
        {
            gameManager.LoseLives();
            Destroy(gameObject);
        }

        //Player bullet hits enemy
        if (other.tag == "Enemy" && tag == "Player Bullet")
        {
            Destroy(gameObject);
        }
    }
}
