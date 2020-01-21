using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum UseShip
{
    normal, missile
}

public static class CosmoXtrikerController
{
    public static void CallCameraSetting()
    {
      //  UnityEngine.XR.XRSettings.showDeviceView = false;
    }

    /// <summary>
    /// Playerが使う戦艦
    /// </summary>
    public static UseShip PlayerUseShip { get; set; }

}