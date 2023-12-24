using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunctions : MonoBehaviour {
    public bool editMode = false;

    public GameObject FindChildWithTag (GameObject parent, string tag) {
        GameObject child = null;

        foreach (Transform transform in parent.transform) {
            if (transform.CompareTag(tag)) {
                child = transform.gameObject;
                break;
            }
        }
        
        return child;
    }

    public void SwitchCurrentMode() {
        if (editMode) {
            editMode = false;
        } else {
            editMode = true;
        }

        Transform player = GameObject.Find("Player").transform;
        Transform playerBase = GameObject.Find("Start and Finish Base").transform;
        GameObject flag = GameObject.Find("Flag");

        player.position = playerBase.position;
        flag.SetActive(false);
    }
}
