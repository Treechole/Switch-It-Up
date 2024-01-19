using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBaseController : MonoBehaviour {
    private Transform player;
    private CommonFunctions commonFunctions;
    private EditModeManager editModeManager;

    private void Awake() {
        player = GameObject.Find("Player").transform;
        commonFunctions = GameObject.Find("Common Functions Container").GetComponent<CommonFunctions>();
        editModeManager = GameObject.Find("Edit Mode").GetComponent<EditModeManager>();

        InitializeBase();
    }

    private void OnTriggerEnter2D(Collider2D collidedObject) {
        if (collidedObject.CompareTag("Player")) {
            GameObject flag = commonFunctions.FindChildWithTag(player.gameObject, "Flag");

            if (flag != null) {
                ReturnFlag(flag.transform);
            }
        }
    }

    private void ReturnFlag(Transform flag) {
        flag.SetParent(null);
        flag.position = transform.position;
        flag.GetComponent<BoxCollider2D>().enabled = true;

        editModeManager.SwitchMode();
    }

    private void InitializeBase() {
        transform.position = player.position;
        GetComponent<BoxCollider2D>().size = player.localScale;
    }
}