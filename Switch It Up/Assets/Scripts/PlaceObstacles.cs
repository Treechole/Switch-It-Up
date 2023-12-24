using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlaceObstacles : MonoBehaviour {
    [SerializeField] private SelectObject objectSelection;
    private GameObject selectedGameObject = null;

    private Tilemap floorTilemap;
    [SerializeField] private GameObject playerBase;

    [SerializeField] private Tilemap placeOnTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap spikesTilemap;

    [SerializeField] private Tile placeableTile;
    [SerializeField] private Tile nonPlaceableTile;

    private Vector3Int previousMousePosition;
    private Vector3Int currentMousePosition;

    private void Awake() {
        floorTilemap = GetComponent<Tilemap>();
        previousMousePosition = floorTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void Update() {
        selectedGameObject = objectSelection.selectedObject;
        ShowPlaceableTilesOnHover();
    }

    private void ShowPlaceableTilesOnHover() {
        currentMousePosition = floorTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        bool onFloorTile = floorTilemap.HasTile(currentMousePosition);

        if (onFloorTile) {
            if (isTilePlaceable(currentMousePosition)) {
                placeOnTilemap.SetTile(currentMousePosition, placeableTile);

                if (Input.GetMouseButtonDown(0)) {
                    PlaceObject();
                }
            } else {
                placeOnTilemap.SetTile(currentMousePosition, nonPlaceableTile);
            }

            HidePreviousTilePlaceablility();
        }
    }

    private bool isTilePlaceable(Vector3Int tilePosition) {
        Vector3Int playerBasePosition = floorTilemap.WorldToCell(playerBase.transform.position);
        if (Vector3.Magnitude(playerBasePosition - tilePosition) < 2) {
            return false;
        }
        return true;
    }

    private void PlaceObject() {
        if (selectedGameObject != null) {
            PlaceableObject selectedObject = selectedGameObject.GetComponent<ObjectManager>().GetObject();
            
            // Refine code by using things nly once - yaani connect information with each other rather than re utilising the base info again
            // Like having all the tilempas stored in some list according to the ObjectType enum and then using the values of enum (0, 1, 2, ...) and accessing the tilemaps
            switch (selectedObject.type) {
                case PlaceableObject.ObjectType.Wall:
                    wallTilemap.SetTile(currentMousePosition, selectedObject.tile);
                    break;

                case PlaceableObject.ObjectType.Spikes:
                    spikesTilemap.SetTile(currentMousePosition, selectedObject.tile);
                    break;
            }
        }
    }

    private void HidePreviousTilePlaceablility() {
        if (previousMousePosition != currentMousePosition) {
            placeOnTilemap.SetTile(previousMousePosition, null);
            previousMousePosition = currentMousePosition;
        }
    }
}
