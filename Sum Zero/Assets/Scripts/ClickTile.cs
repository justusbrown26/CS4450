using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour {

    static public GameObject frameRef;

    public Sprite tileGliph;
    public Sprite tileOpen;
    public bool isOpen = true;
    public int myId = 0;
    public int myNumber = 0;
    private bool mouseIsOver = false;

    private void Start()
    {
        FrameManager.tileArray[myId] = gameObject;
        CloseTile();
        myNumber = Random.Range(-9, 10);
    }

    private void Update()
    {
        if (mouseIsOver)
        {
            if (Input.GetMouseButtonDown(0)) // if tile is clicked
            {
                if (isOpen) // if tile is already open
                {
                    CloseTile();
                }
                else
                {
                    OpenTile();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                CloseTile();
            }
        }

    }

    void OnMouseEnter()
    {
        mouseIsOver = true;
    }

    void OnMouseExit()
    {
        mouseIsOver = false;
    }

    public void OpenTile()
    {
        GetComponent<SpriteRenderer>().sprite = tileOpen;
        GetComponentInChildren<TextMesh>().text = "" + myNumber;
        isOpen = true;
        frameRef.GetComponent<FrameManager>().PlayTile(myId, myNumber); // plays the tile, counts the sum
    }

    public void CloseTile()
    {
        GetComponent<SpriteRenderer>().sprite = tileGliph;
        GetComponentInChildren<TextMesh>().text = "";
        isOpen = false;
    }

}
