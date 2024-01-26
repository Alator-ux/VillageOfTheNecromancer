using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITools
{
    public static List<GameObject> RaycastGameObjects(Vector3 mousePosition, LayerMask layer)
    {
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            return raycastResults.ConvertAll<GameObject>(raycastObject => raycastObject.gameObject);
        }

        return new List<GameObject>();
    }
    public static GameObject GameObjectWithTag(List<GameObject> gameObjects, string tag)
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.CompareTag(tag))
            {
                return gameObject;
            }
        }
        return null;
    }
}
