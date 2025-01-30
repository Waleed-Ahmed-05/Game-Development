using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Collision : MonoBehaviour
{
	[SerializeField] private Transform Player;
	[SerializeField] private Transform Respawn;
	public int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Score")
		{
			Score += 1;
			Debug.Log("Your score is: " + Score);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("Try Again. Your score was: " + Score);
			Score = 0;
			Player.transform.position = Respawn.transform.position;
		}
		else if (other.gameObject.tag == "Finish")
		{
			Time.timeScale = 0.0f;
			Debug.Log("Game over. Your score was: " + Score);
		}
	}
}
