using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Object", menuName = "Placeable Object")]
public class PlaceableObject : ScriptableObject {

    public TileBase tile;
    public Sprite sprite;
    public ObjectType type;

    public enum ObjectType {
        Wall,
        Spikes,
        FireballShooter
    }
}