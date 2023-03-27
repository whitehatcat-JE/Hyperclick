using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHoming : MonoBehaviour
{
    public float speed = 10f; // speed of bullet
    public float turnSpeed = 5f; // turn speed of bullet
    public GameObject player; // reference to the game object the bullet is homing towards
    private Rigidbody2D rb; // reference to the Rigidbody2D component of the bullet

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // get reference to the Rigidbody2D component of the bullet
        Vector2 forceDirection = transform.up; // get the initial force direction (upwards)
        rb.AddForce(forceDirection * speed, ForceMode2D.Impulse); // apply the initial force
    }

    private void FixedUpdate()
    {
        if (rb != null && player != null)
        {
            // calculate the direction towards the player
            Vector2 directionToPlayer = player.transform.position - transform.position;

            // calculate the direction the bullet should be facing
            Vector2 desiredDirection = Vector2.Lerp(transform.right, directionToPlayer.normalized, Time.fixedDeltaTime * turnSpeed);

            // set the right vector to be the desired direction
            transform.right = desiredDirection;

            // set the velocity based on the new direction
            rb.velocity = transform.right * speed;
        }
    }
}
