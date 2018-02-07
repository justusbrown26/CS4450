using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public KeyCode moveUp, moveDown, moveLeft, moveRight;
    public float speedX = 0, speedY = 0;
    public bool linearMovement = true;
    public Camera mainCam;

    private Rigidbody2D rbody;

    public Animator thrusterAnim;

    void Start()
    {
        // store the rigid body in an attribute for easier access.
        rbody = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKey(moveUp))
        {
            if (linearMovement)
                rbody.velocity = new Vector2(0f, speedY);


            thrusterAnim.SetBool("thrust", true);

        }
        else if (Input.GetKey(moveDown))

        {
            if (linearMovement)
                rbody.velocity = new Vector2(0f, -speedY);
        }
        else if (Input.GetKey(moveLeft))
        {

            if (linearMovement)
                rbody.velocity = new Vector2(-speedX, 0f);
        }
        else if (Input.GetKey(moveRight))
        {
            if (linearMovement)
                rbody.velocity = new Vector2(speedX, 0f);
        }
        else
        {
            // no input, reset the speed
            rbody.velocity = new Vector2(0f, 0f);
            thrusterAnim.SetBool("thrust", false);

        }
        AdjustPosition();
    }

    void AdjustPosition()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(transform.position);
        Vector3 topScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Vector3 bottomScreen = mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));

        // vertical adjustment
        if (screenPos.y > Screen.height)
            transform.position = new Vector3(transform.position.x, topScreen.y, transform.position.z);
        else if (screenPos.y < 0)
            transform.position = new Vector3(transform.position.x, bottomScreen.y, transform.position.z);

        // horizontal adjustment
        if (screenPos.x > Screen.width)
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        else if (screenPos.x < 0)
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }

}

