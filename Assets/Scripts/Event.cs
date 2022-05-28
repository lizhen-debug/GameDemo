using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Event
{
    public static void EatApple(Player player)
    {
        GlobalFunctions.ChangePlayerInfo(player.player_info.food_info, GlobalFunctions.OperateType.INCREASE, 10);
    }

}
