using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovingObject : MovingObject
{
    PathFinder pathFinder;
    override protected void Start()
    {
        base.Start();
        pathFinder = new PathFinder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
