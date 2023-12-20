using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour {
    // A boolean to know if the flag is captured
    private bool isCaptured = false;
    private GameObject flagBase;
    private CommonFunctions commonFunctions;

    private void Awake() {
        commonFunctions = GameObject.Find("Common Functions Container").GetComponent<CommonFunctions>();

        flagBase = commonFunctions.FindChildWithTag(gameObject, "Flag Base");
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
}