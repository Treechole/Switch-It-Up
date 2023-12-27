using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    // Health variables of the player
    private float health;
    private float maxHealth = 100f;

    // Health Visualiser
    private GameObject healthBarSprite;

    // Colors to help visualise health
    private Color fullHealthColor = new Color(0, 0.7058824f, 0, 1);
    private Color halfHealthColor = new Color(0.7058824f, 0.7058824f, 0, 1);
    private Color noHealthColor = new Color(0.7058824f, 0, 0, 1);

    // Common functions (using FindChildWithTag)
    private CommonFunctions commonFunctions;

    private void Awake() {
        health = maxHealth;
        healthBarSprite = transform.Find("Health/Sprite").gameObject;

        commonFunctions = GameObject.Find("Common Functions Container").GetComponent<CommonFunctions>();
    }

    // Changing Health
    public void DealDamage(float damage) {
        if (health != 0) {
            health -= damage;
            commonFunctions.FindChildWithTag(gameObject, "Blood Particle System").GetComponent<BloodGenerator>().BloodSpurt(transform);
            UpdateHealthBar();

            if (health == 0) {
                CharacterDied();
            }
        }
    }

    // Health-related events
    private void CharacterDied () {
        Debug.Log("Game Over!");
        Destroy(this.gameObject);
    }

    // Visualising Health
    private void UpdateHealthBar () {
        healthBarSprite.transform.localScale = new Vector3(health/maxHealth, healthBarSprite.transform.localScale.y, healthBarSprite.transform.localScale.z);

        if (health >= maxHealth/2) {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.Lerp(halfHealthColor, fullHealthColor, (2*health/maxHealth) - 1);
        } else {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.Lerp(noHealthColor, halfHealthColor, 2*health/maxHealth);
        }
    }
}