using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float spreadSpeed;
    public float pickupSpeed;
    public float pickupRange;
    public float spreadAmount;
    public AudioClip pickup;
    
    private Vector3 direction;
    private Collider other;
    private float spreadCountdown;
    private GameObject player;

    private GameObject gameHudObject;
    private GameManager gameManager;



    // Start is called before the first frame update
    void Awake()
    {
        gameHudObject = GameObject.Find("Game Hud");
        gameManager = gameHudObject.GetComponent<GameManager>();

        //Get direction
        float x = Random.Range(0, 10);
        float z = Random.Range(0, 10);
        direction = new Vector3(x, 0, z);

        //Set countdown
        spreadCountdown = Random.Range(spreadAmount, spreadAmount * 2);

        //Find player
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (spreadCountdown >= 0)
        {
            transform.Translate(direction * spreadSpeed * (spreadCountdown * 0.01f) * Time.deltaTime);
            spreadCountdown -= Time.deltaTime * 300;
        }

        //Get distance to player
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        //Go towards player
        if (distanceFromPlayer < pickupRange)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, pickupSpeed * Time.deltaTime);

    }

    void OnCollisionEnter(Collision collision)
    {
        other = collision.collider;

        //Player pickup
        if (other.name == "Player")
        {
            gameManager.AddScore();
            AudioSource.PlayClipAtPoint(pickup, transform.position, 1.0F);
            Destroy(gameObject);
        }
    }
}
