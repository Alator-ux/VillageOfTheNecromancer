using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skeleton Spawn Scroll", menuName = "Items/Scrolls")]
public class SkeletonSpawnScroll : Item
{
    public Item resourceType;
    public int skeletonsCount = 3;
    public SkeletonCollector skeletonPrefab;

    public SkeletonCollector SkeletonPrefab { get => skeletonPrefab; }

    private void OnEnable()
    {
        UseScriptName = "SkeletonSpawnScrollUse";
    }
}
