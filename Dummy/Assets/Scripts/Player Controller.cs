using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb;
    public bool Player_On_Ground = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 velocity = rb.velocity;
        velocity.x = xMove * speed;
        velocity.z = zMove * speed;
        rb.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space) && Player_On_Ground)
        {
            rb.AddForce(new Vector3(0, 8, 0), ForceMode.Impulse);
            Player_On_Ground = false;
        }
    }

    // Use both OnCollisionEnter and OnCollisionStay to ensure ground detection
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player_On_Ground = true;
        }
    }
}

