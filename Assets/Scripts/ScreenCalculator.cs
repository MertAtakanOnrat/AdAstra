using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenCalculator
{
    static float left;
    static float right;
    static float top;
    static float bottom;

    /// <summary>
    /// Ekranın sol kenarının kordinatlarını verir.
    /// </summary>
    public static float Left { get { return left; } }
    /// <summary>
    /// Ekranın sag kenarının kordinatlarını verir.
    /// </summary>
    public static float Right { get { return right; } }
    /// <summary>
    /// Ekranın ust kenarının kordinatlarını verir.
    /// </summary>
    public static float Top { get { return top; } }
    /// <summary>
    /// Ekranın alt kenarının kordinatlarını verir.
    /// </summary>
    public static float Bottom { get { return bottom; } }

    public static void Init()
    {
        float screenZaxis = -Camera.main.transform.position.z;
        Vector3 leftBottomCorner = new Vector3(0, 0, screenZaxis);
        Vector3 rightTopCorner = new Vector3(Screen.width, Screen.height, screenZaxis);

        Vector3 leftBottomCornerGameWorld = Camera.main.ScreenToWorldPoint(leftBottomCorner);
        Vector3 rightTopCornerGameWorld = Camera.main.ScreenToWorldPoint(rightTopCorner);

        left = leftBottomCornerGameWorld.x;
        right = rightTopCornerGameWorld.x;
        top = rightTopCornerGameWorld.y;
        bottom = leftBottomCornerGameWorld.y;
    }
}
