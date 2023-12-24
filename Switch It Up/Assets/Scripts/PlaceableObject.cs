using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Placeable Object", menuName = "Placeable Object")]
public class PlaceableObject : ScriptableObject {
    // Create a object that has a tile attribute, sprite attribute and stackable attribute
    public enum ObjectType {
        Wall,
        Spikes
    }

    public TileBase tile;
    public Sprite sprite;
    public ObjectType type;
}