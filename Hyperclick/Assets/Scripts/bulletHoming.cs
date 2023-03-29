// Changes bullet velocity to target player
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHoming : MonoBehaviour {
    // Tracking Variables
    public float speed = 10f;
    public float turnSpeed = 5f;
    // Objects
    public GameObject player;
    public Rigidbody2D rb;
    // Run when bullet spawned in
    private void Start() {
        // Adjust starting velocity
        Vector2 forceDirection = transform.up;
        rb.AddForce(forceDirection * speed, ForceMode2D.Impulse);
    }
    // Execute once per physics update
    private void FixedUpdate() {
        if (rb != null && player != null) { // Prevent bullet from targetting player before player has been assigned
            // Calculate the direction towards the player
            Vector2 directionToPlayer = player.transform.position - transform.position;
            // Calculate the direction the bullet should be facing
            Vector2 desiredDirection = Vector2.Lerp(transform.right, directionToPlayer.normalized, Time.fixedDeltaTime * turnSpeed);
            // Set the right vector to be the desired direction
            transform.right = desiredDirection;
            // Set the velocity based on the new direction
            rb.velocity = transform.right * speed;
        }
    }
}
