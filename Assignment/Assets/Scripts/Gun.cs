using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform Bullet;
    public GameObject BulletPrefab;
    public float Bullet_Speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            var bullet = Instantiate(BulletPrefab, Bullet.position, Bullet.rotation);
            bullet.GetComponent<Rigidbody>().velocity = Bullet.forward * Bullet_Speed;
        }
    }
}
