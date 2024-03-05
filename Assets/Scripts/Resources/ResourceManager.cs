using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Resource[] resources;
    private Dictionary<Resource, SkeletonCollector> resourcesToActors;
    private Dictionary<SkeletonCollector, Resource> actorsToResources;

    private void Start() {
        resources = FindObjectsOfType<Resource>();
        resourcesToActors = new Dictionary<Resource, SkeletonCollector>();
        actorsToResources = new Dictionary<SkeletonCollector, Resource>();
    }

    public Resource GetResourceForActor(SkeletonCollector actor) {
        Vector3 actorPosition = actor.transform.position;
        Resource closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var resource in resources) {
            if (resource == null) continue;
            if (resourcesToActors.ContainsKey(resource)) continue;

            var distance = (resource.transform.position - actorPosition).magnitude;
            if (distance < minDistance) {
                minDistance = distance;
                closest = resource;
            }
        }

        if (closest != null) {
            resourcesToActors[closest] = actor;
            actorsToResources[actor] = closest;
        }

        return closest;
    }

    public void FreeResource(SkeletonCollector actor) {
        if (!actorsToResources.ContainsKey(actor)) return;

        var resource = actorsToResources[actor];
        resourcesToActors.Remove(resource);
        actorsToResources.Remove(actor);
    }
}
