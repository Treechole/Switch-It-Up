using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Custom Tile", menuName = "Custom Tile")]
public class CustomTile : Tile {
    [SerializeField] private bool placeable;
    [SerializeField] private Sprite redArea;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go) {
        CurrentTileIsPlaceable(tilemap);
        return base.StartUp(position, tilemap, go);
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap) {
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
        base.GetTileData(position, tilemap, ref tileData);

        if (!placeable) {
            tileData.sprite = redArea;
        }
    }

    private void CurrentTileIsPlaceable(ITilemap tilemap) {
        Transform player = GameObject.Find("Player").transform;
        
        if (Vector3.Magnitude(player.position - transform.GetPosition()) < 2) {
            placeable = false;
        } else {
            placeable = true;
        }
    }
}
