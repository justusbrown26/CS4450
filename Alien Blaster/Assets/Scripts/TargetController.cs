using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{



    protected static Camera mainCam = null;
    public bool friend = false;
    public int scoreValue;
    //public AudioClip sound;
    //public AudioSource audioSource;
    protected Rigidbody2D rb2d;

    private bool firstHit = false;



    // Use this for initialization
    void Awake()
    {
        // Find the camera from the object tagged as Player.
        if (!mainCam)
            mainCam = GameObject.FindWithTag("Player").GetComponent<PlayerController>().mainCam;
        //audioSource = GetComponent<AudioSource>();

    }

    public void Respawn()
    {
        // Randomize the initial position based on the screen size above the top of the screen
        float x = Random.Range(10, Screen.width - 9);
        float y = Screen.height + Random.Range(20, 400);

        // then covert it to world coordinates and assign it to the critter.
        Vector3 pos = mainCam.ScreenToWorldPoint(new Vector3(x, y, 0f));
        pos.z = transform.position.z;

        transform.position = pos;
        if (!friend)
        {
            Vector2 originalScale = transform.localScale;
            Vector2 randomSize = originalScale + new Vector2(Random.Range(.1f, .75f), Random.Range(.1f, .75f));

            transform.localScale = randomSize;
        }
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.zero;


    }

    // collision detection function
    void OnTriggerEnter2D(Collider2D colInfo)
    {
        if (colInfo.CompareTag("Player"))
        {
            if(!firstHit)
            {
                firstHit = true;
                UIManager.instance.toWin.text = "";
            }
            if (friend)
            {
                GameMaster.instance.AddScore(scoreValue);
                Debug.Log("friend " + GameMaster.instance.Score);
            }
            else
            {
                GameMaster.instance.SubtractScore(scoreValue);
                Debug.Log("enemy " + GameMaster.instance.Score);

            }
            Destroy(gameObject);
            GameMaster.instance.Targets--;
        }
        else if (colInfo.name.Equals("Ground"))
        {
            Destroy(gameObject);
            GameMaster.instance.Targets--;
        }
    }
}
