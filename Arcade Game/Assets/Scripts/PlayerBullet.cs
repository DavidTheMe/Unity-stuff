using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Choose direction
        float horizontalInput2 = Input.GetAxis("RightJoyHorizontal");
        float verticalInput2 = -Input.GetAxis("RightJoyVertical");

        float heading = Mathf.Atan2(horizontalInput2, verticalInput2);
        transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);

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
}
