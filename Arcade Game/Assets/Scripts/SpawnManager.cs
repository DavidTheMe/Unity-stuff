using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    private GameObject gameHudObject;
    private GameManager gameManager;

    private int wave = 1;
    private float spawnCountdown;
    private bool waveOn;
    public int amountOfEnemiesToSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        gameHudObject = GameObject.Find("Game Hud");
        gameManager = gameHudObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int amountOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (amountOfEnemies < 1 && waveOn == false)
        {
            gameManager.AddWave();
            wave += 1;
            waveOn = true;
            amountOfEnemiesToSpawn = 2 + wave;
        }

        if (waveOn == true)
        {
            if (spawnCountdown < 1)
            {
                if (amountOfEnemiesToSpawn > 0)
                {
                    SpawnEnemies();
                    amountOfEnemiesToSpawn -= 1;
                    spawnCountdown = 9 - wave * 1;
                }
                else
                {
                    waveOn = false;
                }
            }
            spawnCountdown -= Time.deltaTime;
        }
    }

    void SpawnEnemies()
    {
        Instantiate(enemy, new Vector3(-17, 0, Random.Range(-10, 10)), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        Instantiate(enemy, new Vector3(17, 0, Random.Range(-10, 10)), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }
}
