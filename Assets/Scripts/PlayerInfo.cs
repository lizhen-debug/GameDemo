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

    public void increase(int val)
    {
        this.cur += val;
    }
    public void decrease(int val)
    {
        this.cur -= val;
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

public class PlayerInfo
{
    public SpiritInfo spirit_info = new SpiritInfo();
    public FoodInfo food_info = new FoodInfo();
}
