using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Resource[] resources;

    private void Start() {
        resources = FindObjectsOfType<Resource>();
    }

    public Resource GetResourceForActor(SkeletonCollector actor) {
        Vector3 actorPosition = actor.transform.position;
        Resource closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var resource in resources) {
            if (resource == null) continue;

            var distance = (resource.transform.position - actorPosition).magnitude;
            if (distance < minDistance) {
                minDistance = distance;
                closest = resource;
            }
        }

        return closest;
    }
}
