using UnityEngine;
using System.Collections;

public static class Globals
{

    public const float gCamTilt = 30;

    public static GameObject gCamera;
    public static GameObject gPlayer;

    public const int kCreaturesLayer = 9;
    public const int kSceneryLayer = 8;
    public const int kGroundLayer = 10;

    public static void Init()
    {
        gCamera = Camera.main.gameObject;
        gPlayer = Creature.Player.gameObject;

        gCamera.transform.rotation = Quaternion.Euler(gCamTilt, 0, 0);
    }

}

