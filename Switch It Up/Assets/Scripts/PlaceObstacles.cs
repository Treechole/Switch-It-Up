using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlaceObstacles : MonoBehaviour {
    [SerializeField] private SelectObject objectSelection;
    private GameObject selectedGameObject = null;
    private Dictionary<TileBase, GameObject> tilesGameObject = new Dictionary<TileBase, GameObject>();

    [SerializeField] private GameObject playerBase;
    [SerializeField] private GameObject flagBase;

    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap placeabilityTilemap;
    public List<Tilemap> objectTilemaps = new List<Tilemap>();

    [SerializeField] private Tile placeableTile;
    [SerializeField] private Tile nonPlaceableTile;

    private Vector3Int previousMousePosition;
    private Vector3Int currentMousePosition;

    public TileBase currentObjectTile = null;

    private void Awake() {
        previousMousePosition = floorTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void Update() {
        selectedGameObject = objectSelection.selectedGameObject;
        tilesGameObject = objectSelection.tilesGameObject;

        currentObjectTile = GetObjectTileAt(currentMousePosition);
        PlaceTilesOnHoverAndClick();
    }

    private void PlaceTilesOnHoverAndClick() {
        currentMousePosition = floorTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        bool onFloorTile = floorTilemap.HasTile(currentMousePosition);

        if (onFloorTile) {
            if (isTilePlaceable(currentMousePosition)) {
                placeabilityTilemap.SetTile(currentMousePosition, placeableTile);
                if (Input.GetMouseButtonDown(0)) {
                    if (canChangeCurrentTile()) { RemoveObject(currentMousePosition); }
                    PlaceObject();
                }
            } else {
                placeabilityTilemap.SetTile(currentMousePosition, nonPlaceableTile);
            }
        }

        HidePreviousTilePlaceablility();
    }

    private bool isTilePlaceable(Vector3Int tilePosition) {
        bool canChangeTile = true;
        if (currentObjectTile && !canChangeCurrentTile()) { canChangeTile = false; }

        return !isTileNearBases(tilePosition) && canChangeTile;
    }

    private bool isTileNearBases(Vector3Int tilePosition) {
        Vector3Int playerBasePosition = floorTilemap.WorldToCell(playerBase.transform.position);
        Vector3Int flagBasePosition = floorTilemap.WorldToCell(flagBase.transform.position);

        if (Vector3.Magnitude(playerBasePosition - tilePosition) < 2 || Vector3.Magnitude(flagBasePosition - tilePosition) < 2) {
            return true;
        }
        
        return false;
    }

    private bool canChangeCurrentTile() {
        if (currentObjectTile && selectedGameObject) {
            TileBase selectedTile = selectedGameObject.GetComponent<ObjectManager>().GetObject().tile;
            if (currentObjectTile != selectedTile) { return true; }
        }

        return false;
    }
    
    private void PlaceObject() {
        if (selectedGameObject != null) {
            ObjectManager selection_ObjectManager = selectedGameObject.GetComponent<ObjectManager>();
            PlaceableObject selectedObject = selection_ObjectManager.GetObject();

            Tilemap objectTilemap = objectTilemaps[(int) selectedObject.type];
            TileBase selectedTile = selectedObject.tile;

            objectTilemap.SetTile(currentMousePosition, selectedTile);
            selection_ObjectManager.ChangeObjectQuantityBy(-1);
        }
    }

    private void RemoveObject(Vector3Int tilePosition) {
        if (currentObjectTile) {
            GameObject placedGameObject = tilesGameObject[currentObjectTile];
            Tilemap placedObject_Tilemap = objectTilemaps[(int) placedGameObject.GetComponent<ObjectManager>().GetObject().type];

            placedObject_Tilemap.SetTile(tilePosition, null);
            placedGameObject.GetComponent<ObjectManager>().ChangeObjectQuantityBy(1);
        }
    }

    private TileBase GetObjectTileAt(Vector3Int tilePosition) {
        foreach (Tilemap tilemap in objectTilemaps) {
            TileBase currentTile = tilemap.GetTile(tilePosition);
            if (currentTile) { return currentTile; }
        }

        return null;
    }

    private void HidePreviousTilePlaceablility() {
        if (previousMousePosition != currentMousePosition) {
            placeabilityTilemap.SetTile(previousMousePosition, null);
            previousMousePosition = currentMousePosition;
        }
    }
}
