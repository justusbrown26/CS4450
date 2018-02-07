using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public Text score;
    public Text outcome;
    public Text toWin;

    private void Awake()
    {
        if(instance == null)
        {
            instance = GetComponent<UIManager>();
        }
        else
        {
            Destroy(instance);
        }

    }

    private void Update()
    {
        score.text = "Score: " + GameMaster.instance.Score;
    }

    public IEnumerator GameLost()
    {
        outcome.text = "Game Lost";
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }

    public void ToWin()
    {
        toWin.text = "Crash the enemy ship to win the game.";
    }

    public IEnumerator GameWon()
    {
        outcome.text = "Game Won!!";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }

}
