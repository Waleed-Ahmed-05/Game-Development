using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
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
		if (other.gameObject.tag == "Enemy")
        {
            Player.transform.position = Respawn.transform.position;
        }
        else if (other.gameObject.tag == "Scene_01")
        {
            SceneManager.LoadScene(1);
        }
        else if (other.gameObject.tag == "Scene_02")
        {
            SceneManager.LoadScene(2);
        }
        else if (other.gameObject.tag == "Finish")
        {
            Time.timeScale = 0.0f;
        }
    }
}
