using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] private Transform Enemy;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Respawn;
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
		if (other.gameObject.tag == "Bullet")
        {
            Enemy.transform.position = Respawn.transform.position;
        }
        else if (other.gameObject.tag == "Player")
        {
            Player.transform.position = Respawn.transform.position;
        }
        else if (other.gameObject.tag == "Finish")
        {
            Time.timeScale = 0.0f;
        }
    }
}
