using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonManager : MonoBehaviour {
    private EditModeManager editModeManager;
    [SerializeField] private Tilemap placeabilityTilemap;

    private void Awake() {
        editModeManager = GameObject.Find("Edit Mode").GetComponent<EditModeManager>();
    }

    public void ExitEditing() {
        editModeManager.SwitchMode();
        HidePlaceability();
    }

    private void HidePlaceability() {
        foreach (Vector3Int tilePosition in placeabilityTilemap.cellBounds.allPositionsWithin) {
            if (placeabilityTilemap.GetTile(tilePosition)) {
                placeabilityTilemap.SetTile(tilePosition, null);
            }
        }
    }
    
}
