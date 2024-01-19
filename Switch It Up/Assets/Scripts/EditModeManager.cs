using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EditModeManager : MonoBehaviour {
    private bool editMode = false;

    [SerializeField] private GameObject flag;
    [SerializeField] private GameObject flagBase;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerBase;

    [SerializeField] private GameObject healthBarSprite;
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

        cinemachine.GetComponent<CinemachineVirtualCamera>().Priority = 11;

        editMode = false;
    }

    private void StartEditMode() {
        healthBarSprite.SetActive(false);
        objectSelector.SetActive(true);
        placeObjects.enabled = true;

        player.transform.position = playerBase.transform.position;
        flag.transform.position = flagBase.transform.position;
        flagBase.SetActive(true);

        cinemachine.GetComponent<CinemachineVirtualCamera>().Priority = 9;

        editMode = true;
    }
}
