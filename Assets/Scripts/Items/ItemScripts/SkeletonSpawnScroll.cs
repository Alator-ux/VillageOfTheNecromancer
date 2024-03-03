using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skeleton Spawn Scroll", menuName = "Items/Scrolls")]
public class SkeletonSpawnScroll : Item
{
    [SerializeField]
    private SkeletonCollector skeletonPrefab;

    public SkeletonCollector SkeletonPrefab { get => skeletonPrefab; }

    private void OnEnable()
    {
        UseScriptName = "SkeletonSpawnScrollUse";
    }
}
