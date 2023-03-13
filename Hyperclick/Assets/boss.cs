using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{

    enum Direction
    {
        N = 0,
        NE = 1,
        E = 2,
        SE = 3,
        S = 4,
        SW = 5,
        W = 6,
        NW = 7
    }


    public GameObject self;
    public GameObject lFirePoint;
    public GameObject rFirePoint;

    public GameObject bullet;
    public GameObject target;

    public Rigidbody2D player;

    public SpriteRenderer displayedSprite;

    public Sprite nSprite;
    public Sprite neSprite;
    public Sprite eSprite;
    public Sprite seSprite;
    public Sprite sSprite;
    public Sprite swSprite;
    public Sprite wSprite;
    public Sprite nwSprite;

    public float MAX_BULLET_DELAY = 1f;
    public float MAX_TARGET_DELAY = 1f;

    public float TARGET_LEFT = -6f;
    public float TARGET_RIGHT = 6f;
    public float TARGET_TOP = 3f;
    public float TARGET_BOTTOM = -2f;

    public float MIN_CENTER_DIS = 0.5f;

    private float targetAngle = 1f;

    private Direction curDir = Direction.S;

    private float bulletDelay = 0f;
    private float targetDelay = 0f;

    // Update is called once per frame
    void Update()
    {
        targetAngle = getPlayerAngle();
        int attempt = 0;
        while (!isFacing(targetAngle) && attempt < 10)
        {
            attempt++;
            int newDir = (int)curDir + 1;
            if (newDir > (int)Direction.NW)
            {
                newDir = 0;
            }
            curDir = (Direction)newDir;
            updateRotation();
        }

        bulletDelay += Time.deltaTime;
        if (bulletDelay > MAX_BULLET_DELAY)
        {
            bulletDelay = 0f;
            // Shoots in direction currently facing
            //Instantiate(bullet, lFirePoint.transform.position, Quaternion.Euler(Vector3.forward * 45f * (float)curDir * -1f));
            //Instantiate(bullet, rFirePoint.transform.position, Quaternion.Euler(Vector3.forward * 45f * (float)curDir * -1f));

            // Shoots in player direction
            GameObject leftBullet = Instantiate(bullet, lFirePoint.transform.position, Quaternion.Euler(Vector3.forward));
            leftBullet.transform.up = player.transform.position - leftBullet.transform.position;
            GameObject rightBullet = Instantiate(bullet, rFirePoint.transform.position, Quaternion.Euler(Vector3.forward));
            rightBullet.transform.up = player.transform.position - rightBullet.transform.position;
        }

        targetDelay += Time.deltaTime;
        if (targetDelay > MAX_TARGET_DELAY)
        {
            targetDelay = 0f;
            Vector3 targetPos = new Vector3(0f, 0f, 0f);
            while (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y) < MIN_CENTER_DIS)
            {
                targetPos = new Vector3(Random.Range(TARGET_LEFT, TARGET_RIGHT), Random.Range(TARGET_TOP, TARGET_BOTTOM), 0f);
            }
            GameObject newTarget = Instantiate(target, targetPos, Quaternion.Euler(Vector3.forward));
        }
    }

    void updateRotation()
    {
        switch (curDir)
        {
            case Direction.N:
                displayedSprite.sprite = nSprite;
                lFirePoint.transform.localPosition = new Vector3(0.956f, 0.061f, 0f);
                rFirePoint.transform.localPosition = new Vector3(-0.736f, 0.071f, 0f);
                break;
            case Direction.NE:
                displayedSprite.sprite = neSprite;
                lFirePoint.transform.localPosition = new Vector3(0.959f, -0.482f, 0f);
                rFirePoint.transform.localPosition = new Vector3(-0.353f, 0.53f, 0f);
                break;
            case Direction.E:
                displayedSprite.sprite = eSprite;
                lFirePoint.transform.localPosition = new Vector3(0.374f, -0.893f, 0f);
                rFirePoint.transform.localPosition = new Vector3(0.389f, 0.543f, 0f);
                break;
            case Direction.SE:
                displayedSprite.sprite = seSprite;
                lFirePoint.transform.localPosition = new Vector3(-0.351f, -0.824f, 0f);
                rFirePoint.transform.localPosition = new Vector3(0.824f, 0.017f, 0f);
                break;
            case Direction.S:
                displayedSprite.sprite = sSprite;
                lFirePoint.transform.localPosition = new Vector3(-0.845f, -0.445f, 0f);
                rFirePoint.transform.localPosition = new Vector3(0.845f, -0.445f, 0f);
                break;
            case Direction.SW:
                displayedSprite.sprite = swSprite;
                lFirePoint.transform.localPosition = new Vector3(-0.752f, 0.1f, 0f);
                rFirePoint.transform.localPosition = new Vector3(0.42f, -0.775f, 0f);
                break;
            case Direction.W:
                displayedSprite.sprite = wSprite;
                lFirePoint.transform.localPosition = new Vector3(-0.268f, 0.63f, 0f);
                rFirePoint.transform.localPosition = new Vector3(-0.268f, -0.81f, 0f);
                break;
            case Direction.NW:
                displayedSprite.sprite = nwSprite;
                lFirePoint.transform.localPosition = new Vector3(0.546f, 0.599f, 0f);
                rFirePoint.transform.localPosition = new Vector3(-0.726f, -0.422f, 0f);
                break;
        }
    }

    bool isFacing(float angle)
    {
        angle *= -1;
        if (angle <= -22.5f) { angle += 360f; }
        return angle > -22.5f + 45f * (float) curDir && angle < 22.5f + 45f * (float) curDir;
    }

    float getPlayerAngle()
    {
        Vector2 selfPos;
        selfPos.x = self.transform.position.x;
        selfPos.y = self.transform.position.y;
        Vector2 plrPos;
        plrPos.x = player.position.x;
        plrPos.y = player.position.y;
        return Vector2.SignedAngle(selfPos, plrPos);
    }

}
