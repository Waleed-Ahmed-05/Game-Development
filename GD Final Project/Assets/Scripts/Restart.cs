using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public Transform Respawn;
    [SerializeField] private Text DeathText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeathText.text = "";
            transform.position = Respawn.transform.position;
            Time.timeScale = 1.0f;
        }
    }
}
