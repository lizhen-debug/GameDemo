using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GlobalFunctions
{
   
    public static Player player;
    public static List<GameObject> cam_arr = new List<GameObject>();
    public static int cur_cam_idx = 0;

    public static List<Action> act_arr = new List<Action>();

    


    public enum OperateType
    {
        INCREASE, DECREASE,
    }

    public static void InitializeAction(Player player)
    {
        act_arr.Add(() => Event.FeedCat(player));
        act_arr.Add(() => Event.CovidTest(player));
        act_arr.Add(() => Event.EatFood(player));
        act_arr.Add(() => Event.ExchangeFood(player));
        act_arr.Add(() => Event.Cook(player));
        act_arr.Add(() => Event.Chat(player));
        act_arr.Add(() => Event.ViewNews(player));
    }

    public static void ChangeCamera(int cam_idx)
    {
        cam_arr[cam_idx].SetActive(true);
        foreach(GameObject cam in cam_arr)
        {
            if (cam == cam_arr[cam_idx]) { continue; }
            cam.SetActive(false);
        }
    }

    public static void InvokeAction(int act_idx)
    {
        act_arr[act_idx].Invoke();
    }

    public static void ChangePlayerInfo(Info info, OperateType operate_type, int val)
    {
        if(operate_type == OperateType.INCREASE)
        {
            info.increase(val);
        }
        if (operate_type == OperateType.DECREASE)
        {
            info.decrease(val);
        }
    }

    public static void EndDay()
    {
        // scene transition effect
        
        // adjust variables
    }


   
}
