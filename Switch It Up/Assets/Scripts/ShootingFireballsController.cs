using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShootingFireballsController : MonoBehaviour {
    [SerializeField] private GameObject fireball;
    [SerializeField] private float rechargeTime = 4f;
    private bool recharged = false;
    private bool currentlyRecharging = false;

    private Tilemap shooterTilemap;

    private void Awake() {
        shooterTilemap = GetComponent<Tilemap>();
    }

    private void Update() {
        if (recharged) {
            ShootFireball();
        } else if (!currentlyRecharging) {
            StartCoroutine(RechargeFireball());
        }
    }

    // Program the fireball
    // Design the shooter and subsequently the fireballs for them being at different walls - shooting fireballs down, up, left, right as well
    private List<Vector3Int> GetShooterPositions() {
        List<Vector3Int> shooterPos = new List<Vector3Int>();

        foreach (Vector3Int tilePos in shooterTilemap.cellBounds.allPositionsWithin) {
            if (shooterTilemap.HasTile(tilePos)) {
                shooterPos.Add(tilePos);
            }
        }

        return shooterPos;
    }

    private void ShootFireball() {
        List<Vector3Int> shooterPositions = GetShooterPositions();

        foreach (Vector3Int shooterPos in shooterPositions) {
            Transform fireballShot = Instantiate(fireball).transform;
            fireballShot.position = shooterPos + shooterTilemap.tileAnchor;
            fireballShot.GetComponent<FireballController>().SetVelocityDirection(Vector2.down);
        }

        recharged = false;
    }

    private IEnumerator RechargeFireball() {
        currentlyRecharging = true;
        yield return new WaitForSeconds(rechargeTime);
        recharged = true;
        currentlyRecharging = false;
    }
}
