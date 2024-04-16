using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EditModeManager : MonoBehaviour {
    private bool editMode = false;

    [SerializeField] private GameObject flag;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerBase;

    [SerializeField] private GameObject healthBarSprite;
    [SerializeField] private Tilemap placeabilityTilemap;
    [SerializeField] private GameObject objectSelector;
    [SerializeField] private PlaceObstacles placeObjects;

    [SerializeField] private GameObject cinemachine;

    private void Awake() {
        StartPlayMode();
    }

    public bool CanEdit() {
        return editMode;
    }

    public void SwitchMode() {
        if (editMode) {
            StartPlayMode();
        } else {
            StartEditMode();
        }
    }

    private void StartPlayMode() {
        healthBarSprite.SetActive(true);
        objectSelector.SetActive(false);
        placeObjects.enabled = false;

        HidePlaceability();
        cinemachine.GetComponent<CinemachineVirtualCamera>().Priority = 11;

        editMode = false;
    }

    private void StartEditMode() {
        healthBarSprite.SetActive(false);
        objectSelector.SetActive(true);
        placeObjects.enabled = true;

        player.transform.position = playerBase.transform.position;
        flag.GetComponent<FlagController>().ReturnFlagToBase();

        cinemachine.GetComponent<CinemachineVirtualCamera>().Priority = 9;

        editMode = true;
    }

    private void HidePlaceability() {
        foreach (Vector3Int tilePosition in placeabilityTilemap.cellBounds.allPositionsWithin) {
            if (placeabilityTilemap.GetTile(tilePosition)) {
                placeabilityTilemap.SetTile(tilePosition, null);
            }
        }
    }
}
