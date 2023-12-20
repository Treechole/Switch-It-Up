using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Custom Tile", menuName = "Custom Tile")]
public class CustomTile : Tile {
    public override void RefreshTile(Vector3Int position, ITilemap tilemap) {
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

    
}
