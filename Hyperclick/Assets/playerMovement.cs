using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerMovement : MonoBehaviour
{
    public UnityEvent damage;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        if (actionBar.dead) { return; }
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _yMov = Input.GetAxisRaw("Vertical");

        Vector2 _movHorizontal = transform.right * _xMov;
        Vector2 _movVertical = transform.up * _yMov;

        movement = (_movHorizontal + _movVertical).normalized;
    }

    void FixedUpdate()
    {
        if (actionBar.dead) { return; }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (actionBar.dead) { return; }
        if (other.CompareTag("Bullet"))
        {
            damage.Invoke();
            bulletMotion bulletScript = other.gameObject.transform.parent.GetComponent<bulletMotion>();
            bulletScript.StartCoroutine(bulletScript.explode());
        }
    }
}
