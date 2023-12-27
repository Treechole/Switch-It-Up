using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
    private Vector2 moveDir;
    private float fireballSpeed = 5f;

    private float fireballDamage = 20f;

    private void Update() {
        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * fireballSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D character) {
        if (character.CompareTag("Player")) {
            HealthController playerHealth = character.transform.GetComponent<HealthController>();
            playerHealth.DealDamage(fireballDamage);

            Destroy(gameObject);
        }

        // Won't work as neither fireball nor wall has a rigidbody
        if (character.CompareTag("Wall")) {
            Destroy(gameObject);
        }
        
    }

    public void SetVelocityDirection(Vector2 direction) {
        moveDir = direction;
    }

}
