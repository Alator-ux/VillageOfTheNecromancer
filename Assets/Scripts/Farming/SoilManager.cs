using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

class SoilManager {
    private Dictionary<Vector2Int, Soil> PositionsToSoils = new();
    private Dictionary<Soil, Vector2Int> SoilsToPositions = new();

    private static SoilManager instance;

    public static SoilManager Instance {
        get {
            instance ??= new SoilManager();

            return instance;
        }
    }

    public void RegisterSoil(Soil soil, Vector2Int position) {
        PositionsToSoils[position] = soil;
        SoilsToPositions[soil] = position;
    }

    public List<Soil> Neighbours(Soil soil) {
        var neighbours = new List<Soil>();
        if (!SoilsToPositions.ContainsKey(soil)) return neighbours;

        var position = SoilsToPositions[soil];

        for (var dx = -1; dx <= 1; dx++) {
            for (var dy = -1; dy <= 1; dy++) {
                var key = position + new Vector2Int(dx, dy);
                if (PositionsToSoils.ContainsKey(key) && key != position) {
                    neighbours.Add(PositionsToSoils[key]);
                }
            }
        }

        return neighbours;
    }
}
