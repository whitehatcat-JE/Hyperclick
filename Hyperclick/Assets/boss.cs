using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class boss : MonoBehaviour
{
    public UnityEvent attackComplete;

    public CinemachineImpulseSource fireScreenShake;

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

    public float MAX_TARGET_DELAY = 1f;

    public float TARGET_LEFT = -6f;
    public float TARGET_RIGHT = 6f;
    public float TARGET_TOP = 3f;
    public float TARGET_BOTTOM = -2f;

    public float MIN_CENTER_DIS = 0.5f;

    private Direction curDir = Direction.S;

    public IEnumerator targetPhase(int targets, float spawnCooldown = 0.5f, int targetGroupingMin = 1, int targetGroupingMax = 1)
    {
        for (int target = 0; target < targets; target++)
        {
            yield return new WaitForSeconds(spawnCooldown);
            int targetItemCount = Random.Range(targetGroupingMin, targetGroupingMax + 1);
            for (int targetItem = 0; targetItem < targetItemCount; targetItem++)
            {
                spawnTarget();
            }
        }
        attackComplete.Invoke();
    }

    public IEnumerator spinAttack(int bullets, float bulletDelay, float rotationDelay)
    {
        curDir = Direction.N;
        updateRotation();
        do
        {
            for (int bullet = 0; bullet < bullets; bullet++)
            {
                yield return new WaitForSeconds(bulletDelay);
                spawnBulletsAtFacing();
            }
            incrementDirection();
            yield return new WaitForSeconds(rotationDelay);
        } while (curDir != Direction.N);
        attackComplete.Invoke();
    }

    public IEnumerator targetedAttack(int bullets, float bulletDelay, bool alternate = false, int repeatAmt = 1, float repeatDelay = 0f)
    {
        bool shootLeft = false;
        for (int repeat = 0; repeat < repeatAmt; repeat++) {
            for (int bullet = 0; bullet < bullets; bullet++)
            {
                float targetAngle = getPlayerAngle();
                int attempt = 0;
                bool rotClockwise = false;
                if (!isFacing(targetAngle))
                {
                    float angleDisplacement = 0f;
                    do
                    {
                        angleDisplacement -= 20f;
                        rotClockwise = isFacing(targetAngle + angleDisplacement);
                    } while (angleDisplacement > -180f && !rotClockwise);
                }
                while (!isFacing(targetAngle) && attempt < 8)
                {
                    attempt++;
                    int newDir = (int)curDir;
                    if (rotClockwise)
                    {
                        newDir--;
                        if (newDir < 0) { newDir = (int)Direction.NW; }
                    }
                    else
                    {
                        newDir++;
                        if (newDir > (int) Direction.NW) { newDir = 0; }
                    }
                    curDir = (Direction)newDir;
                    updateRotation();
                    yield return new WaitForSeconds(0.1f);
                }
                if (alternate) {
                    shootLeft = !shootLeft;
                    spawnBulletsAtPlayer(shootLeft, !shootLeft);
                } else { spawnBulletsAtPlayer(); }
                yield return new WaitForSeconds(bulletDelay);
            }
            yield return new WaitForSeconds(repeatDelay);
        }
        attackComplete.Invoke();
    }

    void incrementDirection()
    {
        int newDir = (int)curDir + 1;
        if (newDir > (int)Direction.NW)
        {
            newDir = 0;
        }
        curDir = (Direction)newDir;
        updateRotation();
    }

    void spawnTarget()
    {
        Vector3 targetPos = new Vector3(0f, 0f, 0f);
        while (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y) < MIN_CENTER_DIS)
        {
            targetPos = new Vector3(Random.Range(TARGET_LEFT, TARGET_RIGHT), Random.Range(TARGET_TOP, TARGET_BOTTOM), 0f);
        }
        GameObject newTarget = Instantiate(target, targetPos, Quaternion.Euler(Vector3.forward));
    }

    void spawnBulletsAtFacing(bool lBullet = true, bool rBullet = true)
    {
        fireScreenShake.GenerateImpulseWithForce(1f);
        if (lBullet) { Instantiate(bullet, lFirePoint.transform.position, Quaternion.Euler(Vector3.forward * 45f * (float)curDir * -1f)); }
        if (rBullet) { Instantiate(bullet, rFirePoint.transform.position, Quaternion.Euler(Vector3.forward * 45f * (float)curDir * -1f)); }
    }

    void spawnBulletsAtPlayer(bool lBullet = true, bool rBullet = true)
    {
        fireScreenShake.GenerateImpulseWithForce(1f);
        if (lBullet)
        {
            GameObject leftBullet = Instantiate(bullet, lFirePoint.transform.position, Quaternion.Euler(Vector3.forward));
            leftBullet.transform.up = player.transform.position - leftBullet.transform.position;
        }
        if (rBullet)
        {
            GameObject rightBullet = Instantiate(bullet, rFirePoint.transform.position, Quaternion.Euler(Vector3.forward));
            rightBullet.transform.up = player.transform.position - rightBullet.transform.position;
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
