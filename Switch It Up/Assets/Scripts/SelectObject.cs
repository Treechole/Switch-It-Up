using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour, IPointerClickHandler {
    public GameObject selectedGameObject = null;
    
    public Dictionary<int, GameObject> objects = new Dictionary<int, GameObject>();
    public Dictionary<TileBase, GameObject> tilesGameObject = new Dictionary<TileBase, GameObject>();
    private List<int> slotIndices = new List<int>();

    private Color selectedColor = new Color(1f, 1f, 1f, 125f/255f);
    private Color unselectedColor = new Color(0.1603774f, 0.1603774f, 0.1603774f, 0.7058824f);

    private void Awake() {
        InitializeObjects();
    }

    // Temporary system - try to implement enum on tiles
    private void InitializeObjects() {
        for (int slotIndex = 0; slotIndex < transform.childCount; slotIndex++) {
            bool objectInSlot = transform.GetChild(slotIndex).childCount == 1;
            if (objectInSlot) {
                Transform obj = transform.GetChild(slotIndex).GetChild(0);
                objects.Add(slotIndex, obj.gameObject);

                tilesGameObject.Add(obj.GetComponent<ObjectManager>().GetObject().tile, obj.gameObject);
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
                if (objects.Keys.Contains(i)) { // For de-selection of object when an empty slot is pressed;
                    numberSelection = objects[i];
                } else {
                    DeselectOtherObjects(null);
                }
            }
        }

        return numberSelection;
    }

    public void SelectCurrentObject(GameObject selection) {
        selectedGameObject = selection;
        
        Transform selectionSlot = selection.transform.parent;
        selectionSlot.GetComponent<Image>().color = selectedColor;
    }

    public void DeselectOtherObjects(GameObject selection) {
        foreach (GameObject obj in objects.Values) {
            if (obj != selection) {
                Transform objSlot = obj.transform.parent;
                objSlot.GetComponent<Image>().color = unselectedColor;
            }
        }
    }

    // Add a system to redo placement of tiles 

    public void RemoveUsedItem(GameObject itemToRemove) {
        foreach (int objIndex in objects.Keys) {
            if (itemToRemove == objects[objIndex]) {
                if (objects[objIndex] == selectedGameObject) {
                    selectedGameObject = null;
                    objects[objIndex].transform.parent.GetComponent<Image>().color = unselectedColor;
                }

                objects.Remove(objIndex);
                break;
            }
        }
    }

    // To deselect object on clicking empty spaces in the panel
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            selectedGameObject = null;
            DeselectOtherObjects(null);
        }
    }

}