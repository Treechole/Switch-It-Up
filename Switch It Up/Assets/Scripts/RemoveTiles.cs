using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class RemoveTiles : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private SelectObject objectSelection;
    [SerializeField] private PlaceObstacles placeObstacles;
    private bool canRemove = false;

    private void Update() {
        if (canRemove) {
            if (TileAtCurrentPosition() != null) {
                
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (canRemove) {
            canRemove = false;
        } else {
            canRemove = true;
            
            objectSelection.SelectCurrentObject(null);
            objectSelection.DeselectOtherObjects(null);
        }
    }

    private TileBase TileAtCurrentPosition() {
        return placeObstacles.currentObjectTile;
    }

    public bool CanRemove() {
        return canRemove;
    }
}
