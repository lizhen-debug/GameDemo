using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GlobalFunctions
{
    public static List<GameObject> cam_arr = new List<GameObject>();
    public static int cur_cam_idx = 0;
    public static void ChangeCamera(int cam_idx)
    {
        cam_arr[cam_idx].SetActive(true);
        foreach(GameObject cam in cam_arr)
        {
            if (cam == cam_arr[cam_idx]) { continue; }
            cam.SetActive(false);
        }
    }

}
