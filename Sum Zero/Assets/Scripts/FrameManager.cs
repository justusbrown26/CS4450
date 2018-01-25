using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : MonoBehaviour {

	static public GameObject[] tileArray = new GameObject[8];
    public GameObject winMessage;
	private bool gameStarted = false;
	private int countOpen, sum;
	private int[] chosenTile = new int[] { -1, -1, -1 };
	private int timeLeftToClose = 100;
	private const int CLOSE_TIME = 100;

	// Use this for initialization
	void Start () {
		ClickTile.frameRef = gameObject; // gives the ClickTile class a reference to the frame
		countOpen = 0;
		sum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted)
		{
			gameStarted = true;
			MakeSolution();
		}
		//else if (chosenTile[0] != -1)
		//{
			if(countOpen == 3)
			{
				if (timeLeftToClose == 0)
				{
					CloseOpenTiles();
				}
				else
				{
					timeLeftToClose--;
				}
			}
		//}
	}

	// Makes sure that the game has at least one solution

	void MakeSolution()
	{

		List<int> randomTiles = new List<int>();
		int num = 0;
		while (randomTiles.Count < 3)
		{
			num = Random.Range(0, 8);
			if (!randomTiles.Contains(num))
			{
				randomTiles.Add(num);
			}
		}

		int num1 = tileArray[randomTiles[0]].GetComponent<ClickTile>().myNumber;
		int num2 = tileArray[randomTiles[1]].GetComponent<ClickTile>().myNumber;

		// Make sure we don't end up with a number less than -9
		if (num1 + num2 > 9)
		{
			num1 -= num1 + num2 - 9;
			tileArray[randomTiles[0]].GetComponent<ClickTile>().myNumber = num1;
		}

		// Make sure we don't end up with a number larger than 9
		if (num1 + num2 < -9)
		{
			num1 -= num1 + num2 + 9;
			tileArray[randomTiles[0]].GetComponent<ClickTile>().myNumber = num1;
		}

		tileArray[randomTiles[2]].GetComponent<ClickTile>().myNumber = -(num1 + num2);

		Debug.Log("tile1 = " + randomTiles[0] + " tile2 = " + randomTiles[1] + " tile3 = " + randomTiles[2]);
	}

	// when a tile is clicked on
	public void PlayTile(int id, int number)
	{
		if(countOpen == 3)
		{
			FrameManager.tileArray[id].GetComponent<ClickTile>().CloseTile();
			return;
		}
		chosenTile[countOpen] = id;
		timeLeftToClose = CLOSE_TIME;
		sum += number;
		countOpen++;
		if(countOpen == 3)
		{
			if(sum == 0)
			{
                OpenAllTiles();
				StartCoroutine(GameWon());
			}
		}
		Debug.Log("Clicked tile: " + id + " with number: " + number);
	}

	public void CloseOpenTiles()
	{
		for (int i = 0; i < countOpen; i++)
		{
			foreach (var tile in chosenTile)
			{
				tileArray[tile].GetComponent<ClickTile>().CloseTile();
			}
			sum = 0;
			countOpen = 0;
			timeLeftToClose = CLOSE_TIME;
		}
	}

    private void OpenAllTiles()
    {
        foreach (var tile in tileArray)
        {
            tile.GetComponent<SpriteRenderer>().sprite = tile.GetComponent<ClickTile>().tileOpen;
            tile.GetComponentInChildren<TextMesh>().text = "" + tile.GetComponent<ClickTile>().myNumber;
        }
    }

	private IEnumerator GameWon()
	{

        yield return new WaitForSeconds(1.5f);
        foreach (var tile in tileArray)
		{
			tile.GetComponent<ClickTile>().gameObject.SetActive(false);
		}
		gameObject.SetActive(false);
        winMessage.SetActive(true);
		
	}
}
