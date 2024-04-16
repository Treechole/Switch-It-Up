using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour {
    // A boolean to know if the flag is captured
    public bool isCaptured = false;
    [SerializeField] private GameObject flagBase;

    private void Awake() {
        flagBase.transform.position = transform.position;
    }

    // On interacting with the flag
    private void OnTriggerEnter2D(Collider2D collidedObject) {
        if (collidedObject.CompareTag("Player") && !isCaptured) {
            Transform player = collidedObject.transform;
            PickupFlag(player);
        }
    }

    private void PickupFlag(Transform player) {
        transform.SetParent(player);
        transform.localPosition = new Vector3(0.4f, 0, 0);
        transform.GetComponent<BoxCollider2D>().enabled = false;

        flagBase.SetActive(false);

        isCaptured = true;
    }

    public void ReturnFlagToBase() {
        transform.SetParent(null);
        transform.localPosition = flagBase.transform.position;
        transform.GetComponent<BoxCollider2D>().enabled = true;

        flagBase.SetActive(true);

        isCaptured = false;
    }
}