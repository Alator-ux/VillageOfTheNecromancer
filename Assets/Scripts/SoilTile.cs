using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewSoilTile", menuName = "Tiles/SoilTile")]
public class SoilTile : Tile
{
    public bool Wet;
    public bool Cursed;
}
