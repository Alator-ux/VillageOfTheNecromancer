using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public static class Vector3Extension
{
    private static float eps = 0.05f;
    public static Vector2 ToVector2(this Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }

    public static bool NearlyEqual(this Vector3 vec3, Vector2 vec2)
    {
        return Math.Abs(vec3.x - vec2.x) <= eps && Math.Abs(vec3.y - vec2.y) <= eps;
    }
    public static bool NearlyEqual(this Vector3 vec3, Vector3 otherVec3)
    {
        return Math.Abs(vec3.x - otherVec3.x) <= eps 
            && Math.Abs(vec3.y - otherVec3.y) <= eps
            && Math.Abs(vec3.z - otherVec3.z) <= eps;
    }
}

public static class Vector2Extension
{
    private static float eps = 0.05f;
    public static Vector3 ToVector3(this Vector2 vec2, float z = 0f)
    {
        return new Vector3(vec2.x, vec2.y, z);
    }

    public static bool NearlyEqual(this Vector2 vec2, Vector2 otherVec2)
    {
        return Math.Abs(vec2.x - otherVec2.x) <= eps 
            && Math.Abs(vec2.y - otherVec2.y) <= eps;
    }
    public static bool NearlyEqual(this Vector2 vec2, Vector3 otherVec3)
    {
        return Math.Abs(vec2.x - otherVec3.x) <= eps
            && Math.Abs(vec2.y - otherVec3.y) <= eps;
    }
}