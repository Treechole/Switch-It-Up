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

    private Tilemap floorTilemap;
    [SerializeField] private GameObject playerBase;

    [SerializeField] private Tilemap placeabilityTilemap;
    [SerializeField] private List<Tilemap> tilemaps = new List<Tilemap>();

    [SerializeField] private Tile placeableTile;
    [SerializeField] private Tile nonPlaceableTile;

    private Vector3Int previousMousePosition;
    private Vector3Int currentMousePosition;

    [SerializeField] private List<TileBase> wallTiles;

    private void Awake() {
        floorTilemap = GetComponent<Tilemap>();
        previousMousePosition = floorTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void Update() {
        selectedGameObject = objectSelection.selectedGameObject;
        PlaceTilesOnHoverAndClick();
    }

    private void PlaceTilesOnHoverAndClick() {
        currentMousePosition = floorTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        bool onFloorTile = floorTilemap.HasTile(currentMousePosition);

        if (onFloorTile) {
            if (isTilePlaceable(currentMousePosition)) {
                placeabilityTilemap.SetTile(currentMousePosition, placeableTile);
                if (Input.GetMouseButtonDown(0)) { PlaceObject(); }
            } else {
                placeabilityTilemap.SetTile(currentMousePosition, nonPlaceableTile);
            }

            HidePreviousTilePlaceablility();
        }
    }

    private bool isTilePlaceable(Vector3Int tilePosition) {
        Vector3Int playerBasePosition = floorTilemap.WorldToCell(playerBase.transform.position);
        List<Vector3Int> firstNeighbours = GetFirstNeighbours();

        if (Vector3.Magnitude(playerBasePosition - tilePosition) < 2 || !firstNeighbours.Contains(tilePosition)) {
            return false;
        }
        return true;
    }

    private List<Vector3Int> GetFirstNeighbours() {
        List<Vector3Int> neighbours = new List<Vector3Int>();

        foreach (Vector3Int tilePos in tilemaps[0].cellBounds.allPositionsWithin) {
            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    Vector3Int neighbourTilePos = tilePos + new Vector3Int(x, y, 0);
                    if (tilemaps[0].HasTile(tilePos) && !tilemaps[0].HasTile(neighbourTilePos) && !neighbours.Contains(neighbourTilePos)) {
                        neighbours.Add(neighbourTilePos);
                    }
                }
            }
        }

        return neighbours;
    }

    private void PlaceObject() {
        if (selectedGameObject != null) {
            ObjectManager selection_ObjectManager = selectedGameObject.GetComponent<ObjectManager>();
            PlaceableObject selectedObject = selection_ObjectManager.GetObject();

            // Refine code by using things nly once - yaani connect information with each other rather than re utilising the base info again
            // Like having all the tilempas stored in some list according to the ObjectType enum and then using the values of enum (0, 1, 2, ...) and accessing the tilemaps

            Tilemap objectTilemap = tilemaps[(int) selectedObject.type];
            TileBase selectedTile = selectedObject.tile;

            objectTilemap.SetTile(currentMousePosition, selectedTile);

            // switch (selectedObject.type) {
            //     case PlaceableObject.ObjectType.Wall:
            //         // wallTilemap.SetTile(currentMousePosition, selectedObject.tile);
            //         UpdateWall(currentMousePosition, objectTilemap);
            //         break;

                // case PlaceableObject.ObjectType.Spikes:
                //     spikesTilemap.SetTile(currentMousePosition, selectedObject.tile);
                //     break;
            // }

            selection_ObjectManager.UpdateObjectQuantity(-1);
        }
    }

    private void HidePreviousTilePlaceablility() {
        if (previousMousePosition != currentMousePosition) {
            placeabilityTilemap.SetTile(previousMousePosition, null);
            previousMousePosition = currentMousePosition;
        }
    }
}
