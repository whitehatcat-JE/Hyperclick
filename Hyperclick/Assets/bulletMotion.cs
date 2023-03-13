using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMotion : MonoBehaviour
{
    public GameObject self;
    public Rigidbody2D rdby;

    public Sprite spriteB;
    public Sprite spriteC;

    public SpriteRenderer displayedSprite;

    public float moveSpeed = 1f;

    float MAX_TIME = 25f;

    float expireTime = 0f;

    int collisionCount = 0;
    int MAX_COLLISIONS = 3;
    // Start is called before the first frame update
    void Start()
    {
        self.tag = "Bullet";
        rdby.AddForce(self.transform.up * moveSpeed);
    }

    void Update()
    {
        expireTime += Time.deltaTime;
        if (expireTime > MAX_TIME)
        {
            Destroy(self);
        }
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    self.transform.position += self.transform.up * Time.deltaTime * moveSpeed;
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            collisionCount++;
            switch (collisionCount)
            {
                case 1:
                    displayedSprite.sprite = spriteB;
                    break;
                case 2:
                    displayedSprite.sprite = spriteC;
                    break;
            }
            if (collisionCount >= MAX_COLLISIONS)
            {
                Destroy(self);
            }
        }
    }
}
