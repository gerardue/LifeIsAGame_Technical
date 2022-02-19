using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathParabolic
{
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t, AnimationCurve parabolicCurve)
    {

        var mid = Vector3.Lerp(start, end, t);

        //return new Vector3(mid.x, Curve(height, t), mid.z);
        return new Vector3(mid.x, Curve(height, parabolicCurve.Evaluate(t)), mid.z);
    }

    private static float Curve(float height, float t)
    {
        //return -4 * (height * (t * t)) + 4 * (height * t);
        return height * t;
    }
}
