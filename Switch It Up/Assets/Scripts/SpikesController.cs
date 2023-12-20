using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour {
    // Spikes' stats
    [SerializeField] private float damagePerHit = 10f;
    [SerializeField] private float rechargeTime = 2f;

    // Player-interaction info
    private GameObject player = null;
    private bool playerOnPlatform = false;

    // Spike-mechanism related info
    private bool recharged = true;
    private bool currentlyRecharging = false;

    private void Update() {
        if (playerOnPlatform) {
            DamagePlayer();
        }

        if (!recharged && !currentlyRecharging) {
            StartCoroutine(RechargePlatform());
        }
    }

    private void OnTriggerEnter2D(Collider2D character) {
        if (character.CompareTag("Player")) {
            playerOnPlatform = true;
            player = character.gameObject;
        }
    }

    private void DamagePlayer() {
        if (recharged) {
            HealthController playerHealthSystem = player.GetComponent<HealthController>();

            playerHealthSystem.DealDamage(damagePerHit);
            recharged = false;
        }
    }

    private void OnTriggerExit2D(Collider2D character) {
        if (character.CompareTag("Player")) {
            playerOnPlatform = false;
            player = null;
        }
    }

    private IEnumerator RechargePlatform() {
        currentlyRecharging = true;
        yield return new WaitForSeconds(rechargeTime);
        recharged = true;
        currentlyRecharging = false;
    }
}
