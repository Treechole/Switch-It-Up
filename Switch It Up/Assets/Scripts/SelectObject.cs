using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SelectObject : MonoBehaviour {
    public GameObject selectedObject = null;
    
    private Dictionary<int, GameObject> objects = new Dictionary<int, GameObject>();

    private List<int> slotIndices = new List<int>();

    private void Awake() {
        InitializeObjects();
    }

    private void InitializeObjects() {
        for (int slotIndex = 0; slotIndex < transform.childCount; slotIndex++) {
            bool objectInSlot = transform.GetChild(slotIndex).childCount == 1;
            if (objectInSlot) {
                Transform obj = transform.GetChild(slotIndex).GetChild(0);
                objects.Add(slotIndex, obj.gameObject);
            }

            slotIndices.Add(slotIndex);
        }
    }

    private void Update() {
        GameObject numberSelection = SelectUsingNumbers();
        if (numberSelection != null) {
            SelectCurrentObject(numberSelection);
            DeselectOtherObjects(numberSelection);
        }
    }

    private GameObject SelectUsingNumbers() {
        GameObject numberSelection = null;

        foreach (int i in slotIndices) {
            if (Input.GetKeyUp(KeyCode.Alpha1 + i)) {
                if (ifPresentIn(i, objects.Keys)) { // For de-selection of object when an empty slot is pressed;
                    numberSelection = objects[i];
                } else {
                    DeselectOtherObjects(null);
                }
            }
        }

        return numberSelection;
    }

    public void SelectCurrentObject(GameObject selection) {
        // ObjectManager selectionManager = selection.GetComponent<ObjectManager>();
        selectedObject = selection; //Manager.GetObject();
        
        Transform selectionSlot = selection.transform.parent;
        selectionSlot.GetComponent<Image>().color = new Color(1f, 1f, 1f, 125f/255f);
    }

    public void DeselectOtherObjects(GameObject selection) {
        foreach (GameObject obj in objects.Values) {
            if (obj != selection) {
                Transform objSlot = obj.transform.parent;
                objSlot.GetComponent<Image>().color = new Color(0.1603774f, 0.1603774f, 0.1603774f, 0.7058824f);
            }
        }
    }

    private bool ifPresentIn(int i, Dictionary<int, GameObject>.KeyCollection list) {
        foreach (int element in list) {
            if (i == element) {
                return true;
            }
        }

        return false;
    }
}