using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInfo
{
    public SpiritInfo spirit_info = new SpiritInfo(0,100,100);
    public FullnessInfo food_info = new FullnessInfo(0,100,100);
    public ActionInfo action_info = new ActionInfo(0,10,10);
    public List<Item> Inventory = new List<Item>();
}

public class PlayerChangeInfo
{
    string info_type;//spirit_info.xxx  food_info.xxx   action_info.xxx
    string op_type;//set    increase    decrease
    string value;
}
