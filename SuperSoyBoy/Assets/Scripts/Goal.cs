using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public AudioClip goalClip;
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			// 1
			var timer = FindObjectOfType<Timer>();
			// 2
			GameManager.instance.SaveTime(timer.time);
			var audioSource = GetComponent<AudioSource>();
			if (audioSource != null && goalClip != null)
			{
				audioSource.PlayOneShot(goalClip);
			}
			GameManager.instance.RestartLevel(0.5f);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
