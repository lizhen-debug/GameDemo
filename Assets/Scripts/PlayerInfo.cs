using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Info
{
    public int min, max, cur;
    public Info(int min = 0, int max = 100, int cur = 0)
    {
        this.min = min;
        this.max = max;
        this.cur = cur;
    }

    public bool increase(int val)
    {
        this.cur = Math.Min(this.cur + val, this.max);
        return true;
    }
    public bool decrease(int val)
    {
        if(this.cur - val < 0){ return false;}
        this.cur -= val;
        return true;
    }

}

public class FoodInfo : Info
{
   public FoodInfo(int min = 0, int max = 100, int cur = 0) : base(min, max, cur) { }
}

public class SpiritInfo : Info
{
    public SpiritInfo(int min = 0, int max = 100, int cur = 0) : base(min, max, cur) { }
}

public class ActionInfo : Info
{
    public ActionInfo(int min = 0, int max = 10, int cur = 0) : base(min, max, cur) { }
}

public class PlayerInfo
{
    public SpiritInfo spirit_info = new SpiritInfo(0,100,100);
    public FoodInfo food_info = new FoodInfo(0,100,100);
    public ActionInfo action_info = new ActionInfo(0,10,10);
}
