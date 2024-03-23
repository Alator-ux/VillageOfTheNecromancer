using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FriendlyPlant : AnimatedPlant
{
    protected override List<bool> Conditions()
    {
        var l = base.Conditions();
        l.Add(FriendsArround() >= 1);
        return l;
    }

    private int FriendsArround() {
        var neighbourSoils = SoilManager.Instance.Neighbours(Soil);
        return neighbourSoils.Select(soil => soil.CurrentPlant).Where(plant => plant is FriendlyPlant).Count();
    }
}
