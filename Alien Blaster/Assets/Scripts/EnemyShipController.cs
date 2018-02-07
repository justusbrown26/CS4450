using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : TargetController {

    private void Update()
    {
        AdjustPosition();
    }

    void AdjustPosition()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(transform.position);

        if (screenPos.y < 0)
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);

        // horizontal adjustment
        if (screenPos.x > Screen.width)
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        else if (screenPos.x < 0)
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.down*5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            GameMaster.instance.GameWonExplosion.SetBool("Explosion", true);
            GameMaster.instance.GameWon();
            gameObject.SetActive(false);
        }
    }
}
