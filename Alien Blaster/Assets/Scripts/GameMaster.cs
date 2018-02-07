using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;

    public int targetMax = 10;
    public GameObject friendPrefab;
    public GameObject foePrefab;
    public GameObject enemyShipPrefab;
    public int winningScore;
    public Animator GameWonExplosion;

    private GameObject target;

    private int targets = 0;
    private int score = 0;
    private System.Random rand;
    private bool gameWon = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<GameMaster>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rand = new System.Random();
        UIManager.instance.toWin.text = "Score " + winningScore + " points to destroy the enemy.";
    }


    // Update is called once per frame
    void Update() {
        if (!gameWon)
        {
            if (targets < targetMax)
            {
                if (rand.Next(3) == 0)
                {
                    target = Instantiate(friendPrefab);
                    target.transform.parent = GameObject.Find("Targets").transform;
                    target.GetComponent<TargetController>().Respawn();
                    targets++;
                }
                else
                {
                    target = Instantiate(foePrefab);
                    target.transform.parent = GameObject.Find("Targets").transform;
                    target.GetComponent<TargetController>().Respawn();
                    targets++;
                }
            }
        }
    }

    public void AddScore(int num)
    {
        if (!gameWon)
        {
            score += num;
            if (score >= winningScore)
            {
                gameWon = true;
                SendEnemyShip();
            }
        }
    }

    public void SubtractScore(int num)
    {
        if (!gameWon)
        {
            score -= num;
            if (score < 0)
            {
                StartCoroutine(UIManager.instance.GameLost());
            }
        }
    }

    public int Score
    {
        get { return score; }
    }

    public int Targets
    {
        get { return targets; }
        set
        {
            targets = value;
        }
    }

    private void SendEnemyShip()
    {

        GameObject tmp = Instantiate(enemyShipPrefab);
        tmp.transform.parent = GameObject.Find("Targets").transform;
        tmp.GetComponent<TargetController>().Respawn();
        UIManager.instance.ToWin();



    }

    public void GameWon()
    {
        StartCoroutine(UIManager.instance.GameWon());
    }
}
