using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<Item, List<Resource>> itemsToResources = new();
    private Dictionary<Resource, SkeletonCollector> resourcesToActors;
    private Dictionary<SkeletonCollector, Resource> actorsToResources;

    private void Start() {
        var resources = FindObjectsOfType<Resource>();

        foreach (var resource in resources) {
            if (!itemsToResources.ContainsKey(resource.RetrievableItem))
                itemsToResources[resource.RetrievableItem] = new List<Resource>();

            itemsToResources[resource.RetrievableItem].Add(resource);
        }

        resourcesToActors = new Dictionary<Resource, SkeletonCollector>();
        actorsToResources = new Dictionary<SkeletonCollector, Resource>();
    }

    public Resource GetResourceForActor(SkeletonCollector actor) {
        Vector3 actorPosition = actor.transform.position;
        Resource closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var resource in itemsToResources[actor.resourceType]) {
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
