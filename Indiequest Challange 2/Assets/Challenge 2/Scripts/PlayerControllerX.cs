using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private int cooldown = 0;
    public GameObject dogPrefab;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && cooldown < 1)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            cooldown = 300;
        }
        cooldown -= 1;
    }
}
