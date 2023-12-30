using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Routine
{
    public Transform waypoint;
    public float timeAtWaypoint;
}
public class NPCRoutine : MonoBehaviour
{
    private int currentWaypoint = 0;
    private float timeAtCurrentWaypoint = 0;

    [SerializeField] List<Routine> NPCSchedule = new List<Routine>();
    void Start()
    {
    }
    
    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (NPCSchedule.Count == 0)
            return;

        Transform targetWaypoint = NPCSchedule[currentWaypoint].waypoint;
        float step = 5 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        timeAtCurrentWaypoint += Time.deltaTime;

        if (timeAtCurrentWaypoint >= NPCSchedule[currentWaypoint].timeAtWaypoint)
        {
            timeAtCurrentWaypoint = 0f;
            currentWaypoint = (currentWaypoint + 1) % NPCSchedule.Count;
        }
    }
}
