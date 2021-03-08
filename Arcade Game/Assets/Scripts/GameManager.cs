using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI livesText;
    public GameObject gameoverText;

    public GameObject player;

    private int score;
    private int wave;
    public int lives;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE:" + score;
        waveText.text = "WAVE:" + wave;
        livesText.text = "LIVES:" + lives;

        if (lives <= 0)
        {
            Destroy(player);
            gameoverText.gameObject.SetActive(true);
        }
    }

    public void AddScore()
    {
        score += 10;
    }

    public void AddWave()
    {
        wave += 1;
    }
    public void LoseLives()
    {
        lives -= 1;
    }
}
