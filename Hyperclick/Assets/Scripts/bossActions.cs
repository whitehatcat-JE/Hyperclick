// Controls boss attacks and rotations
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class bossActions : MonoBehaviour {
    // Boss Direction Enumerator
    enum Direction {
        N = 0,
        NE = 1,
        E = 2,
        SE = 3,
        S = 4,
        SW = 5,
        W = 6,
        NW = 7
    }
    // Events
    public UnityEvent attackComplete;
    // Game Objects
    public CinemachineImpulseSource fireScreenShake;
    //      Bullet
    public GameObject lFirePoint;
    public GameObject rFirePoint;

    public GameObject bullet;
    public GameObject traceBullet;
    public GameObject target;
    //      Player
    public GameObject playerObj;
    public Rigidbody2D playerRB;
    // Sprites
    public SpriteRenderer displayedSprite;
    public Sprite[] directionSprites = new Sprite[8];
    // Target Variables
    private const float MAX_TARGET_DELAY = 1f;

    private const float TARGET_LEFT = -6f;
    private const float TARGET_RIGHT = 6f;
    private const float TARGET_TOP = 3f;
    private const float TARGET_BOTTOM = -2f;

    private const float MIN_CENTER_DIS = 4f;
    // Direction Variables
    private Direction curDir = Direction.S;
    // Spawn set amount of targets randomly around arena
    public IEnumerator targetPhase(int targets, float spawnCooldown = 0.5f, int minAmt = 1, int maxAmt = 1) { // min & max refers to amount of targets that can spawn at once
        for (int target = 0; target < targets; target++) { // Loop target spawning
            yield return new WaitForSeconds(spawnCooldown);
            if (gameManager.dead) { yield break; }
            for (int cnt = 0; cnt < Random.Range(minAmt, maxAmt + 1); cnt++){ spawnTarget(); } // Spawn target
        }
        attackComplete.Invoke();
    }
    // Fire bullets in all directions
    public IEnumerator spinAttack(int bullets, float bulletDelay, float rotDelay, bool trackBullets = false, bool altTrack = false, bool splitAlt = false) {
        while (curDir != Direction.N) { // Rotate boss to north direction
            curDir = (Direction) (((int)curDir + (curDir > Direction.S ? 1 : -1)) % 8);
            updateRotation();
            yield return new WaitForSeconds(0.1f);
        }
        do { // Loop through all directions
            for (int bullet = 0; bullet < bullets; bullet++) { // Fire set amount of bullets
                if (gameManager.dead || gameManager.justLeveled) { break; }
                spawnBulletsAtFacing(true, true, trackBullets);
                if (splitAlt) { trackBullets = !trackBullets; } // Alternate bullet type
                yield return new WaitForSeconds(bulletDelay);
            }
            // Rotates boss
            curDir = (Direction)(((int)curDir + 1) % 8);
            updateRotation();

            if (altTrack) { trackBullets = !trackBullets; } // Switch bullet type whenever direction changes
            yield return new WaitForSeconds(rotDelay);
            if (gameManager.dead || gameManager.justLeveled) { break; }
        } while (curDir != Direction.N);
        attackComplete.Invoke();
    }
    // Fire bullets towards player
    public IEnumerator targetedAttack(int bullets, float bulletDelay, bool alt = false, int repeatAmt = 1, float repeatDelay = 0f, bool trackBullets = false, bool altTrack = false, bool altTrackBullets = false) {
        for (int rptIdx = 0; rptIdx < repeatAmt; rptIdx++) { // Repeat targeting
            for (int bulNum = 0; bulNum < bullets; bulNum++) { // Spawn multiple bullets in calculated direction
                // Calculate whether clockwise or anti-clockwise rotation will be faster
                float angle = getPlayerAngle();
                bool rotDir = false;
                for (float offset = 0f; offset > -180f && !rotDir && !isFacing(angle); offset -= 20f) {
                    rotDir = isFacing(angle + offset);
                }
                // Rotate boss in calculated direction till facing player
                for (int attempt = 0; !isFacing(angle); attempt++) {
                    curDir = (Direction) (((int) curDir + (rotDir ? -1 : 1) + 8) % 8);
                    updateRotation();
                    yield return new WaitForSeconds(0.1f);
                    if (gameManager.dead || gameManager.justLeveled) { break; }
                }
                // Spawn bullets in direction
                spawnBulletsAtPlayer(alt ? bulNum % 2 == 0 : true, alt ? bulNum % 2 == 1 : true, altTrackBullets ? bulNum % 2 == 0 : (altTrack ? rptIdx % 2 == 0 : false));
                yield return new WaitForSeconds(bulletDelay);
                if (gameManager.dead || gameManager.justLeveled) { break; }
            }
            yield return new WaitForSeconds(repeatDelay); // Add firing cooldown
            if (gameManager.dead || gameManager.justLeveled) { break; }
        }
        attackComplete.Invoke();
    }
    // Spawn target at random position in arena
    void spawnTarget() {
        // Select position to spawn target 
        Vector3 targetPos = new Vector3(0f, 0f, 0f);
        while (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y) < MIN_CENTER_DIS) { // Ensures target doesn't spawn on top of boss sprite
            targetPos = new Vector3(Random.Range(TARGET_LEFT, TARGET_RIGHT), Random.Range(TARGET_TOP, TARGET_BOTTOM), 0f);
        }
        // Spawn target
        GameObject newTarget = Instantiate(target, targetPos, Quaternion.Euler(Vector3.forward));
    }
    // Spawn bullet/s in direction boss facing
    void spawnBulletsAtFacing(bool lBullet = true, bool rBullet = true, bool tracking = false) {
        fireScreenShake.GenerateImpulseWithForce(1f); // Screen recoil
        if (lBullet) { // Spawn bullet from left side of boss
            if (tracking) { // Spawn tracking bullet
                Quaternion direction = Quaternion.Euler(Vector3.forward * 45f * (float)curDir * -1f + Vector3.forward * 90f); // Velocity direction
                GameObject newBullet = Instantiate(traceBullet, lFirePoint.transform.position, direction);
                newBullet.GetComponent<bulletHoming>().player = playerObj; // Gives bullet player gameObject so bullet can aim towards player
            } else { // Spawn generic bullet
                Quaternion direction = Quaternion.Euler(Vector3.forward * 45f * (float)curDir * -1f);
                Instantiate(bullet, lFirePoint.transform.position, direction);
            }
        } if (rBullet) { // Spawn bullet from right side of boss
            if (tracking) { // Spawn tracking bullet
                Quaternion direction = Quaternion.Euler(Vector3.forward * 45f * (float)curDir * -1f + Vector3.forward * 90f);
                GameObject newBullet = Instantiate(traceBullet, rFirePoint.transform.position, direction);
                newBullet.GetComponent<bulletHoming>().player = playerObj; // Gives bullet player gameObject so bullet can aim towards player
            } else { // Spawn generic bullet
                Quaternion direction = Quaternion.Euler(Vector3.forward * 45f * (float)curDir * -1f);
                Instantiate(bullet, rFirePoint.transform.position, direction);
            }
        }
    }
    // Spawn bullet/s in player direction
    void spawnBulletsAtPlayer(bool lBullet = true, bool rBullet = true, bool tracking = false) {
        fireScreenShake.GenerateImpulseWithForce(1f);
        if (lBullet) {
            if (tracking) {
                // Calculate angle to fire bullet in
                Vector2 directionToPlayer = playerRB.transform.position - lFirePoint.transform.position;
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                // Spawn tracking bullet
                GameObject newBullet = Instantiate(traceBullet, lFirePoint.transform.position, Quaternion.Euler(0, 0, angle));
                newBullet.GetComponent<bulletHoming>().player = playerObj;
            } else {
                // Spawn generic bullet
                GameObject leftBullet = Instantiate(bullet, lFirePoint.transform.position, Quaternion.Euler(Vector3.forward));
                leftBullet.transform.up = playerRB.transform.position - leftBullet.transform.position;
            }
        } if (rBullet) {
            if (tracking) {
                // Calculate angle to fire bullet in
                Vector2 directionToPlayer = playerRB.transform.position - rFirePoint.transform.position;
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                // Spawn tracking bullet
                GameObject newBullet = Instantiate(traceBullet, rFirePoint.transform.position, Quaternion.Euler(0, 0, angle));
                newBullet.GetComponent<bulletHoming>().player = playerObj;
            } else {
                // Spawn generic bullet
                GameObject rightBullet = Instantiate(bullet, rFirePoint.transform.position, Quaternion.Euler(Vector3.forward));
                rightBullet.transform.up = playerRB.transform.position - rightBullet.transform.position;
            }
        }
    }
    // Adjust boss sprite & bullet spawning locations based on current direction
    void updateRotation() {
        displayedSprite.sprite = directionSprites[(int) curDir]; // Update sprite
        // Update bullet spawning locations
        switch (curDir) {
            case Direction.N:
                lFirePoint.transform.localPosition = new Vector3(0.956f, 0.061f, 0f);
                rFirePoint.transform.localPosition = new Vector3(-0.736f, 0.071f, 0f);
                break;
            case Direction.NE:
                lFirePoint.transform.localPosition = new Vector3(0.959f, -0.482f, 0f);
                rFirePoint.transform.localPosition = new Vector3(-0.353f, 0.53f, 0f);
                break;
            case Direction.E:
                lFirePoint.transform.localPosition = new Vector3(0.374f, -0.893f, 0f);
                rFirePoint.transform.localPosition = new Vector3(0.389f, 0.543f, 0f);
                break;
            case Direction.SE:
                lFirePoint.transform.localPosition = new Vector3(-0.351f, -0.824f, 0f);
                rFirePoint.transform.localPosition = new Vector3(0.824f, 0.017f, 0f);
                break;
            case Direction.S:
                lFirePoint.transform.localPosition = new Vector3(-0.845f, -0.445f, 0f);
                rFirePoint.transform.localPosition = new Vector3(0.845f, -0.445f, 0f);
                break;
            case Direction.SW:
                lFirePoint.transform.localPosition = new Vector3(-0.752f, 0.1f, 0f);
                rFirePoint.transform.localPosition = new Vector3(0.42f, -0.775f, 0f);
                break;
            case Direction.W:
                lFirePoint.transform.localPosition = new Vector3(-0.268f, 0.63f, 0f);
                rFirePoint.transform.localPosition = new Vector3(-0.268f, -0.81f, 0f);
                break;
            case Direction.NW:
                lFirePoint.transform.localPosition = new Vector3(0.546f, 0.599f, 0f);
                rFirePoint.transform.localPosition = new Vector3(-0.726f, -0.422f, 0f);
                break;
        }
    }
    // Return true if boss facing player
    bool isFacing(float angle) {
        // Clamp given angle to 0-360 degrees
        angle *= -1;
        if (angle <= -22.5f) { angle += 360f; }
        // Calculate & return whether boss facing player
        return angle > -22.5f + 45f * (float) curDir && angle < 22.5f + 45f * (float) curDir;
    }
    // Calculate boss angle to player
    float getPlayerAngle() {
        // Convert boss & player positions from Vector3 to Vector2
        Vector2 selfPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 plrPos = new Vector2(playerRB.position.x, playerRB.position.y);
        // Calculate & return angle
        return Vector2.SignedAngle(selfPos, plrPos);
    }
}
