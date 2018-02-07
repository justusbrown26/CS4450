using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollControl : MonoBehaviour {

    public float speed = -1f;
    float height;
    public Transform top, bottom;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0f, speed, 0f);
        height = (top.position.y - bottom.position.y) / 2;
    }

    // Update is called once per frame
    void Update () {
        if (speed > 0)
        {
            // moving up
            // if we get past the height of one image, move both of them down to restart the cycle.
            if (transform.position.y >= height)
                transform.position = new Vector3(transform.position.x, -height, transform.position.z);

        }
        else
        {
            // moving down
            // if we get past minus the height of one image, move both of them up to restart the cycle.
            if (transform.position.y <= -height)
                transform.position = new Vector3(transform.position.x, height, transform.position.z);

        }
    }
}
