using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunctions : MonoBehaviour {
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
}
